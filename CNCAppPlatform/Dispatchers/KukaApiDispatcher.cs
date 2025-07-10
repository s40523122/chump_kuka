using CefSharp.DevTools.CSS;
using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka.Dispatchers
{
    internal class KukaApiDispatcher
    {
        private bool _enable = false;
        private HttpRequest _kuka_api_server;
        private System.Windows.Forms.Timer _api_timer;
        private ConcurrentQueue<Func<Task>> _api_queue = new ConcurrentQueue<Func<Task>>();

        public KukaApiDispatcher(string url)
        {
            _kuka_api_server = new HttpRequest(url, 5);

            // 設定計時器
            _api_timer = new System.Windows.Forms.Timer();
            _api_timer.Interval = 1000; // 每 1 秒請求一次
            _api_timer.Tick += ProcessNextApiAsync;
        }

        public async Task<bool> Start()
        {
            //if (value == _enable) return;
            //_enable = value;

            if (_enable) 
                return true;
            
            _enable = true;

            bool conn = _enable = await CheckConnect();
            
             if (!conn) 
                return false;

            _api_timer.Start();
            

            return true;
            //else
            //{
            //    if (_api_timer.Enabled) _api_timer.Stop();
            //    while (_api_queue.TryDequeue(out _)) { }  // 清空佇列
            //}
        }

        /// <summary>
        /// 確認通訊是否正常
        /// </summary>
        private async Task<bool> CheckConnect()
        {
            // 透過向 /areaQuery 請求，判定是否通訊正常
            
            await RequestApiAsync("areaQuery", null, HandleAreaResponse);
            if(KukaParm.KukaOriginAreaModels?.Count == 0) return false;

            var request_body = new
            {
                areaCodes = KukaParm.KukaOriginAreaModels.Select(a => a.AreaCode).ToList()
            };
            await RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse);

            return (KukaParm.KukaOriginAreaModels.Count > 0) ? true : false;

        }

        /// <summary>
        /// 依序處理 API 請求
        /// </summary>
        private async void ProcessNextApiAsync(object sender, EventArgs e)
        {
            // 停止計時器，確保在請求處理中不會再觸發計時器
            _api_timer.Stop();

            while (_api_queue.TryDequeue(out var apiTask))        // 依序取出並執行等待列表中的第一項任務
            {
                await apiTask();
                await Task.Delay(100);
            }

            // AppendRobotStatusTask();        // 機器人狀態查詢為固定行程
            _api_timer.Start();
        }

        /// <summary>
        /// 發送 API 請求
        /// </summary>
        private async Task RequestApiAsync(string apiName, object requestBody, Action<JObject> handleResponse)
        {
            if (!_enable) return;

            int maxRetries = 3;     // 最大重試次數
            int delayMilliseconds = 500;       // 重試間隔

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    int response_code = requestBody == null     // 如果 requestBody 為 null 表示請求為 GET，反之為 POST
                        ? await _kuka_api_server.GetResponse(apiName)
                        : await _kuka_api_server.PostRequest(apiName, requestBody);

                    string responseBody = _kuka_api_server.ResponseMessage;

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
                    Log.Append($"{handleResponse.Method.Name} 訪問 KUKA API 失敗 (第 {attempt} 次)", "ERROR", "KukaAPiHandle");
                    await Task.Delay(delayMilliseconds);
                }
            }
        }

        /// <summary>
        /// 將機器人狀態查詢請求加入 API 等待列表
        /// </summary>
        public void AppendRobotStatusTask()
        {
            var request_body = new
            {
                robotId = "",
                robotType = "",
                mapCode = "",
                floorNumber = ""
            };
            _api_queue.Enqueue(() => RequestApiAsync("robotQuery", request_body, HandleRobotStatusResponse));
            // Log.Append("已加入 /robotQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將區域狀態查詢請求加入 API 等待列表
        /// </summary>
        public void AppendAreaTask()
        {
            _api_queue.Enqueue(() => RequestApiAsync("areaQuery", null, HandleAreaResponse));
            Log.Append("已加入 /areaQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將區域節點狀態查詢請求加入 API 等待列表
        /// </summary>
        public void AppendNodesTask()
        {
            var request_body = new
            {
                areaCodes = KukaParm.KukaOriginAreaModels.Select(a => a.AreaCode).ToList()
            };

            _api_queue.Enqueue(() => RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse));
            Log.Append("已加入 /areaNodesQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 將派車任務請求加入 API 等待列表
        /// </summary>
        public void AppendCarryTask(CarryNode[] carry_nodes)
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            List<dynamic> mission_data = new List<dynamic>();
            bool put_down = true;      // 表示貨架是否放下
            foreach (CarryNode node in carry_nodes)
            {
                put_down = !put_down;       // 切換放下/頂升
                mission_data.Add(new
                {
                    sequence = 1,
                    position = node.Code,     //"A000000002",
                    type = node.Type,     // "NODE_AREA",
                    putDown = put_down,
                    passStrategy = "AUTO",
                    waitingMillis = 0
                });
            }

            var request_body = new
            {
                orgId = "chump",     //"9001",
                requestId = $"request{timestamp}",
                missionCode = $"mission{timestamp}",
                missionType = "RACK_MOVE",
                viewBoardType = "",
                robotType = "LIFT",
                robotModels = new string[] { },
                robotIds = new string[] {},
                priority = 1,
                containerType = "",
                containerCode = "",
                templateCode = "",
                lockRobotAfterFinish = false,
                unlockRobotId = "",
                unlockMissionCode = "",
                idleNode = "",
                missionData = mission_data.ToArray(),
            };

            _api_queue.Enqueue(() => RequestApiAsync("submitMission", request_body, HandleCarryResponse));
            Log.Append("已加入 /submitMission 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// API1 的回應處理
        /// </summary>
        private void HandleRobotStatusResponse(JObject resp_json)
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

        private void HandleAreaResponse(JObject resp_json)
        {
            KukaParm.KukaOriginAreaModels = resp_json["data"].ToObject<List<KukaAreaModel>>();

            // 加入節點查詢
            //AppendNodesTask();
        }

        private void HandleNodesResponse(JObject resp_json)
        {
            var nodeData = resp_json["data"].ToObject<List<dynamic>>();

            // List<KukaAreaModel> _kuka_areas = KukaParm.KukaAreaModels.Select(area => (KukaAreaModel)area.Clone()).ToList();

            // 將第二個 JSON 的 nodeList 合併進 areas
            foreach (var area in KukaParm.KukaOriginAreaModels)
            {
                // 根據 areaCode 尋找匹配的 nodeList
                var matchingNode = nodeData.FirstOrDefault(x => x.areaCode == area.AreaCode);
                if (matchingNode != null)
                {
                    area.NodeList = matchingNode.nodeList.ToObject<string[]>();
                }
            }
            // KukaParm.KukaAreaModels = _kuka_areas;
        }

        private void HandleCarryResponse(JObject resp_json)
        {
            Log.Append($"已成功派發任務", "Info", "");
        }

    }
}
