using Chump_kuka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka.Controller
{
    public class ModulePlugIn
    {
        public static IKukaService CarryTaskModule;
        public static IFeedbackService FeedbackModule;
        public static IKukaApiService KukaApiModule;
        public static IChatService ChatModule;


        static ModulePlugIn()
        {
            KukaApiModule = new KukaApiController();
            CarryTaskModule = new CarryTaskController(KukaApiModule);
            ChatModule = new ChatController(CarryTaskModule);
            FeedbackModule = new FeedbackController(CarryTaskModule, ChatModule);
        }

    }
}
