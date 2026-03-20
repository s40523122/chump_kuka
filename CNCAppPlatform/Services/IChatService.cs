using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka.Services
{
    public interface IChatService
    {
        void SendFeedbackInfo(string feedback_msg);

        Task<bool> Init(bool is_server, IPEndPoint listen_server_info);

        void AppendCarryTask(KukaModel.CarryModel start_carry_info, KukaModel.CarryModel goal_carry_info, bool wait = true);

        void UpdateTaskList();

        void DelTask(string task_id);

        void CancelTask(string task_id);

    }
}
