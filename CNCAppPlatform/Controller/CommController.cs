using CefSharp.DevTools.CSS;
using Chump_kuka.Dispatchers;
using iCAPS;
using LiveCharts.Definitions.Series;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chump_kuka.Controller
{
    internal class CommController
    {
        private static UdpDispatcher _udp_listener;
        private static bool _is_server = false;
        static CommController()
        {
            
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

                Send(jsonOutput);
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
                
                Send(jsonOutput);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }   
        }

        public static void Init(bool  is_server, IPEndPoint listen_info)
        {
            _is_server = is_server;

            // 初始化，移除所有綁定事件
            KukaParm.RobotStatusChanged -= KukaParm_RobotStatusChanged;
            KukaParm.AreaStatusChanged -= KukaParm_AreaStatusChanged;

            // 重新建立新的 UDP 伺服器實例
            _udp_listener?.Close();
            _udp_listener = new UdpDispatcher(listen_info);

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
                KukaParm.RobotStatusChanged += KukaParm_RobotStatusChanged;
                _udp_listener.MessageReceived += when_server_MessageReceived;
            }

            KukaParm.AreaStatusChanged += KukaParm_AreaStatusChanged;       // 無論是否為伺服器，都需要更新區域資料
        }

        public static void SayHi()
        {
            MyCommModel comm_model = new MyCommModel()
            {
                Type = "info",
                Message = "Hi"
            };
            string jsonOutput = JsonConvert.SerializeObject(comm_model, Formatting.Indented);

            Send(jsonOutput);
        }

        /// <summary>
        /// 當伺服器接收訊息時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void when_server_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {
            List<KukaAreaModel> areas;

            // 解析回應字串
            MyCommModel response_body = JsonConvert.DeserializeObject<MyCommModel>(e.Message);
            switch (response_body.Type)
            {
                case "info":
                    //MsgBox.Show("e.Message","Server");
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

        /// <summary>
        /// 當客戶端接收訊息時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void when_client_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {
            // 解析回應字串
            MyCommModel response_body = JsonConvert.DeserializeObject<MyCommModel>(e.Message);
            try
            {
                switch (response_body.Type)
                {
                    case "info":
                        MessageBox.Show(e.Message);
                        break;

                    case "robot":
                        KukaParm.RobotStatusInfos = JsonConvert.DeserializeObject<JArray>(response_body.Message);

                        break;
                    case "area":
                        // 若字串為區域類別，解析資料訊息後，將比較後差異處，更新為接收資料
                        List<KukaAreaModel> areas = JsonConvert.DeserializeObject<List<KukaAreaModel>>(response_body.Message);

                        if (KukaParm.KukaAreaModels.Count == 0)
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

        public static void Send(string msg)
        {
            if (_is_server)
            {
                _udp_listener?.SendToGroup(msg);
            }
            else
            {
                _udp_listener?.SendToServer(msg);
            }
        }
        private class MyCommModel
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }
    }
}
