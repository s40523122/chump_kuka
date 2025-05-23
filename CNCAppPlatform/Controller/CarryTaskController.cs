using Chump_kuka.Controller;
using Chump_kuka.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    internal class CarryTaskController
    {
        private static int _task_id = 1;
        private static CarryTask _current_task = null;
        private static List<CarryTask> _carry_queue = new List<CarryTask>();


        static CarryTaskController()
        {
            FeedbackDispatcher.Called += FeedbackDispatcher_Called;
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
                KukaApiController.PubCarryTask();
            }
            else
            {
                Log.Append($"無可執行搬運任務。(起始節點: {start_area_code})", "WARRNING", "CarryTaskController");
            }
        }

        /// <summary>
        /// 將搬運任務加入等待列表
        /// </summary>
        /// <param name="wait">若為 true，需等待報工系統通知；反之，直接派發任務。</param>
        public static void AddToQueue(bool wait=true)
        {
            // 取得起始區域代號
            string start_code = KukaParm.KukaAreaModels.FirstOrDefault(m => m.NodeList.Contains(KukaParm.StartNode.Code)).AreaCode;
            
            // 建立搬運任務資訊
            CarryTask task = new CarryTask(_task_id, KukaParm.StartNode, KukaParm.GoalNode, start_code);
            _task_id++;
            _carry_queue.Add(task);

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI

            // 非等待或第2區域的任務優先執行
            if (!wait || start_code == KukaParm.KukaAreaModels[2].AreaCode)
            {
                _current_task = task;
                KukaApiController.PubCarryTask();
            }
            // 先全部開放
            //_current_task = task;
            //KukaApiController.PubCarryTask();
        }

        private static SimpleCarryTask[] GetQueueArray()
        {
            var simple_queue = _carry_queue.Select(q => new SimpleCarryTask(q)).ToArray();
            return simple_queue;
        }
        
        /// <summary>
        /// 當前區域被呼叫，找到可執行的搬運任務
        /// </summary>
        /// <param name="start_area_code"></param>
        /// <returns></returns>
        public static bool GetCallTask(string start_area_code)
        {
            // 找到符合開始區域且尚未執行的第一筆資料
            _current_task = _carry_queue.FirstOrDefault(m => m.AreaCode == start_area_code && m.Called == false && m.FinishTime == null);

            bool success = PubCarryTask();
            return success;
        }

        private static bool PubCarryTask()
        {
            if (_current_task != null)
            {
                // Console.WriteLine($"找到的資料: Start = {foundModel.StartNode}, Goal = {foundModel.GoalNode}");
                // 自動指派起始終點節點
                KukaParm.StartNode = _current_task.StartNode;
                KukaParm.GoalNode = _current_task.GoalNode;
                _current_task.Called = true;       // 已呼叫

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
            _current_task = null;

            ChatController.SyncCarryTask(GetQueueArray());      // 同步&更新所有 UI
        }
    }


    public class SimpleCarryTask
    {
        public bool Called { get; set; } = false;
        public int ID { get; set; }
        public string StartNode { get; set; }
        public string GoalNode { get; set; }
        public string CreateTime { get; set; }

        public string FinishTime { get; set; }

        public SimpleCarryTask() { }
        public SimpleCarryTask(CarryTask task)
        {
            ID = task.ID;
            StartNode = task.StartNode.Name;
            GoalNode = task.GoalNode.Name;
            CreateTime = task.CreateTime.ToString(@"MM/dd tt hh:mm");
            FinishTime = task.FinishTime?.ToString(@"MM/dd tt hh:mm");
            Called = task.Called;
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

        public CarryTask(int task_id, CarryNode start_node, CarryNode goal_node, string areaCode)
        {
            ID = task_id;
            StartNode = start_node;
            GoalNode = goal_node;
            CreateTime = DateTime.Now;
            AreaCode = areaCode;
            FinishTime = null;
        }
    }
}
