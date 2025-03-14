using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    internal class KukaApiController
    {
        public static HttpRequest kuka_api_server ;

        // Api 啟用狀態
        public static bool Enable
        {
            get => _enable;
            set
            {
                if (value == _enable) return;
                _enable = value;

                if (_enable)
                {
                    if (!apiTimer.Enabled) apiTimer.Start();
                }
                else
                {
                    if (apiTimer.Enabled) apiTimer.Stop();
                    while (apiQueue.TryDequeue(out _)) { }  // 清空佇列
                }
            }
        }
        private static bool _enable = false;

        private static Timer apiTimer;
        private static ConcurrentQueue<Func<Task>> apiQueue = new ConcurrentQueue<Func<Task>>();

        static KukaApiController() 
        {
            // 設定計時器
            apiTimer = new System.Windows.Forms.Timer();
            apiTimer.Interval = 1000; // 每 1 秒請求一次
            apiTimer.Tick += ProcessNextApiAsync;

            //apiTimer.Start();

            // 加入 API 任務到佇列
            // AppendRobotStatusTask();        // 加入機器人狀態任務查詢

            //apiQueue.Enqueue(() => RequestApiAsync("API2", "https://api.example.com/data2", HandleApi2Response));
            //apiQueue.Enqueue(() => RequestApiAsync("API3", "https://api.example.com/data3", HandleApi3Response));
        }

        /// <summary>
        /// 確認通訊是否正常
        /// </summary>
        public static async Task<bool> CheckConnect()
        {
            // 透過向 /areaQuery 請求，判定是否通訊正常，
            // 此函數執行時會中斷計時器，管理器的所有動作將會失效
            apiTimer.Stop();
            //try
            //{
            //    int response_code = await kuka_api_server.GetResponse("areaQuery");

            //    // 若通訊異常，關閉管理器
            //    if (response_code != 200)
            //    {
            //        Log.Append(kuka_api_server.ResponseMessage, "ERROR", "KUKA API Conn Test");
            //        Enable = false;
            //        return false;
            //    }
            //    apiTimer.Start();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Log.Append($"訪問 KUKA API 失敗: {ex.Message}", "ERROR", "Connect Test");
            //    Enable = false;
            //    return false;
            //}
            await RequestApiAsync("areaQuery", null, HandleAreaResponse);
            var request_body = new
            {
                areaCodes = KukaParm.KukaAreaModels.Select(a => a.AreaCode).ToList()
            };
            await RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse);
            if (KukaParm.KukaAreaModels.Count > 0)
            {
                apiTimer.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 將機器人狀態查詢請求加入 API 等待列表
        /// </summary>
        public static void AppendRobotStatusTask()
        {
            var request_body = new
            {
                robotId = "",
                robotType = "",
                mapCode = "",
                floorNumber = ""
            };
            apiQueue.Enqueue(() => RequestApiAsync("robotQuery", request_body, HandleRobotStatusResponse));
            // Log.Append("已加入 /robotQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將區域狀態查詢請求加入 API 等待列表
        /// </summary>
        public static void AppendAreaTask()
        {
            apiQueue.Enqueue(() => RequestApiAsync("areaQuery", null, HandleAreaResponse));
            Log.Append("已加入 /areaQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將區域節點狀態查詢請求加入 API 等待列表
        /// </summary>
        public static void AppendNodesTask()
        {
            var request_body = new
            {
                areaCodes = KukaParm.KukaAreaModels.Select(a => a.AreaCode).ToList()
            };

            apiQueue.Enqueue(() => RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse));
            Log.Append("已加入 /areaNodesQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將派車任務請求加入 API 等待列表
        /// </summary>
        public static void AppendCarryTask()
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var request_body = new
            {
                orgId = "chump",     //"9001",
                requestId = $"request{timestamp}",
                missionCode = $"mission{timestamp}",
                missionType = "RACK_MOVE",
                viewBoardType = "",
                robotType = "LIFT",
                robotModels = new string[] { },
                robotIds = new string[] {"1"},
                priority = 1,
                containerType = "",
                containerCode = "",
                templateCode = "",
                lockRobotAfterFinish = false,
                unlockRobotId = "",
                unlockMissionCode = "",
                idleNode = "",
                missionData = new[]
                {
                    new
                    {
                        sequence = 1,
                        position = KukaParm.StartNode.Code,     //"A000000002",
                        type = KukaParm.StartNode.Type,     // "NODE_AREA",
                        putDown = false,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    },
                    new
                    {
                        sequence = 2,
                        position = KukaParm.GoalNode.Code,     //"A000000003",
                        type = KukaParm.GoalNode.Type,     // "NODE_AREA",
                        putDown = true,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    }
                }
            };

            apiQueue.Enqueue(() => RequestApiAsync("submitMission", request_body, HandleCarryResponse));
            Log.Append("已加入 /submitMission 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 依序處理 API 請求
        /// </summary>
        private static async void ProcessNextApiAsync(object sender, EventArgs e)
        {            
            // 停止計時器，確保在請求處理中不會再觸發計時器
            apiTimer.Stop();

            while (apiQueue.TryDequeue(out var apiTask))        // 依序取出並執行等待列表中的第一項任務
            {
                await apiTask();
                await Task.Delay(100);
            }

            // AppendRobotStatusTask();        // 機器人狀態查詢為固定行程
            apiTimer.Start();
        }

        /// <summary>
        /// 發送 API 請求
        /// </summary>
        private static async Task RequestApiAsync(string apiName, object requestBody, Action<JObject> handleResponse)
        {
            if (!Enable) return;

            int maxRetries = 3;     // 最大重試次數
            int delayMilliseconds = 500;       // 重試間隔

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    int response_code = requestBody == null     // 如果 requestBody 為 null 表示請求為 GET，反之為 POST
                        ? await kuka_api_server.GetResponse(apiName)
                        : await kuka_api_server.PostRequest(apiName, requestBody);

                    string responseBody = kuka_api_server.ResponseMessage;

                    // 處理 API 回應
                    JObject resp_json = JObject.Parse(responseBody);
                    if (!(bool)resp_json["success"])
                    {
                        Log.Append($"訪問 KUKA API 發生異常。 [{(string)resp_json["code"]}] {(string)resp_json["message"]}", "ERROR", $"/{apiName}");
                        return;
                    }

                    handleResponse(resp_json);


                    // 因 robotQuery 會發出太多次請求，屏蔽 Log 紀錄
                    if (apiName != "robotQuery") 
                        Log.Append($"收到來自 /{apiName} 的回應", "INFO", "KukaAPiHandle");

                    return;
                }
                catch (Exception ex)
                {
                    Log.Append($"訪問 KUKA API 失敗 (第 {attempt} 次): {ex}", "ERROR", "KukaAPiHandle");
                    await Task.Delay(delayMilliseconds);
                }
            }
        }

        /// <summary>
        /// API1 的回應處理
        /// </summary>
        private static void HandleRobotStatusResponse(JObject resp_json)
        {
            JArray robot_infos = (JArray)resp_json["data"];

            //JObject updete_time = new JObject();
            for (int index = 0; index < robot_infos.Count; index++)
            {
                robot_infos[index]["updateTime"] = DateTime.Now.ToString(@"G");
            }
            //robot_infos.Add(updete_time);
            
            KukaParm.RobotStatusInfos = robot_infos;
            AppendRobotStatusTask();        // 機器人狀態查詢為固定行程
        }

        private static void HandleAreaResponse(JObject resp_json)
        {
            KukaParm.KukaAreaModels = resp_json["data"].ToObject<List<KukaAreaModel>>();

            // 加入節點查詢
            AppendNodesTask();
        }

        private static void HandleNodesResponse(JObject resp_json)
        {
            var nodeData = resp_json["data"].ToObject<List<dynamic>>();

            // List<KukaAreaModel> _kuka_areas = KukaParm.KukaAreaModels.Select(area => (KukaAreaModel)area.Clone()).ToList();

            // 將第二個 JSON 的 nodeList 合併進 areas
            foreach (var area in KukaParm.KukaAreaModels)
            {
                // 根據 areaCode 尋找匹配的 nodeList
                var matchingNode = nodeData.FirstOrDefault(x => x.areaCode == area.AreaCode);
                if (matchingNode != null)
                {
                    area.NodeList = matchingNode.nodeList.ToObject<List<string>>();
                }
            }
            // KukaParm.KukaAreaModels = _kuka_areas;
        }

        private static void HandleCarryResponse(JObject resp_json)
        {
            Log.Append($"已成功派發任務", "Info", "");
        }
    }
}
