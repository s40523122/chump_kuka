using Chump_kuka.Dispatchers;
using Chump_kuka.Services;
using iCAPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chump_kuka.Dispatchers.HttpListenerDispatcher;
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Controller
{
    internal class FeedbackController : IFeedbackService
    {
        private HttpListenerDispatcher _kuka_response;
        private FeedbackDispatcher _feedback_comm;
        private IKukaService _kuka_service;
        private IChatService _chat_service;

        public FeedbackController(IKukaService kuka_service, IChatService chat_service)
        {
            _kuka_service = kuka_service;
            _chat_service = chat_service;

            EventBus.FeedbackCalled += FeedbackDispatcher_Called;             // 報工系統呼叫任務事件
        }

        private void FeedbackDispatcher_Called(TextEventArgs e)
        {
            // 接收到叫車命令，尋找可派發任務
            string start_area_code = e.Message;

            string can_carry_mission_code = _kuka_service.GetCallTask(start_area_code);
            if (can_carry_mission_code != null)
            {
                // KukaApiController.PubCarryTask();
                ChatController.PubLog($"接收叫車任務，等待執行。");

                // 觸發接收報工系統 call 事件
                ReceiveAutoTaskCall(can_carry_mission_code, start_area_code);

                //HeardEventArgs args = new HeardEventArgs(can_carry_mission_code, start_area_code, KukaMissionStep.Received);
                //EventBus.PublishMissionStepChanged(args);
            }
            else
            {
                ChatController.PubLog($"接收叫車任務，無可執行搬運任務。(起始節點: {start_area_code})");
            }
        }

        public async Task<bool> StartFeedbackServer(int listen_port)
        {
            _feedback_comm = new FeedbackDispatcher();
            bool is_conn = await _feedback_comm.StartRecordListener(listen_port);
            return is_conn;
        }

        public async Task<bool> StartListenKmResResponse(string url)
        {
            return await _kuka_response.StartKukaListener(url);
        }


        public void Init(bool is_server)
        {
            if (_kuka_response == null)
            {
                _kuka_response = new HttpListenerDispatcher();
                _kuka_response.HeardKMRES += _kuka_response_HeardKMRES;

                EventBus.MissionStepChanged -= EventBus_HttpListenerHeard;       // 防止重複加入事件

                // 僅在確定為伺服器(主機)時，才加入事件
                if (is_server)
                {
                    EventBus.MissionStepChanged += EventBus_HttpListenerHeard;
                }
            }
        }

        public void ReceiveAutoTaskCall(string mission_code, string start_area_code)
        {
            _kuka_response.ManualHeardEvent(mission_code, start_area_code, KukaMissionStep.Received);
        }

        private void _kuka_response_HeardKMRES(Models.Msgs.MissionStatusMsg args)
        {
            _kuka_service.ParseMission(args.MissionCode, args.StatusMsg, args.Describe, out string start_area_code, out KukaMissionStep step);


            if (args.StatusMsg == "CANCELED")
            {
                PubCarryError(start_area_code);     // 通知報工系統任務失敗
            }
            else
            {
                // 觸發接收事件
                EventBus.PublishMissionStepChanged(new HeardEventArgs(args.MissionCode, start_area_code, step));
            }
        }        

        private void EventBus_HttpListenerHeard(HeardEventArgs args)
        {
            if (args.StartAreaCode == KukaParm.BindAreaModel.AreaCode)
            {
                switch (args.Step)
                {
                    case 0:
                        break;
                    case KukaModel.KukaMissionStep.Received:
                        break;
                    case KukaModel.KukaMissionStep.Start:
                        PubReady();     // 回報搬運車進站
                        PubRobotFunc();       // station_agv_star
                        break;
                    case KukaModel.KukaMissionStep.Leaved:
                        PubRobotOut();      // station_agv_begin
                        break;
                    case KukaModel.KukaMissionStep.Goal:
                        PubCarryOver();
                        break;
                    case KukaModel.KukaMissionStep.Complete:
                        _kuka_service.FeedbackFinish(args.MissionCode);
                        //int index = KukaParm.KukaAreaModels.FindIndex(m => m.AreaCode == e.AreaCode);       // 找到起點區域的 index
                        //int next_index = (index+1) % KukaParm.KukaAreaModels.Count;     // 使用「模運算」達到環狀效果
                        // KukaModel.Area heard_area = KukaParm.KukaAreaModels.FirstOrDefault(area => area.AreaCode == e.StartAreaCode);

                        /* 上行錯誤訊息
           HttpListener發生錯誤[System.NullReferenceException: 並未將物件參考設定為物件的執行個體。
           於 Chump_kuka.Controller.ChatController.HttpListenerDispatcher_Heard(Object sender, HeardEventArgs e) 於 C:\Users\11228\OneDrive - 財團法人精密機械研究發展中心\chump_kuka\CNCAppPlatform\Controller\ChatController.cs: 行 295
           於 Chump_kuka.Dispatchers.HttpListenerDispatcher._kuka_listener_MessageReceived(Object sender, HttpMessageEventArgs e) 於 C:\Users\11228\OneDrive - 財團法人精密機械研究發展中心\chump_kuka\CNCAppPlatform\Dispatchers\HttpListenerDispatcher.cs: 行 217
           於 iCAPS.Managers.HttpListenerManager.<HandleClientAsync>d__16.MoveNext() 於 C:\Users\11228\OneDrive - 財團法人精密機械研究發展中心\chump_kuka\CNCAppPlatform\Services\Managers\HttpListenerManager.cs: 行 99
        --- 先前擲回例外狀況之位置中的堆疊追蹤結尾 ---
           於 System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
           於 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
           於 System.Runtime.CompilerServices.TaskAwaiter.GetResult()
           於 iCAPS.Managers.HttpListenerManager.<<Start>b__14_0>d.MoveNext() 於 C:\Users\11228\OneDrive - 財團法人精密機械研究發展中心\chump_kuka\CNCAppPlatform\Services\Managers\HttpListenerManager.cs: 行 63]
                         */

                        break;
                }
            }
        }

        public static int GetStationNo(string area_code = "")
        {
            if (area_code == "")
                area_code = KukaParm.BindAreaModel.AreaCode;

            // int index = KukaParm.KukaAreaModels.FindIndex(m => m.AreaCode == KukaParm.GetAreaModel(area_code).AreaCode);
            int index = KukaParm.GetAreaModel(area_code).Index;
            return index == -1 ? 0 : index + 1;
        }

        public void PubReady()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_ready");
                _chat_service.SendFeedbackInfo(feedback_msgs[1]);
            }
        }

        public void AreaReadyFunc()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                _chat_service.SendFeedbackInfo(feedback_msgs[0]);
            }

        }

        public void PubRobotFunc()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_star");
                _chat_service.SendFeedbackInfo(feedback_msgs[2]);
            }

        }

        public void PubRobotOut()
        {
            int _bind_station_no = GetStationNo();

            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_begin");
                _chat_service.SendFeedbackInfo(feedback_msgs[3]);
            }

        }

        public void PubCarryOver()
        {
            // 頭尾未形成迴圈 (但目前規劃，最後一站搬運到第一站後，無須回報第一站完成，所以不影響)
            int _bind_station_no = GetStationNo() + 1;
            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_end");
                _chat_service.SendFeedbackInfo(feedback_msgs[4]);
            }

        }

        public void PubCarryError(string area_code)
        {
            // 頭尾未形成迴圈
            int _bind_station_no = GetStationNo(area_code) + 1;
            string feedback_string = INiReader.ReadINIFile(Env.LayoutPath, "Control", $"station{_bind_station_no}");
            string[] feedback_msgs = feedback_string.Split(';');

            if (_bind_station_no != 0)
            {
                //ChatController.SendFeedbackInfo($"station{_bind_station_no}_agv_end");
                _chat_service.SendFeedbackInfo(feedback_msgs[5]);
            }

        }

        public async void SendToRecordSystem(string msg)
        {
            // 不論如何，送送訊息給已連接的 client
            //await _server.SendToAllClients(msg);

            await _feedback_comm.SendToRecordSystem(msg);
        }
    }
}
