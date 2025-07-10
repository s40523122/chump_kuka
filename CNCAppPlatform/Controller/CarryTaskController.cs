using Chump_kuka.Controller;
using Chump_kuka.Dispatchers;
using iCAPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace Chump_kuka
{
    internal class CarryTaskController
    {
        private static bool _agv_running = false;
        private static int _task_id = 1;
        private static CarryTask _current_task = null;
        
        private static BindingList<CarryTask> _task_queue = new BindingList<CarryTask>();      // 搬運任務佇列

        private static System.Timers.Timer _task_timer;

        private static List<string> _id_table = new List<string>();      // 暫存任務 ID 表，若任務柱列被刪除，可查詢刪除ID

        public static event Action<bool> OnTimerAlive;     // 計時器啟用事件


        static CarryTaskController()
        {
            FeedbackDispatcher.Called += FeedbackDispatcher_Called;

            if (!Env.ICapsServer) return;
            InitRecordTasks();
            _task_queue.ListChanged += task_queue_ListChanged;
        }

        private static void InitRecordTasks()
        {
            // 取得當天 InI 檔案內紀錄的任務，並實例
            string file_path = KukaParm.GetTodayTaskPath();
            int.TryParse(INiReader.ReadINIFile(file_path, "tasks", "task_last_id"), out int record_count);        // 任務數量
            if (record_count > 0)
            {
                for (int index = 0; index < record_count; index++)
                {
                    string task = INiReader.ReadINIFile(file_path, "tasks", $"{index + 1}", 65535);
                    if(task == "")      // 任務已被刪除
                    {
                        continue;
                    }
                    _task_queue.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<CarryTask>(task));
                }

                _task_id = record_count + 1;
                _id_table = _task_queue.Select(t => t.ID.ToString()).ToList();      // 將所有 ID 加進暫存任務 ID 表
            }

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        private static void task_queue_ListChanged(object sender, ListChangedEventArgs e)
        {
            // 當任務狀態更改時，自動儲存，防止系統崩潰後，資料消失
            // 任務清單檔案依日期建立&儲存
            
            string file_path = KukaParm.GetTodayTaskPath();
            INiReader.WriteINIFile(file_path, "tasks", "task_last_id", _task_queue[_task_queue.Count - 1].ID.ToString());     // 紀錄最後一筆任務id
            
            int change_index = e.NewIndex;

            if (e.ListChangedType == ListChangedType.ItemDeleted)       // 如果是刪除事件
            {
                string rm_id = _id_table[change_index];
                INiReader.WriteINIFile(file_path, "tasks", rm_id, null);
                _id_table.Remove(rm_id);
            }
            else
            {
                CarryTask task = _task_queue[change_index];       // 取得更新項目
                string task_msg = Newtonsoft.Json.JsonConvert.SerializeObject(task);
                INiReader.WriteINIFile(file_path, "tasks", task.ID.ToString(), task_msg);       //單筆任務寫入

                // 若新資料，加進暫存任務 ID 表
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    _id_table.Add(task.ID.ToString());
                }
            }            
        }

        private static void initTimer()
        {
            Log.Append("候車計時器初始化", "SYSTEM", nameof(CarryTaskController));
            // 設定計時器
            _task_timer = new System.Timers.Timer();
            _task_timer.Interval = 200; // 每 0.2 秒請求一次

            _task_timer.Elapsed += ProcessNextApiAsync;
            _task_timer.AutoReset = true; // 是否重複執行（true 表示會一直觸發）
            _task_timer.Start();
            //_task_timer.Enabled = true;
            //_task_timer.Tick += ProcessNextApiAsync;
        }
        private static async void ProcessNextApiAsync(object sender, EventArgs e)
        {
            // 停止計時器，確保在請求處理中不會再觸發計時器
            _task_timer.Stop();

            OnTimerAlive?.Invoke(true);

            await Task.Delay(800);
            OnTimerAlive?.Invoke(false);
            // AppendRobotStatusTask();        // 機器人狀態查詢為固定行程

            bool build_task = FindAndAssignTask();
            // 如果有派發任務，則停止計時器
            if (!build_task)
            {
                _task_timer.Start();
            }
            
        }

        private static void FeedbackDispatcher_Called(object sender, TextEventArgs e)
        {
            // TODO 等待上一筆任務結束


            // 接收到叫車命令，尋找可派發任務
            string start_area_code = e.Message;

            HttpListenerDispatcher.ManualHeardEvent(start_area_code, 1);        // 觸發接收報工系統 call 事件

            bool can_carry = GetCallTask(start_area_code);
            if (can_carry)
            {
                // KukaApiController.PubCarryTask();
                ChatController.PubLog($"接收叫車任務，等待執行。");
            }
            else
            {
                ChatController.PubLog($"接收叫車任務，無可執行搬運任務。(起始節點: {start_area_code})");
            }
        }

        /// <summary>
        /// 將搬運任務加入等待列表
        /// </summary>
        /// <param name="wait">若為 true，需等待報工系統通知；反之，直接派發任務。</param>
        public static void AddToQueue(bool wait=true)
        {
            if (_task_timer == null)
            {
                initTimer();
            }
            // 取得起始區域代號

            string start_area_code; 
            KukaAreaModel try_find = KukaParm.KukaAreaModels.FirstOrDefault(m => m.NodeList.Contains(KukaParm.StartNode.Code));
            if(try_find == null)
            {
                start_area_code = KukaParm.StartNode.Code;
            }
            else
            {
                start_area_code = try_find.AreaCode;
            }

            // 判定是否重複起始點(起始點已在任務列表中，且該任務尚未完成)
            bool exists_task = _task_queue.Any(m => 
                                        m.StartNode.Code == KukaParm.StartNode.Code && 
                                        m.StartNode.Type == KukaParm.StartNode.Type  && 
                                        m.FinishTime == null);
            if (exists_task)
            {
                ChatController.PubLog("派發重複任務");
                return;
            }
            
            // 建立搬運任務資訊
            CarryTask task = new CarryTask(_task_id, !wait, KukaParm.StartNode, KukaParm.GoalNode, start_area_code);

            // 最後一區的任務優先執行
            if (start_area_code == KukaParm.KukaAreaModels[KukaParm.KukaAreaModels.Count - 1].AreaCode)
            {
                task.Called = true;
            }
            _task_id++;
            _task_queue.Add(task);

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI

            // 非等待或最後一區的任務優先執行
            //if (!wait || start_code == KukaParm.KukaAreaModels[KukaParm.KukaAreaModels.Count-1].AreaCode)
            //{
            //    _current_task = task;
            //    KukaApiController.PubCarryTask();
            //}
            // 先全部開放
            //_current_task = task;
            //KukaApiController.PubCarryTask();
        }

        private static bool IsAreaFully(KukaAreaModel area_model)
        {
            // 若滿載，返回 true，反之 false
            return !area_model.NodeStatus.Contains(0);
        }

        private static bool FindAndAssignTask()
        {
            foreach (CarryTask task in _task_queue)
            {
                if (task.Called && task.FinishTime == null)
                {
                    // 檢查目標是否滿載
                    if (task.GoalNode.Type == "NODE_AREA")
                    {
                        KukaAreaModel start_area = KukaParm.KukaAreaModels.FirstOrDefault(m => m.AreaCode == task.StartNode.Code);
                        KukaAreaModel target_area = KukaParm.KukaAreaModels.FirstOrDefault(m => m.AreaCode == task.GoalNode.Code);

                        CarryNode[] carry_nodes;
                        if (IsAreaFully(target_area))
                        {
                            // 目標區域滿載，跳過這一筆
                            ChatController.PubLog($"當前任務[{task.ID}]無法執行。目標區域滿載，優先執行下一筆任務");

                            if (IsAreaFully(start_area))        // 若當前區域滿載
                            {
                                if (!target_area.Next().NodeStatus.Contains(0))
                                {
                                    ChatController.PubLog($"當前任務[{task.ID}]無法執行。目標區域皆滿載，優先執行下一筆任務");
                                    continue;
                                }

                                carry_nodes = new CarryNode[]
                                {
                                    _current_task.GoalNode,
                                    new CarryNode(target_area.Next()),
                                    _current_task.StartNode,
                                    _current_task.GoalNode
                                };
                                ChatController.PubLog($"當前任務[{task.ID}]啟動策略。優先執行下一筆任務");
                            }
                            else
                            {
                                carry_nodes = new CarryNode[]
                                {
                                    _current_task.GoalNode,
                                    new CarryNode(start_area),
                                    _current_task.StartNode,
                                    _current_task.GoalNode
                                };
                            }
                        }
                        else
                        {
                            carry_nodes = new CarryNode[]
                            {
                                _current_task.StartNode,
                                _current_task.GoalNode
                            };
                         
                        }
                        KukaApiController.PubCarryTask(carry_nodes);
                    }
                    _current_task = task;

                    // 修改 start_node goal_node
                    KukaParm.StartNode = _current_task.StartNode;
                    KukaParm.GoalNode = _current_task.GoalNode;
                    KukaApiController.PubCarryTask();
                    // KukaApiController.PubCarryTask();

                    ChatController.PubLog($"已派發任務，ID: {_current_task.ID}");

                    return true;
                }
            }

            return false;
            // ChatController.PubLog($"無任務派發");
        }

        /// <summary>
        /// 將任務模型列表轉換成簡化模型，方便資料傳輸
        /// </summary>
        /// <returns></returns>
        public static SimpleCarryTask[] GetQueueArray()
        {
            var simple_queue = _task_queue.Select(q => new SimpleCarryTask(q)).ToArray();
            return simple_queue;
        }
        
        /// <summary>
        /// 區域被呼叫，找到可執行的搬運任務
        /// </summary>
        /// <param name="start_area_code"></param>
        /// <returns></returns>
        private static bool GetCallTask(string start_area_code)
        {
            // 找到符合開始區域且尚未執行的第一筆資料
            CarryTask call_task = _task_queue.FirstOrDefault(m => m.AreaCode == start_area_code && m.Called == false && m.FinishTime == null);
            if (call_task != null)
            {
                call_task.Called = true;
                ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
                return true;
            }
            return false;
        }

        private static bool PubCarryList()
        {
            if (_current_task != null)
            {
                // Console.WriteLine($"找到的資料: Start = {foundModel.StartNode}, Goal = {foundModel.GoalNode}");
                // 自動指派起始終點節點
                KukaParm.StartNode = _current_task.StartNode;
                KukaParm.GoalNode = _current_task.GoalNode;
                // _current_task.Called = true;       // 已呼叫

                ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
                return true;
            }
            else
            {
                Console.WriteLine("找不到指定名稱的資料。");
                return false;
            }
        }

        /// <summary>
        /// 回報任務完成，並重置 _current_task
        /// </summary>
        public static void FeedbackFinish()
        {
            if (_current_task != null) 
                _current_task.FinishTime = DateTime.Now;
            // _current_task = null;

            _task_timer.Start();

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 回報任務失敗，並重置 _current_task
        /// </summary>
        public static void FeedbackFail()
        {
            if (_current_task != null)
                _current_task.FinishTime = DateTime.MinValue;
            // _current_task = null;

            _task_timer.Start();

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 增加任務狀態紀錄
        /// </summary>
        /// <param name="log_message"></param>
        public static void AppendTaskLog(string log_message)
        {
            if (_current_task != null)
                _current_task.LogMsg += $"[{DateTime.Now.ToString(@"MM/dd tt hh:mm:ss")}] {log_message}\n";
            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 刪除指定任務
        /// </summary>
        /// <param name="log_message"></param>
        public static void RemoveTask(string task_id)
        {
            int.TryParse(task_id, out int rm_id);
            if (rm_id == 0)
            {
                ChatController.PubLog($"錯誤: 請確認搬運任務編號正確[{rm_id}]");
                return;
            }

            if (_current_task != null)
            {
                if (_current_task.ID == rm_id)
                {
                    ChatController.PubLog($"搬運任務[{rm_id}]運行中，無法移除");
                    return;
                }
            }
            else
            {
                CarryTask target = _task_queue.FirstOrDefault(m => m.ID == rm_id);       // 找到 ID 對應任務
                if (target != null)
                {
                    _task_queue.Remove(target);
                    ChatController.PubLog($"已從任務列表中移除搬運任務[{rm_id}]");
                }
                else
                {
                    ChatController.PubLog($"找不到指定任務[{rm_id}]");
                }
            }
            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }
    }


    public class SimpleCarryTask
    {
        public string Called { get; set; }
        public int ID { get; set; }
        public string StartNode { get; set; }
        public string GoalNode { get; set; }
        public string CreateTime { get; set; }
        public string FinishTime { get; set; }
        public string LogMsg { get; set; }

        public SimpleCarryTask() { }
        public SimpleCarryTask(CarryTask task)
        {
            ID = task.ID;
            StartNode = task.StartNode.Name;
            GoalNode = task.GoalNode.Name;
            CreateTime = task.CreateTime.ToString(@"MM/dd tt hh:mm");
            FinishTime = task.FinishTime?.ToString(@"MM/dd tt hh:mm");
            Called = task.Called ? "🔔" : "🔕";
            LogMsg = task.LogMsg;

            if(FinishTime == null)
            {
                FinishTime = "";
            }
        }
    }

    public class CarryTask
    {
        public int ID { get; set; }
        public bool Called { get; set; } = false;
        public string AreaCode { get; set; }
        public CarryNode StartNode { get; set; }
        public CarryNode GoalNode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string LogMsg { get; set; } = "";

        public CarryTask(int task_id, bool called, CarryNode start_node, CarryNode goal_node, string areaCode)
        {
            ID = task_id;
            Called = called;
            StartNode = start_node;
            GoalNode = goal_node;
            CreateTime = DateTime.Now;
            AreaCode = areaCode;
            FinishTime = null;
        }
    }
}
