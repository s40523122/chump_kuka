using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Services
{
    public interface IKukaService
    {
        //IEnumerable<ListItem> FindCarryTask(string mission_code);

        /// <summary>
        /// 找到符合任務編號的任務實例
        /// </summary>
        KukaModel.CarryTask FindCarryTask(string mission_code);

        void FeedbackFinish(string mission_code);

        void FeedbackFail(string mission_code);

        void AppendTaskLog(string mission_code, string log_message);

        void CancelTask(string task_id);

        void RemoveTask(int task_id);

        void AddToQueue(KukaModel.CarryModel start_node, KukaModel.CarryModel goal_node, out string mission_code, bool wait = true,
                    bool is_plan = false);

        void ParseMission(string mission_code, string status_msg, string describe, out string start_area_code, out KukaMissionStep step);

        KukaModel.SimpleCarryTask[] GetQueueArray();

        event Action<bool> OnTimerAlive;

        string GetCallTask(string start_area_code);
    }
}
