using CefSharp.DevTools.DOM;
using Chump_kuka.Controller;
using Chump_kuka.Dispatchers;
using iCAPS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using static Chump_kuka.KukaModel;
using static Chump_kuka.Log;

namespace Chump_kuka
{
    internal class CarryTaskController
    {
        private static bool _agv_running = false;
        private static int _task_id = 1;
        private static KukaModel.CarryTask _current_task = null;
        
        private static BindingList<KukaModel.CarryTask> _task_queue = new BindingList<KukaModel.CarryTask>();      // 搬運任務佇列

        private static System.Timers.Timer _task_timer;

        private static List<string> _id_table = new List<string>();      // 暫存任務 ID 表，若任務柱列被刪除，可查詢刪除ID

        public static event Action<bool> OnTimerAlive;     // 計時器啟用事件

        public static KukaModel.CarryTask CurrentTask { get => _current_task; }

        static CarryTaskController()
        {
            FeedbackDispatcher.Called += FeedbackDispatcher_Called;

            if (!Env.ICapsServer) return;
            InitRecordTasks();
            _task_queue.ListChanged += task_queue_ListChanged;
        }

        /// <summary>
        /// 取得當天 InI 檔案內紀錄的任務，並實例
        /// </summary>
        private static void InitRecordTasks()
        {
            string file_path = KukaParm.GetTodayTaskPath();
            int.TryParse(INiReader.ReadINIFile(file_path, "tasks", "task_last_id"), out int record_count);        // 任務數量
            if (record_count > 0)
            {
                for (int index = 1; index <= record_count; index++)
                {
                    string task_json = INiReader.ReadINIFile(file_path, "tasks", $"{index}", 65535);
                    if(task_json == "")      // 任務已被刪除
                    {
                        continue;
                    }
                    CarryTask raed_task = Newtonsoft.Json.JsonConvert.DeserializeObject<CarryTask>(task_json);
                    
                    // 未完成任務需綁定模型
                    if(raed_task.FinishTime == null)
                    {
                        if (raed_task?.StartNode.NodeModel != null)
                        {
                            foreach (Area area in KukaParm.KukaAreaModels)
                            {
                                KukaModel.Node node = area.GetNode(raed_task?.StartNode.NodeModel.NodeCode);
                                if (node != null)
                                {
                                    raed_task.StartNode.NodeModel = node;
                                    break;
                                }
                            }
                        }
                        if (raed_task?.GoalNode.NodeModel != null)
                        {
                            foreach (Area area in KukaParm.KukaAreaModels)
                            {
                                KukaModel.Node node = area.GetNode(raed_task?.StartNode.NodeModel.NodeCode);
                                if (node != null)
                                {
                                    raed_task.GoalNode.NodeModel = node;
                                    break;
                                }
                            }
                        }
                    }
                    
                    _task_queue.Add(raed_task);
                }

                _task_id = record_count + 1;
                _id_table = _task_queue.Select(t => t.ID.ToString()).ToList();      // 將所有 ID 加進暫存任務 ID 表

                initTimer();        // 自動開始流程
            }

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 當任務狀態更改時，自動儲存 InI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void task_queue_ListChanged(object sender, ListChangedEventArgs e)
        {
            // 當任務狀態更改時，自動儲存，防止系統崩潰後，資料消失
            // 任務清單檔案依日期建立&儲存
            
            string file_path = KukaParm.GetTodayTaskPath();
            int last_id = _task_queue.Count > 0 ? _task_queue[_task_queue.Count - 1].ID : 0;
            INiReader.WriteINIFile(file_path, "tasks", "task_last_id", last_id.ToString());     // 紀錄最後一筆任務id
            
            int change_index = e.NewIndex;

            if (e.ListChangedType == ListChangedType.ItemDeleted)       // 如果是刪除事件
            {
                string rm_id = _id_table[change_index];
                INiReader.WriteINIFile(file_path, "tasks", rm_id, null);
                _id_table.Remove(rm_id);
            }
            else
            {
                KukaModel.CarryTask task = _task_queue[change_index];       // 取得更新項目
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

            string can_carry_mission_code = GetCallTask(start_area_code);
            if (can_carry_mission_code != null)
            {
                // KukaApiController.PubCarryTask();
                ChatController.PubLog($"接收叫車任務，等待執行。");
                HttpListenerDispatcher.ManualHeardEvent(can_carry_mission_code, start_area_code, 1);        // 觸發接收報工系統 call 事件
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
        public static void AddToQueue(KukaModel.CarryModel start_node, KukaModel.CarryModel goal_node, out string mission_code, bool wait=true, 
                    bool is_plan = false)
        {
            mission_code = "";

            // 初始化計時器
            if (_task_timer == null)
            {
                initTimer();
            }

            // 判定是否建立重複起始點(起始點已在任務列表中，且該任務尚未完成)
            bool exists_task = _task_queue.Any(m => 
                                        m.StartNode.NodeModel?.NodeCode == start_node.NodeModel?.NodeCode && 
                                        m.FinishTime == null);
            if (exists_task)
            {
                ChatController.PubError("派發重複任務 !");
                return;
            }
            
            // 建立搬運任務資訊
            KukaModel.CarryTask task = new KukaModel.CarryTask(_task_id, !wait, start_node, goal_node);
            mission_code = task.MissionCode;
            task.IsPlan = is_plan;      // 判斷是否為策略任務

            // 最後一區的任務優先執行
            if (start_node.AreaCode == KukaParm.KukaAreaModels[KukaParm.KukaAreaModels.Count - 1].AreaCode)
            {
                task.Called = true;
            }
            _task_id++;

            _task_queue.Add(task);      // 加入佇列

            // 如果起點是 node 標記為任務占用
            if(start_node.NodeModel != null)
            {
                start_node.NodeModel.NodeStatus = 1;
            }

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 尋找可執行的搬運任務
        /// </summary>
        /// <returns></returns>
        private static bool FindAndAssignTask()
        {
            CarryTask plan_task = _task_queue.FirstOrDefault(task => task.Called && task.FinishTime == null && task.IsPlan);
            if (plan_task != null)
            {
                KukaApiController.PubCarryTask(plan_task);
                ChatController.PubLog($"已派發任務，ID: {plan_task.ID}");
                return true;
            }

            foreach (KukaModel.CarryTask task in _task_queue)
            {
                if (task.Called && task.FinishTime == null)
                {
                    KukaModel.Node goal_node = task.GoalNode.NodeModel;
                    // 檢查目標是否為貨架點
                    if (goal_node != null)     // 目標為貨架點
                    {
                        // 檢查目標貨架點是否搬允許搬運
                        
                        if (goal_node.IsEmpty())
                        {
                            // 貨架點無占用，可直接派發任務
                        }
                        else
                        {
                            // 貨架點占用，判斷是否上鎖
                            if (goal_node.Lock)
                            {
                                // 貨架點已鎖定，執行策略
                                KukaModel.Area start_area;
                                if (task.StartNode.NodeModel != null) start_area = task.StartNode.NodeModel.Parent;
                                else start_area = KukaParm.KukaAreaModels.FirstOrDefault(area => area.AreaCode == task.StartNode.AreaCode);

                                TaskPlan(task.ID, goal_node, start_area);
                                return false;
                            }

                            ChatController.PubLog($"當前任務[{task.ID}]無法執行。目標貨架點滿載，優先執行下一筆任務");
                            continue;
                        }
                    }
                    else       // 目標為區域
                    {
                        KukaModel.Area goal_area = KukaParm.KukaAreaModels.FirstOrDefault(area => area.AreaCode == task.GoalNode.AreaCode);
                        // 先搜尋是否有空貨架點
                        KukaModel.Node empty_node = goal_area?.GetEmptyNode();
                        if (empty_node == null)
                        {
                            // 沒有空貨架點，搜尋是否有上鎖貨架點
                            KukaModel.Node lock_node = goal_area?.GetLockNode();
                            if(lock_node == null)
                            {
                                // 找不到上鎖貨架，執行下一筆
                                ChatController.PubLog($"當前任務[{task.ID}]無法執行。目標區域皆滿載，優先執行下一筆任務");
                                continue;
                            }

                            // 找到上鎖貨架，執行策略
                            KukaModel.Area start_area;
                            if (task.StartNode.NodeModel != null) start_area = task.StartNode.NodeModel.Parent;
                            else start_area = KukaParm.KukaAreaModels.FirstOrDefault(area => area.AreaCode == task.StartNode.AreaCode);
                            TaskPlan(task.ID, lock_node, start_area);
                            return false;
                        }
                        else
                        {
                            // 加入可搬運貨架點
                            task.GoalNode.NodeModel = empty_node;
                        }
                    }
                    
                    // 派發 API
                    KukaApiController.PubCarryTask(task);
                    _current_task = task;
                    ChatController.PubLog($"已派發任務，ID: {_current_task.ID}");

                    return true; 
                }
            }

            return false;
        }

        private static bool TaskPlan(int task_id, KukaModel.Node lock_node, Area start_area)
        {
            // 執行搬運策略
            // 需確認已經指定目標貨架點，並且該貨架點已鎖定

            KukaModel.Node start_empty_node = start_area.GetEmptyNode();
            if (start_empty_node != null)       // 策略A => 將上鎖貨架搬運到當前區域無佔用位置
            {
                ChatController.PubLog($"[task_{task_id}] > 啟動策略A，搬運到起始區域。");

                AddToQueue(new CarryModel(lock_node.NodeName, null, lock_node),
                            new CarryModel(start_empty_node.NodeName, null, start_empty_node),
                            out string mission_code, false);
                AppendTaskLog(mission_code, $"[task_{task_id}] 策略A [搬運到起始區域]\n===");
            }
            else        // 策略B => 將上鎖貨架搬運到下一區域無佔用位置
            {
                // 目前區域無空位，更換策略至下一區域
                KukaModel.Node next_empty_node = lock_node.Parent?.Next().GetEmptyNode();
                if (next_empty_node == null) return false;      // 找不到下一目標

                ChatController.PubLog($"[task_{task_id}] > 啟動策略B，搬運到下一區域。");
                AddToQueue(new CarryModel(lock_node.NodeName, null, lock_node),
                            new CarryModel(next_empty_node.NodeName, null, next_empty_node),
                            out string mission_code, false);
                AppendTaskLog(mission_code, $"[task_{task_id}] 策略B [搬運到下一區域]\n===");
            }
            _current_task = _task_queue.FirstOrDefault(task => task.ID == task_id);
            return true;
        }

        /// <summary>
        /// 將任務模型列表轉換成簡化模型，方便資料傳輸
        /// </summary>
        /// <returns></returns>
        public static KukaModel.SimpleCarryTask[] GetQueueArray()
        {
            var simple_queue = _task_queue.Select(queue => new KukaModel.SimpleCarryTask(queue)).ToArray();
            return simple_queue;
        }
        
        /// <summary>
        /// 區域被呼叫，找到可執行的搬運任務
        /// </summary>
        /// <param name="start_area_code"></param>
        /// <returns></returns>
        private static string GetCallTask(string start_area_code)
        {
            // 找到符合開始區域且尚未執行的第一筆資料
            KukaModel.CarryTask call_task = _task_queue.FirstOrDefault(task => task.StartNode.AreaCode == start_area_code &&
                                                                                task.Called == false &&
                                                                                task.FinishTime == null);
            if (call_task != null)
            {
                call_task.Called = true;
                ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
                return call_task.MissionCode;
            }

            return null;
        }

        //private static bool PubCarryList()
        //{
        //    if (_current_task != null)
        //    {
        //        // Console.WriteLine($"找到的資料: Start = {foundModel.StartNode}, Goal = {foundModel.GoalNode}");
        //        // 自動指派起始終點節點
        //        KukaParm.StartNode = _current_task.StartNode;
        //        KukaParm.GoalNode = _current_task.GoalNode;
        //        // _current_task.Called = true;       // 已呼叫

        //        ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("找不到指定名稱的資料。");
        //        return false;
        //    }
        //}

        /// <summary>
        /// 回報任務完成，並重置 _current_task
        /// </summary>
        public static void FeedbackFinish(string mission_code)
        {
            //if (_current_task != null) 
            //    _current_task.FinishTime = DateTime.Now;
            KukaModel.CarryTask finish_task = _task_queue.FirstOrDefault(task => task.MissionCode == mission_code);
            finish_task.FinishTime = DateTime.Now;

            // 判斷結完成的任務是否為策略任務
            if (finish_task.IsPlan)
            {
                // 若是策略任務，移轉鎖定狀態
                if (finish_task.StartNode.NodeModel.Lock)
                {
                    finish_task.StartNode.NodeModel.Lock = false;
                    finish_task.GoalNode.NodeModel.Lock = true;
                }

                finish_task.StartNode.NodeModel.NodeStatus = 0;
            }
            else
            {
                _current_task.StartNode.NodeModel.NodeStatus = 0;
            }
            _current_task = null;
            _task_timer.Start();

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 回報任務失敗，並重置 _current_task
        /// </summary>
        public static void FeedbackFail(string mission_code)
        {
            //if (_current_task != null)
            //    _current_task.FinishTime = DateTime.MinValue;
            // _current_task = null;
            KukaModel.CarryTask cancle_task = _task_queue.FirstOrDefault(task => task.MissionCode == mission_code);
            cancle_task.FinishTime = DateTime.MinValue;
            _current_task.StartNode.NodeModel.NodeStatus = 0;
            _current_task = null;

            _task_timer.Start();

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }

        /// <summary>
        /// 增加任務狀態紀錄
        /// </summary>
        /// <param name="log_message"></param>
        public static void AppendTaskLog(string mission_code, string log_message)
        {
            //if (_current_task != null)
            //    _current_task.LogMsg += $"[{DateTime.Now.ToString(@"MM/dd tt hh:mm:ss")}] {log_message}\n";
            KukaModel.CarryTask task = _task_queue.FirstOrDefault(t => t.MissionCode == mission_code);
            task.LogMsg += $"[{DateTime.Now.ToString(@"MM/dd tt hh:mm:ss")}] {log_message}\n";

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
                ChatController.PubError($"錯誤: 請確認搬運任務編號正確[{rm_id}]");
                return;
            }


            if (_current_task?.ID == rm_id)
            {
                ChatController.PubError($"搬運任務[{rm_id}]運行中，無法移除");
                return;
            }

            KukaModel.CarryTask target = _task_queue.FirstOrDefault(m => m.ID == rm_id);       // 找到 ID 對應任務
            if (target != null)
            {
                _task_queue.Remove(target);
                ChatController.PubLog($"已從任務列表中移除搬運任務[{rm_id}]");
            }
            else
            {
                ChatController.PubLog($"找不到指定任務[{rm_id}]");
            }
            
            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }
    }


    
}
