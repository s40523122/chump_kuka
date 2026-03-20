using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Models.Msgs
{
    public class MissionStatusMsg
    {
        public string MissionCode { get; private set; }
        public string StatusMsg { get; set; }

        public string Describe { get; private set; }

        public MissionStatusMsg(string mission_code, string status_msg, string describe)
        {
            MissionCode = mission_code;
            StatusMsg = status_msg;
            Describe = describe;
        }
    }
}
