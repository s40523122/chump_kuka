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
        public async static Task<bool> StartListenKuka(string url)
        {
            return await HttpListenerDispatcher.StartKukaListener(url);
        }
    }

    

    class KukaTask
    {
        int ID { get; set; }
        string StartNode { get; set; }
        string EndNode { get; set; }
        DateTime CreateTime { get; set; }

        DateTime FinishTime { get; set; }
    }
}
