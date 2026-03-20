using Chump_kuka.Dispatchers;
using Chump_kuka.Services;
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
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Controller
{
    internal class ChatController : IChatService
    {
        private static MqttDispatcher _mqtt;
        private static bool _is_master = false;
        private IKukaService _kuka_service;
        private IFeedbackService _feedback_service;



        public ChatController(IKukaService kuka_service)
        {
            _kuka_service = kuka_service;
        }

        public async Task<bool> Init(bool is_server, IPEndPoint listen_server_info)
        {
            _is_master = is_server;

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
                _mqtt.Subscriber("cancel_task", CancelTaskCb);
                _mqtt.Subscriber("update_task", UpdateTaskCb);
                _mqtt.Subscriber("lock_request", LockCb);

                EventBus.RobotStatusChanged += KukaParm_RobotStatusChanged;          // 伺服器機器人資訊更新時，發佈到客戶端
                EventBus.MissionStepChanged += HttpListenerDispatcher_Heard;
            }

            _mqtt.Subscriber("log", LogCb);
            _mqtt.Subscriber("hello", HelloCb);
            _mqtt.Subscriber("robot", RobotCb, 0);
            _mqtt.Subscriber("area", AreaCb);
            _mqtt.Subscriber("area/rack_status", RackCb);
            _mqtt.Subscriber("area/node_status", NodesCb);
            //_mqtt.Subscriber("carry/finish", CarryFinishCb);
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
            // MsgBox.Show(message, "錯誤");
            Log.Append(message, "ERROR", "ChatController");
        }

        private static void HelloCb(string message)
        {
            if (!_is_master)        // 非伺服端不要傳遞訊息
                return;

            try
            {
                Log.Append("接收客戶端訊號...", "INFO", "ChatController");
                string jsonOutput = JsonConvert.SerializeObject(KukaParm.GetAreaArray(), Formatting.Indented);

                _mqtt.Publisher("area", jsonOutput);
                Log.Append("回應當前區域狀態", "INFO", "ChatController");
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON 序列化失敗：" + ex.Message, "錯誤");
            }
        }

        private static void HeardCb(string message)
        {
            HeardEventArgs data = JsonConvert.DeserializeObject<HeardEventArgs>(message);
            EventBus.PublishMissionStepChanged(data);
        }

        private static void RobotCb(string message)
        {
            KukaParm.RobotStatusInfos = JsonConvert.DeserializeObject<KukaModel.RobotInfo[]>(message);
        }

        private static void AreaCb(string message)
        {
            // 若字串為區域類別，解析資料訊息後，將比較後差異處，更新為接收資料

            List<KukaModel.Area> areas = JsonConvert.DeserializeObject<List<KukaModel.Area>>(message);

            // 如果接收列表資訊與當前不同，更新當前列表
            //if (KukaParm.KukaAreaModels.Count == 0)
            //    KukaParm.KukaAreaModels = areas;
            string[] origin_code_array = KukaParm.GetAreaArray().Select(m => m.AreaCode).ToArray();       // 將所有代號取出為陣列
            string[] source_code_array = areas.Select(m => m.AreaCode).ToArray();       // 將所有代號取出為陣列
            if (origin_code_array != source_code_array)     // 只判定區域代號是否修正
            {
                KukaParm.UpdateAreaModels(areas);
                //foreach (KukaModel.Area source_area in areas)
                //{
                //    var base_model = KukaParm.KukaAreaModels.FirstOrDefault(b => b.AreaName == source_area.AreaName);
                //    base_model.CompareAndUpdate(source_area);
                //}
            }
        }

        private static void RackCb(string message)
        {
            KukaModel.Area receive_area = JsonConvert.DeserializeObject<KukaModel.Area>(message);

            // 找到區域模型並修改節點貨架狀態
            KukaModel.Area find_area = KukaParm.GetAreaModel(receive_area.AreaCode);
            if (find_area != null)
            {
                // find_area.NodeStatus = receive_area.NodeStatus;
                for (int i = 0; i < receive_area.NodeList.Length; i++)
                {
                    find_area.NodeList[i].RackStatus = receive_area.NodeList[i].RackStatus;
                }
            }
            else
            {
                Log.DebugInfo($"收到貨架更新訊息但找不到區域[{receive_area.AreaCode}]");
            }
        }

        private static void NodesCb(string message)
        {
            KukaModel.Area receive_area = JsonConvert.DeserializeObject<KukaModel.Area>(message);

            KukaModel.Area find_area = KukaParm.GetAreaModel(receive_area.AreaCode);
            if (find_area != null)
            {
                for (int i = 0; i < receive_area.NodeList.Length; i++)
                {
                    find_area.NodeList[i].IsLock = receive_area.NodeList[i].IsLock;
                    find_area.NodeList[i].NodeStatus = receive_area.NodeList[i].NodeStatus;
                }
            }
        }

        private static void LockCb(string message)
        {
            KukaModel.Area receive_area = JsonConvert.DeserializeObject<KukaModel.Area>(message);

            KukaModel.Area find_area = KukaParm.GetAreaModel(receive_area.AreaCode);
            if (find_area != null)
            {
                for (int i = 0; i < receive_area.NodeList.Length; i++)
                {
                    find_area.NodeList[i].IsLock = receive_area.NodeList[i].IsLock;
                }
            }
            
            NodesCb(message);
            SyncNodeStatus1(find_area);
        }

        private void FeedCb(string message)
        {
            if (_is_master)
            {
                //Log.Append("接收到報工任務", "INFO", "ChatController");
                PubLog("接收到報工任務");
                SendFeedbackInfo(message);
            }
        }

        private void CarryCb(string message)
        {
            try
            {
                PubLog("接收排程搬運任務");

                KukaModel.CarryModel[] nodes = JsonConvert.DeserializeObject<KukaModel.CarryModel[]>(message);
                AppendCarryTask(nodes[0], nodes[1], true);
            }
            catch (Exception _e)
            {
                PubLog("錯誤: 接收排程搬運任務" + _e.ToString());
            }
        }
        private void CarryAutoCb(string message)
        {
            try
            {
                PubLog("接收基本搬運任務");
                KukaModel.CarryModel[] nodes = JsonConvert.DeserializeObject<KukaModel.CarryModel[]>(message);
                AppendCarryTask(nodes[0], nodes[1], false);
            }
            catch (Exception _e)
            {
                PubLog("錯誤: 接收基本搬運任務" + _e.ToString());
            }
        }

        private static void CarryListCb(string message)
        {
            KukaModel.SimpleCarryTask[] tasks = JsonConvert.DeserializeObject<List<KukaModel.SimpleCarryTask>>(message).ToArray();
            EventBus.PublishCarryTaskUpdated(tasks);
        }

        private void DelTaskCb(string message)
        {
            Log.Append($"已接收刪除任務[{message}]", "CHAT", "ChatController");
            int.TryParse(message, out int task_id);
            _kuka_service.RemoveTask(task_id);
        }

        private void CancelTaskCb(string message)
        {
            Log.Append($"已接收強制取消任務[{message}]", "CHAT", "ChatController");
            _kuka_service.CancelTask(message);
        }

        private void UpdateTaskCb(string message)
        {
            SyncCarryTask(_kuka_service.GetQueueArray());
        }

        /// <summary>
        /// 監聽 http 訊息後，觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpListenerDispatcher_Heard(HeardEventArgs e)
        {
            if (_is_master)
            {
                // 同步至所有從機中
                string message = JsonConvert.SerializeObject(e, Formatting.Indented);

                _mqtt.Publisher("heard", message);
            }
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
            Log.Append("呼叫伺服器，取得區域狀態", "INFO", "ChatController");
            _mqtt.Publisher("hello", "Hi");
        }       

        /// <summary>
        /// 所有主/從站同步搬運任務
        /// </summary>
        public static void SyncCarryTask(KukaModel.SimpleCarryTask[] tasks)
        {
            EventBus.PublishCarryTaskUpdated(tasks);

            string task_list_json = JsonConvert.SerializeObject(tasks, Formatting.Indented);

            _mqtt?.Publisher("carry/list", task_list_json);
        }

        /*/// <summary>
        /// 所有主/從站同步所有節點狀態
        /// </summary>
        public static void SyncNodeStatus(KukaModel.Area update_model)
        {
            string nodes_json = JsonConvert.SerializeObject(update_model, Formatting.Indented);

            _mqtt.Publisher("area/nodes", nodes_json);
        }*/

        /// <summary>
        /// 同步貨架狀態
        /// </summary>
        /// <param name="area_rack">字串陣列，第一字串為區域代號</param>
        public static void SyncRackStatus(KukaModel.Area area)
        {
            string nodes_json = JsonConvert.SerializeObject(area, Formatting.Indented);

            _mqtt.Publisher($"area/rack_status", nodes_json);
        }

        /// <summary>
        /// 更新節點狀態(僅server)
        /// </summary>
        /// <param name="area_rack">字串陣列，第一字串為區域代號</param>
        public static void SyncNodeStatus1(KukaModel.Area area)
        {
            if (!_is_master)        // 非伺服端不要傳遞訊息
                return;

            string nodes_json = JsonConvert.SerializeObject(area, Formatting.Indented);

            _mqtt.Publisher($"area/node_status", nodes_json);
        }

        /// <summary>
        /// 請求鎖定節點
        /// </summary>
        /// <param name="area_code"></param>
        public static void RequestLockNode(KukaModel.Area request_area)
        {
            string area_json = JsonConvert.SerializeObject(request_area, Formatting.Indented);

            if (_is_master)
            {
                LockCb(area_json);
            }
            else
            {
                _mqtt.Publisher("lock_request", area_json);
            }
        }

        

        public void SendFeedbackInfo(string feedback_msg)
        {
            if (_is_master)
            {
                // _feedback_service.SendToRecordSystem(feedback_msg);
                Log.Append($"發送報工訊息{feedback_msg}", "INFO", "ChatController");
            }
            else
            {
                // Send("feedback", feedback_msg);
                _mqtt.Publisher("feedback", feedback_msg);
            }
        }

        public void AppendCarryTask(KukaModel.CarryModel start_carry_info, KukaModel.CarryModel goal_carry_info, bool wait=true)
        {
            if (_is_master)        
            {
                // 若為 master 端，將任務加入等候區
                _kuka_service.AddToQueue(start_carry_info, goal_carry_info, out _, wait);
            }
            else
            {
                // 若為 slave 端，傳送節點資訊，讓伺服器處理
                // 若 wait = true，透過 "carry" 主題傳遞資料，代表需要等待叫車訊號。
                string topic_name = wait ? "carry" : "carry/auto";

                KukaModel.CarryModel[] nodes = new KukaModel.CarryModel[2] { start_carry_info, goal_carry_info };
                string task_node_string = JsonConvert.SerializeObject(nodes, Formatting.Indented);

                _mqtt.Publisher(topic_name, task_node_string);
            }
        }

        public void DelTask(string task_id)
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

        public void UpdateTaskList()
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

        public void CancelTask(string task_id)
        {
            if (_is_master)
            {
                CancelTaskCb(task_id);
            }
            else
            {
                _mqtt.Publisher("cancel_task", task_id);
            }
            
        }
    }
}
