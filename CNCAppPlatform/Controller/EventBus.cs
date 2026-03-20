using CefSharp.DevTools.Accessibility;
using Chump_kuka.Dispatchers;
using Chump_kuka.Models;
using Chump_kuka.Models.Msgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Markup;
using static Chump_kuka.Dispatchers.HttpListenerDispatcher;

namespace Chump_kuka.Controller
{
    internal class EventBus
    {
        public static event Action<LogMsg> LogAppended;
        public static event Action<HeardEventArgs> MissionStepChanged;       // HttpListener 接收 KMRES 資訊事件
        public static event Action<TextEventArgs> FeedbackCalled;       // 報工系統通知事件
        public static event PropertyChangedEventHandler RobotStatusChanged;     // KUKA機器人資訊更新事件
        public static event Action<KukaModel.SimpleCarryTask[]> CarryTaskUpdated;       // 搬運清單更新事件
        public static event Action<HeardEventArgs> StepChanged;
        public static event Action<MissionStatusMsg> ApiFailed;


        public static void PublishLogMsg(LogMsg data)
        {
            LogAppended?.Invoke(data);
        }


        /// <summary>
        /// 發布 HttpListener 接收的 KMRES 資訊事件
        /// </summary>
        /// <param name="data"></param>
        public static void PublishMissionStepChanged(HeardEventArgs data)
        {
            MissionStepChanged?.Invoke(data);
        }

        public static void PublishFeedbackCalled(TextEventArgs data)
        {
            FeedbackCalled?.Invoke(data);
        }

        public static void PublishApiFailed(MissionStatusMsg data)
        {
            ApiFailed?.Invoke(data);
        }

        public static void PublishRobotStatusChanged(string propertyName)
        {
            RobotStatusChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static void PublishCarryTaskUpdated(KukaModel.SimpleCarryTask[] data)
        {
            CarryTaskUpdated?.Invoke(data);
        }

        /// <summary>
        /// 發布 HttpListener 接收的 KMRES 資訊事件
        /// </summary>
        /// <param name="data"></param>
        public static void PublishStepChanged(HeardEventArgs data)
        {
            StepChanged?.Invoke(data);
        }

        private void Log_LogAppended(object sender, LogMsg e)
        {
            switch (e.GetMethod())
            {
                case "sync":
                    break;

                default:
                    Log.ErrorInfo($"無法映射Log方法[\"{e.GetMethod()}\"]");
                    break;
            }
        }
    }
}
