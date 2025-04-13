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
        private static List<CarryTask> _tasks = new List<CarryTask>();
        public static void AddToQueue()
        {
            // 取得起始區域代號
            string start_code = KukaParm.KukaAreaModels.FirstOrDefault(m => m.NodeList.Contains(KukaParm.StartNode.Code)).AreaCode;
            
            // 建立搬運任務資訊
            CarryTask task = new CarryTask(_task_id, KukaParm.StartNode.Code, KukaParm.GoalNode.Code, start_code);
            _task_id = _task_id++;
            _tasks.Add(task);
        }
        
        /// <summary>
        /// 當前區域被呼叫，找到可執行的搬運任務
        /// </summary>
        /// <param name="start_area_code"></param>
        /// <returns></returns>
        public static CarryTask GetCallTask(string start_area_code)
        {
            CarryTask foundModel = _tasks.FirstOrDefault(m => m.AreaCode == start_area_code);

            if (foundModel != null)
            {
                Console.WriteLine($"找到的資料: Start = {foundModel.StartNode}, Goal = {foundModel.GoalNode}");
                foundModel.Called = true;       // 已呼叫
                return foundModel;
            }
            else
            {
                Console.WriteLine("找不到指定名稱的資料。");
                return null;
            }
        }
    }

    
    

    class CarryTask
    {
        public int ID { get; set; }
        public bool Called { get; set; } = false;
        public string AreaCode { get; set; }
        public string StartNode { get; set; }
        public string GoalNode { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime FinishTime { get; set; }

        public CarryTask(int task_id, string start_node, string goal_node, string areaCode)
        {
            ID = task_id;
            StartNode = start_node;
            GoalNode = goal_node;
            CreateTime = DateTime.Now;
            AreaCode = areaCode;
        }
    }
}
