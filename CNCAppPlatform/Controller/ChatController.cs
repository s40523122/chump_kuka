using CefSharp.DevTools.CSS;
using Chump_kuka.Dispatchers;
using Chump_kuka.Services.Managers;
using iCAPS;
using LiveCharts.Definitions.Series;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Chump_kuka.Controller
{
    internal class ChatController
    {
        private static UdpDispatcher _udp_listener;
        private static bool _is_master = false;

        public static string HostIP = "127.0.0.1";

        public static event EventHandler<HttpListenerDispatcher.HeardEventArgs> StepChanged;
        public static event CarryTasksEventHandler CarryTaskUpdated;
        public static event EventHandler<MessageIPEventArgs> MessageReceived;     // 接收客戶端訊息時觸發的事件

        static ChatController()
        {
            HttpListenerDispatcher.Heard += HttpListenerDispatcher_Heard; ;
        }

        /// <summary>
        /// 監聽 http 訊息後，觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HttpListenerDispatcher_Heard(object sender, HttpListenerDispatcher.HeardEventArgs e)
        {
            // 若監聽目標為綁定區域
            if (e.AreaCode == KukaParm.BindAreaModel.AreaCode)
            {
                PubToLocalController(sender, e);     // 傳送至下一階段
            }
            else
            {
                // 若為其他區域，則傳送至其他模組中
                string message = JsonConvert.SerializeObject(e, Formatting.Indented);

                Send("heard", message);
            }
        }

        private static void PubToLocalController(object sender, HttpListenerDispatcher.HeardEventArgs e)
        {
            switch (e.Step)
            {
                case 1:     // 
                    break;
                case 2:     // 機器人進站
                    LocalAreaController.PubRobotFunc();       // station_agv_star
                    break;
                case 4:     // 機器人出站
                    LocalAreaController.PubRobotOut();      // station_agv_begin
                    break;
                case 5:     // 搬運任務完成
                    LocalAreaController.PubCarryOver();
                    SendCarryFinish(KukaParm.TargetAreaModel.AreaCode);
                    break;
                case 7:
                    break;
            }

            StepChanged?.Invoke(sender, e);     // 傳送至下一階段
        }

        public static void Init(bool  is_server, IPEndPoint listen_info)
        {
            HostIP = Env.LocalIp;
            _is_master = is_server;

            // 初始化，移除所有綁定事件
            KukaParm.RobotStatusChanged -= KukaParm_RobotStatusChanged;
            // KukaParm.AreaStatusChanged -= KukaParm_AreaStatusChanged;

            // 重新建立新的 UDP 伺服器實例
            _udp_listener?.Close();
            _udp_listener = new UdpDispatcher(HostIP, listen_info);

            _udp_listener.MessageReceived += (s, e) => MessageReceived?.Invoke(s, e);

            // 若模組被規劃為伺服器時，加入更新事件
            // 反之，加入 UDP 群組，等待訊息
            if (!is_server)
            {
                _udp_listener?.JoinGroup();
                _udp_listener.MessageReceived += when_client_MessageReceived;
                SayHi();
            }

            else
            {
                KukaParm.RobotStatusChanged += KukaParm_RobotStatusChanged;          // 伺服器機器人資訊更新時，發佈到客戶端
                _udp_listener.MessageReceived += when_server_MessageReceived;
            }

            // KukaParm.AreaStatusChanged += KukaParm_AreaStatusChanged;       // 當貨架狀態更新時，同步所有模組
        }

        private static void KukaParm_RobotStatusChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                // 將訊息封裝成 MyCommModel 後，透過 UDP 傳送
                MyCommModel comm_model = new MyCommModel()
                {
                    Type = "robot",
                    Message = JsonConvert.SerializeObject(KukaParm.RobotStatusInfos, Formatting.Indented)
                };

                string jsonOutput = JsonConvert.SerializeObject(comm_model, Formatting.Indented);

                SyncSend(jsonOutput);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }
        }

        private static void KukaParm_AreaStatusChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                // 將訊息封裝成 MyCommModel 後，透過 UDP 傳送
                MyCommModel comm_model = new MyCommModel()
                {
                    Type = "area",
                    Message = JsonConvert.SerializeObject(KukaParm.KukaAreaModels, Formatting.Indented)
                };

                string jsonOutput = JsonConvert.SerializeObject(comm_model, Formatting.Indented);

                SyncSend(jsonOutput);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }
        }

        public static void SayHi()
        {
            MyCommModel comm_model = new MyCommModel()
            {
                Type = "info",
                Message = "Hi"
            };
            string jsonOutput = JsonConvert.SerializeObject(comm_model, Formatting.Indented);

            SyncSend(jsonOutput);
        }

        /// <summary>
        /// 當伺服器接收訊息時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static async void when_server_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {
            List<KukaAreaModel> areas;

            // 解析回應字串
            MyCommModel response_body = JsonConvert.DeserializeObject<MyCommModel>(e.Message);

            try
            {
                switch (response_body.Type)
                {
                    case "node_status":
                        KukaAreaModel receive_area = JsonConvert.DeserializeObject<KukaAreaModel>(response_body.Message);
                        KukaAreaModel find_area = KukaAreaModel.Find(receive_area.AreaName, KukaParm.KukaAreaModels);
                        find_area.NodeStatus = receive_area.NodeStatus;
                        break;
                    case "feedback":
                        Log.Append("接收到報工任務", "info", "ChatController");
                        SendFeedbackInfo(response_body.Message);
                        break;
                    case "carry":
                        //MsgBox.Show("接收到搬運任務", "Sever");
                        try
                        {
                            Log.Append("接收到搬運任務", "info", "ChatController");
                            ParseAndUpdateCarryNode(response_body.Message);
                            // KukaApiController.PubCarryTask();
                            AppendCarryTask();
                        }
                        catch (Exception _e)
                        {
                            Console.WriteLine(_e.ToString());
                        }
                        break;
                    case "info":
                        //MsgBox.Show("e.Message", "Server");
                        Log.Append("接收call", "info", "ChatController");
                        Console.WriteLine(response_body.Message);
                        KukaParm_AreaStatusChanged(sender, null);
                        break;

                    case "area":
                        // 若字串為區域類別，解析資料訊息後，將比較後差異處，更新為接收資料
                        areas = JsonConvert.DeserializeObject<List<KukaAreaModel>>(response_body.Message);
                        try
                        {
                            foreach (KukaAreaModel source_area in areas)
                            {
                                var base_model = KukaParm.KukaAreaModels.FirstOrDefault(b => b.AreaCode == source_area.AreaCode);
                                base_model.CompareAndUpdate(source_area);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        /// <summary>
        /// 當客戶端接收訊息時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void when_client_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {
            // 解析回應字串
            MyCommModel response_body = JsonConvert.DeserializeObject<MyCommModel>(e.Message);

            //Log.Append("Heard" + response_body.Type, "INFO", "Recrive UDP");
            Console.WriteLine("Heard " + response_body.Type);

            try
            {
                switch (response_body.Type)
                {
                    case "node_status":
                        KukaAreaModel receive_area = JsonConvert.DeserializeObject<KukaAreaModel>(response_body.Message);
                        KukaAreaModel find_area = KukaAreaModel.Find(receive_area.AreaName, KukaParm.KukaAreaModels);
                        find_area.NodeStatus = receive_area.NodeStatus;
                        break;
                    case "task_list":
                        SimpleCarryTask[] tasks = JsonConvert.DeserializeObject<List<SimpleCarryTask>>(response_body.Message).ToArray();
                        CarryTaskUpdated?.Invoke(null, tasks);
                        break;

                    case "carry":
                        ParseAndUpdateCarryNode(response_body.Message);
                        break;
                    case "finish":
                        HttpListenerDispatcher.HeardEventArgs _e = new HttpListenerDispatcher.HeardEventArgs(response_body.Message, 0);
                        StepChanged.Invoke(null, _e);
                        break;
                    case "heard":
                        HttpListenerDispatcher.HeardEventArgs data = JsonConvert.DeserializeObject<HttpListenerDispatcher.HeardEventArgs>(response_body.Message);
                        if (data.AreaCode == KukaParm.BindAreaModel.AreaCode)
                        {
                            PubToLocalController(sender, data);     // 傳送至下一階段
                        }
                        break;
                    case "info":
                        // MessageBox.Show(e.Message);
                        Log.Append("接收call", "info", "ChatController");
                        break;

                    case "robot":
                        KukaParm.RobotStatusInfos = JsonConvert.DeserializeObject<JArray>(response_body.Message);

                        break;
                    case "area":
                        // 若字串為區域類別，解析資料訊息後，將比較後差異處，更新為接收資料
                        List<KukaAreaModel> areas = JsonConvert.DeserializeObject<List<KukaAreaModel>>(response_body.Message);

                        // 如果接收列表資訊與當前不同，更新當前列表
                        //if (KukaParm.KukaAreaModels.Count == 0)
                        //    KukaParm.KukaAreaModels = areas;
                        string[] origin_code_array = KukaParm.KukaAreaModels.Select(m => m.AreaCode).ToArray();       // 將所有代碼取出為陣列
                        string[] source_code_array = areas.Select(m => m.AreaCode).ToArray();       // 將所有代碼取出為陣列
                        if (origin_code_array != source_code_array)
                            KukaParm.KukaAreaModels = areas;

                        foreach (KukaAreaModel source_area in areas)
                        {
                            var base_model = KukaParm.KukaAreaModels.FirstOrDefault(b => b.AreaName == source_area.AreaName);
                            base_model.CompareAndUpdate(source_area);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 所有主/從站同步搬運節點
        /// </summary>
        public static void SyncCarryNode()
        {
            CarryNode[] nodes = new CarryNode[2]
            {
                KukaParm.StartNode,
                KukaParm.GoalNode
            };

            MyCommModel model = new MyCommModel()
            {
                Type = "carry",
                Message = JsonConvert.SerializeObject(nodes, Formatting.Indented)
            };

            SyncSend(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        /// <summary>
        /// 所有主/從站同步搬運任務
        /// </summary>
        public static void SyncCarryTask(SimpleCarryTask[] tasks)
        {
            CarryTaskUpdated?.Invoke(null, tasks);

            MyCommModel model = new MyCommModel()
            {
                Type = "task_list",
                Message = JsonConvert.SerializeObject(tasks, Formatting.Indented)
            };

            SyncSend(JsonConvert.SerializeObject(model, Formatting.Indented));

        }

        /// <summary>
        /// 所有主/從站同步所有節點狀態
        /// </summary>
        public static void SyncNodeStatus()
        {
            MyCommModel model = new MyCommModel()
            {
                Type = "node_status",
                Message = JsonConvert.SerializeObject(KukaParm.BindAreaModel, Formatting.Indented)
            };
            SyncSend(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public static void SyncSend(string msg)
        {
            try
            {
                if (_is_master)
                {
                    _udp_listener?.SendToGroup(msg);
                }
                else
                {
                    _udp_listener?.SendToServer(msg);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        /// <summary>
        /// 當完成搬運任務時，通知目標區域更新狀態
        /// </summary>
        /// <param name="area_code"></param>
        public static void SendCarryFinish(string area_code)
        {
            // 如果目標是當前模組，直接觸發步驟 0
            if (area_code == KukaParm.BindAreaModel.AreaCode)
            {
                HttpListenerDispatcher.HeardEventArgs _e = new HttpListenerDispatcher.HeardEventArgs(area_code, 0);
                StepChanged.Invoke(null, _e);
            }
            else
            {
                Send("finish", area_code);
            }
            
        }

        public static void SendFeedbackInfo(string feedback_msg)
        {
            if (_is_master)
            {
                FeedbackDispatcher.SendToRecordSystem(feedback_msg);
            }
            else
            {
                Send("feedback", feedback_msg);
            }
        }

        private static void Send(string type, string msg)
        {
            // 將訊息封裝成 MyCommModel 後，透過 UDP 傳送
            MyCommModel comm_model = new MyCommModel()
            {
                Type = type,
                Message = msg
            };

            string jsonOutput = JsonConvert.SerializeObject(comm_model, Formatting.Indented);

            SyncSend(jsonOutput);
        }

        public static void AppendCarryTask(bool wait=true)
        {
            if (_is_master)        
            {
                // 若為 master 端，將任務加入等候區
                CarryTaskController.AddToQueue(wait);

            }
            else
            {
                // 若為 slave 端，傳送節點資訊，讓伺服器處理
                SyncCarryNode();
            }
        }

        private static void ParseAndUpdateCarryNode(string carry_node_msg)
        {
            List<CarryNode> nodes = JsonConvert.DeserializeObject<List<CarryNode>>(carry_node_msg);
            KukaParm.StartNode = nodes[0];
            KukaParm.GoalNode = nodes[1];

        }

        private class MyCommModel
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }
    }
}
