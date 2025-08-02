using Chump_kuka.Dispatchers;
using iCAPS;
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
    internal class ChatController
    {
        private static MqttDispatcher _mqtt;
        private static bool _is_master = false;

        public static event EventHandler<HttpListenerDispatcher.HeardEventArgs> StepChanged;
        public static event CarryTasksEventHandler CarryTaskUpdated;

        static ChatController()
        {

        }

        public async static Task<bool> Init(bool is_server, IPEndPoint listen_server_info)
        {
            _is_master = is_server;

            // 初始化，移除所有綁定事件
            KukaParm.RobotStatusChanged -= KukaParm_RobotStatusChanged;
            HttpListenerDispatcher.Heard -= HttpListenerDispatcher_Heard;

            // 重新建立新的 MQTT 實例
            if (_mqtt != null)
            {
                await _mqtt?.StopBroker();
                await _mqtt?.CloseClient();
            }

            _mqtt = new MqttDispatcher();

            if (_is_master)
                await _mqtt.StartBroker(listen_server_info.Port);

            bool success = await _mqtt.InitClient(listen_server_info.Address.ToString(), listen_server_info.Port);
            if (!success) 
                return success;
            
            if (_is_master)
            {
                _mqtt.Subscriber("carry", CarryCb);
                _mqtt.Subscriber("carry/auto", CarryAutoCb);
                _mqtt.Subscriber("feedback", FeedCb);
                _mqtt.Subscriber("del_task", DelTaskCb);
                _mqtt.Subscriber("update_task", UpdateTaskCb);

                KukaParm.RobotStatusChanged += KukaParm_RobotStatusChanged;          // 伺服器機器人資訊更新時，發佈到客戶端
                HttpListenerDispatcher.Heard += HttpListenerDispatcher_Heard;
            }

            _mqtt.Subscriber("log", LogCb);
            _mqtt.Subscriber("hello", HelloCb);
            _mqtt.Subscriber("robot", RobotCb, 0);
            _mqtt.Subscriber("area", AreaCb);
            _mqtt.Subscriber("area/nodes", NodesCb);
            _mqtt.Subscriber("carry/finish", CarryFinishCb);
            _mqtt.Subscriber("carry/list", CarryListCb);
            _mqtt.Subscriber("heard", HeardCb);
            _mqtt.Subscriber("log/error", ErrorCb);

            SayHi();        // 初次上線，通知取得區域資料

            return true;
        }

        public static void PubLog(string message)
        {
            Log.Append(message, "ASYNC", "ChatController");
            _mqtt?.Publisher("log", message);
        }

        public static void PubError(string message)
        {
            ErrorCb(message);
            _mqtt.Publisher("log/error", message);
        }

        private static void LogCb(string message)
        {
            Log.Append(message, "ASYNC", "ChatController");
        }

        private static void ErrorCb(string message)
        {
            MsgBox.Show(message, "錯誤");
            Log.Append(message, "ERROR", "ChatController");
        }

        private static void HelloCb(string message)
        {
            Log.Append("接收call", "info", "ChatController");
            
            if (!_is_master)        // 非伺服端不要傳遞訊息
                return;
            try
            {
                string jsonOutput = JsonConvert.SerializeObject(KukaParm.KukaAreaModels, Formatting.Indented);

                _mqtt.Publisher("area", jsonOutput);
                Log.Append("回應call", "info", "ChatController");
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }
        }

        private static void HeardCb(string message)
        {
            HttpListenerDispatcher.HeardEventArgs data = JsonConvert.DeserializeObject<HttpListenerDispatcher.HeardEventArgs>(message);
            if (data.AreaCode == KukaParm.BindAreaModel.AreaCode)
            {
                PubToLocalController(null, data);     // 傳送至下一階段
            }
        }

        private static void RobotCb(string message)
        {
            KukaParm.RobotStatusInfos = JsonConvert.DeserializeObject<JArray>(message);
        }

        private static void AreaCb(string message)
        {
            // 若字串為區域類別，解析資料訊息後，將比較後差異處，更新為接收資料


            #region 版本補丁
            //List<KukaModel.KukaOriginAreaModel> origin_areas = JsonConvert.DeserializeObject<List<KukaModel.KukaOriginAreaModel>>(message);
            //List<KukaModel.Area> areas = new List<KukaModel.Area>();
            //foreach (KukaModel.KukaOriginAreaModel origin in origin_areas)
            //{
            //    List<KukaModel.Node> nodes = new List<KukaModel.Node>();
            //    for (int i=0; i<origin.NodeList.Length; i++)
            //    {
            //        KukaModel.Node new_node = new KukaModel.Node(origin.NodeList[i]);
            //        if (origin.NodeStatus.Length > i) new_node.RackStatus = origin.NodeStatus[i];
            //        nodes.Add(new_node);
            //    }
            
            //    areas.Add(new KukaModel.Area()
            //    {
            //        AreaName = origin.AreaName,
            //        AreaCode = origin.AreaCode,
            //        AreaType = origin.AreaType,
            //        NodeList = nodes.ToArray()
            //    });
            //}

            #endregion 版本補丁

            List<KukaModel.Area> areas = JsonConvert.DeserializeObject<List<KukaModel.Area>>(message);

            // 如果接收列表資訊與當前不同，更新當前列表
            //if (KukaParm.KukaAreaModels.Count == 0)
            //    KukaParm.KukaAreaModels = areas;
            string[] origin_code_array = KukaParm.KukaAreaModels.Select(m => m.AreaCode).ToArray();       // 將所有代碼取出為陣列
            string[] source_code_array = areas.Select(m => m.AreaCode).ToArray();       // 將所有代碼取出為陣列
            if (origin_code_array != source_code_array)     // 只判定區域代碼是否修正
            {
                KukaParm.KukaAreaModels = areas;

                //foreach (KukaModel.Area source_area in areas)
                //{
                //    var base_model = KukaParm.KukaAreaModels.FirstOrDefault(b => b.AreaName == source_area.AreaName);
                //    base_model.CompareAndUpdate(source_area);
                //}
            }
        }

        private static void NodesCb(string message)
        {

            //#region 版本補丁
            //KukaModel.KukaOriginAreaModel origin = JsonConvert.DeserializeObject<KukaModel.KukaOriginAreaModel>(message);
            //List<KukaModel.Node> nodes = new List<KukaModel.Node>();
            //for (int i = 0; i < origin.NodeList.Length; i++)
            //{
            //    nodes.Add(new KukaModel.Node(origin.NodeList[i])
            //    {
            //        RackStatus = origin.NodeStatus[i],
            //    });
            //}

            //KukaModel.Area receive_area = new KukaModel.Area()
            //{
            //    AreaName = origin.AreaName,
            //    AreaCode = origin.AreaCode,
            //    AreaType = origin.AreaType,
            //    NodeList = nodes.ToArray()
            //};

            //return;
            //#endregion 版本補丁
            KukaModel.Area receive_area = JsonConvert.DeserializeObject<KukaModel.Area>(message);


            KukaModel.Area find_area = KukaModel.Area.Find(receive_area.AreaName, KukaParm.KukaAreaModels);
            if (find_area != null)
            {
                // find_area.NodeStatus = receive_area.NodeStatus;
                for (int i = 0; i < receive_area.NodeList.Length; i++)
                {
                    find_area.NodeList[i].RackStatus = receive_area.NodeList[i].RackStatus;
                    find_area.NodeList[i].Lock = receive_area.NodeList[i].Lock;
                    find_area.NodeList[i].NodeStatus = receive_area.NodeList[i].NodeStatus;
                }
                // find_area.LockNodes = receive_area.LockNodes;
            }

        }

        private static void FeedCb(string message)
        {
            if (_is_master)
            {
                //Log.Append("接收到報工任務", "INFO", "ChatController");
                PubLog("接收到報工任務");
                SendFeedbackInfo(message);
            }
        }

        private static void CarryCb(string message)
        {
            try
            {
                PubLog("接收排程搬運任務");
                ParseAndUpdateCarryNode(message, out KukaModel.CarryModel start_carry_node, out KukaModel.CarryModel goal_carry_node);
                // KukaApiController.PubCarryTask();
                AppendCarryTask(start_carry_node, goal_carry_node, true);
            }
            catch (Exception _e)
            {
                PubLog("錯誤: 接收排程搬運任務" + _e.ToString());
            }
        }
        private static void CarryAutoCb(string message)
        {
            try
            {
                PubLog("接收基本搬運任務");
                ParseAndUpdateCarryNode(message, out KukaModel.CarryModel start_carry_node, out KukaModel.CarryModel goal_carry_node);
                // KukaApiController.PubCarryTask();
                AppendCarryTask(start_carry_node, goal_carry_node, false);
            }
            catch (Exception _e)
            {
                PubLog("錯誤: 接收基本搬運任務" + _e.ToString());
            }
        }

        private static void CarryFinishCb(string message)
        {
            // 解析訊息為 [ 任務編號, 區域標號 ]
            List<string> mission_area = JsonConvert.DeserializeObject<List<string>>(message);
            SendCarryFinish(mission_area[0], mission_area[1]);
        }

        private static void CarryListCb(string message)
        {
            KukaModel.SimpleCarryTask[] tasks = JsonConvert.DeserializeObject<List<KukaModel.SimpleCarryTask>>(message).ToArray();
            CarryTaskUpdated?.Invoke(null, tasks);
        }

        private static void DelTaskCb(string message)
        {
            Log.Append($"已接收刪除任務[{message}]", "CHAT", "ChatController");
            CarryTaskController.RemoveTask(message);
        }

        private static void UpdateTaskCb(string message)
        {
            SyncCarryTask(CarryTaskController.GetQueueArray());
        }

        /// <summary>
        /// 監聽 http 訊息後，觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HttpListenerDispatcher_Heard(object sender, HttpListenerDispatcher.HeardEventArgs e)
        {
            string target_area_code;

            // 如果 step 為 7 代表搬運任務已完成
            if (e.Step == 7)
            {
                CarryTaskController.FeedbackFinish(e.MissionCode);
                //int index = KukaParm.KukaAreaModels.FindIndex(m => m.AreaCode == e.AreaCode);       // 找到起點區域的 index
                //int next_index = (index+1) % KukaParm.KukaAreaModels.Count;     // 使用「模運算」達到環狀效果
                KukaModel.Area heard_area = KukaParm.KukaAreaModels.FirstOrDefault(area => area.AreaCode == e.AreaCode);
                SendCarryFinish(e.MissionCode, heard_area.Next().AreaCode);         // 通知目標區域更新(起點區域index+1)
            }

            // 若監聽目標為綁定區域
            if (e.AreaCode == KukaParm.BindAreaModel.AreaCode)
            {
                PubToLocalController(sender, e);     // 傳送至下一階段
                
            }
            else
            {
                // 若為其他區域，則傳送至其他模組中
                string message = JsonConvert.SerializeObject(e, Formatting.Indented);

                // Send("heard", message);
                _mqtt.Publisher("heard", message);
            }

            PubLog($"Area_{e.AreaCode}:in step [{e.Step}]");
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
                    break;
                case 7:
                    break;
            }

            StepChanged?.Invoke(sender, e);     // 通知任務步驟事件更新
        }

        private static void KukaParm_RobotStatusChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                string jsonOutput = JsonConvert.SerializeObject(KukaParm.RobotStatusInfos, Formatting.Indented);

                _mqtt.Publisher("robot", jsonOutput, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }
        }

        public static void SayHi()
        {
            Log.Append("已發送招呼", "INFO", "ChatController");
            _mqtt.Publisher("hello", "HI");
        }       

        /// <summary>
        /// 所有主/從站同步搬運任務
        /// </summary>
        public static void SyncCarryTask(KukaModel.SimpleCarryTask[] tasks)
        {
            CarryTaskUpdated?.Invoke(null, tasks);

            string task_list_json = JsonConvert.SerializeObject(tasks, Formatting.Indented);

            _mqtt?.Publisher("carry/list", task_list_json);
        }

        /// <summary>
        /// 所有主/從站同步所有節點狀態
        /// </summary>
        public static void SyncNodeStatus(KukaModel.Area update_model)
        {
            string nodes_json = JsonConvert.SerializeObject(update_model, Formatting.Indented);
            _mqtt.Publisher("area/nodes", nodes_json);
        }

        /// <summary>
        /// 當完成搬運任務時，通知目標區域更新狀態
        /// </summary>
        /// <param name="area_code"></param>
        public static void SendCarryFinish(string mission_code, string area_code)
        {
            // 如果目標是當前模組，直接觸發步驟 0，通知工作站更新狀態
            if (area_code == KukaParm.BindAreaModel.AreaCode)
            {
                HttpListenerDispatcher.HeardEventArgs _e = new HttpListenerDispatcher.HeardEventArgs(mission_code, area_code, 0);

                StepChanged.Invoke(null, _e);
                Log.Append("Get finish", "INFO", "ChatController");
            }
            else
            {
                if (_is_master)
                {
                    string[] send_info = new string[2] { mission_code, area_code };
                    _mqtt.Publisher("carry/finish", JsonConvert.SerializeObject(send_info));
                }
                    
            }
        }

        public static void SendFeedbackInfo(string feedback_msg)
        {
            if (_is_master)
            {
                FeedbackDispatcher.SendToRecordSystem(feedback_msg);
                Log.Append($"發送報工訊息{feedback_msg}", "INFO", "ChatController");
            }
            else
            {
                // Send("feedback", feedback_msg);
                _mqtt.Publisher("feedback", feedback_msg);
            }
        }

        public static void AppendCarryTask(KukaModel.CarryModel start_node, KukaModel.CarryModel goal_node, bool wait=true)
        {
            if (_is_master)        
            {
                // 若為 master 端，將任務加入等候區
                CarryTaskController.AddToQueue(start_node, goal_node, out _, wait);
            }
            else
            {
                // 若為 slave 端，傳送節點資訊，讓伺服器處理
                // 若 wait = true，透過 "carry" 主題傳遞資料，代表需要等待叫車訊號。
                string topic_name = wait ? "carry" : "carry/auto";

                string[] nodes = new string[2]
                {
                    $"{start_node.Name};{start_node.AreaCode};{start_node.NodeModel.NodeCode}",
                    $"{goal_node.Name};{goal_node.AreaCode};{goal_node.NodeModel?.NodeCode}",
                };

                string task_node_string = JsonConvert.SerializeObject(nodes, Formatting.Indented);

                _mqtt.Publisher(topic_name, task_node_string);
            }
        }

        public static void DelTask(string task_id)
        {
            if (_is_master)
            {
                DelTaskCb(task_id);
            }
            else
            {
                _mqtt?.Publisher("del_task", task_id);
            }
        }

        public static void UpdateTaskList()
        {
            if (_is_master)
            {
                UpdateTaskCb("update_task");
            }
            else
            {
                _mqtt?.Publisher("update_task", "update_task");
            }
        }

        public static void ReSendTask(string task_id)
        {

            _mqtt.Publisher("resend_task", task_id);
        }

        private static void ParseAndUpdateCarryNode(string carry_node_msg, out KukaModel.CarryModel start_carry_node, out KukaModel.CarryModel goal_carry_node)
        {
            List<string> nodes = JsonConvert.DeserializeObject<List<string>>(carry_node_msg);
            
            string[] start_info = nodes[0].Split(';');
            string[] goal_info = nodes[1].Split(';');
            KukaModel.Node start_node = null, goal_node = null;
            foreach (KukaModel.Area area in KukaParm.KukaAreaModels)
            {
                if (start_node == null)
                {
                    start_node = area.GetNode(start_info[2]);
                }
                if (goal_node == null)
                {
                    goal_node = area.GetNode(goal_info[2]);
                }
            }
            start_carry_node = new KukaModel.CarryModel(start_info[0], start_info[1], start_node);
            goal_carry_node = new KukaModel.CarryModel(goal_info[0], goal_info[1], goal_node);
        }
    }
}
