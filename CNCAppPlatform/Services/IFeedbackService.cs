using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka.Services
{
    public interface IFeedbackService
    {
        void Init(bool is_server);

        void PubCarryError(string area_code);

        Task<bool> StartFeedbackServer(int listen_port);

        Task<bool> StartListenKmResResponse(string url);

        void AreaReadyFunc();

        void SendToRecordSystem(string msg);

        void ReceiveAutoTaskCall(string mission_code, string start_area_code);
    }
}
