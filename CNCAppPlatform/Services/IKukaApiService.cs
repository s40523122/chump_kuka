using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka.Services
{
    public interface IKukaApiService
    {
        //void PubCarryError(string area_code);

        void PubCarryTask(KukaModel.CarryTask carry_task);

        void PubCarryCancel(string mission_code);

        Task<bool> ConnectAndCheck(string url);

        void GetRobotStatus();
    }
}

