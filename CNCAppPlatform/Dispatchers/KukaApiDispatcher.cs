using CefSharp.DevTools.CSS;
using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Chump_kuka.KukaModel;

namespace Chump_kuka.Dispatchers
{
    internal class KukaApiDispatcher
    {
        private bool _enable = false;
        private HttpRequest _kuka_api_server;
        private System.Windows.Forms.Timer _api_timer;
        private ConcurrentQueue<Func<Task>> _api_queue = new ConcurrentQueue<Func<Task>>();

        private List<KukaModel.Area> _raw_areas = null;

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
            if(_raw_areas == null || _raw_areas?.Count == 0) return false;

            var request_body = new
            {
                areaCodes = _raw_areas.Select(a => a.AreaCode).ToList()
            };
            await RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse);
            
            return (_raw_areas?.Count > 0) ? true : false;

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
        private async Task RequestApiAsync(string apiName, dynamic requestBody, Action<JObject> handleResponse)
        {
            if (!_enable) return;


            if (Env.IsDebug)
            {
                JObject sim_response = DebugApiSim(apiName);
                if (sim_response != null)
                {
                    handleResponse(sim_response);
                }
                return;
            }

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
                        string message = $"訪問 KUKA API 發生異常。 [{(string)resp_json["code"]}] {(string)resp_json["message"]}";
                        Log.Append(message, "ERROR", $"/{apiName}");
                        if(apiName == "submitMission")
                        {
                            CarryTaskController.FeedbackFail(requestBody.missionCode);     // 回報任務失敗
                            CarryTaskController.AppendTaskLog(requestBody.missionCode, message);
                        }
                            
                        return;
                    }

                    handleResponse(resp_json);


                    // 因 robotQuery 會發出太多次請求，屏蔽 Log 紀錄
                    if (apiName != "robotQuery")
                        Log.Append($"收到來自 /{apiName} 的回應\n{responseBody}", "KAPI", "KukaAPiHandle");

                    return;
                }
                catch (Exception ex)
                {
                    Log.Append($"{handleResponse.Method.Name} 訪問 KUKA API 失敗 (第 {attempt} 次)", "ERROR", "KukaAPiHandle");
                    await Task.Delay(delayMilliseconds);
                }
            }
        }

        private JObject DebugApiSim(string api_name)
        {
            switch (api_name)
            {
                case "areaQuery":
                    List<KukaModel.Area> area_sim = new List<KukaModel.Area>()
                    {
                        new KukaModel.Area("area001", "備料區", 0, null),
                        new KukaModel.Area("area002", "組裝區", 0, null),
                        new KukaModel.Area("area003", "成品區", 0, null)
                    };
                    for (int area_index = 0; area_index < area_sim.Count; area_index++)
                    {
                        area_sim[area_index].SetIndex(area_sim, area_index);
                    }
                    return new JObject { ["data"] = JArray.FromObject(area_sim) };
                case "areaNodesQuery":
                    List<dynamic> nodes_sim = new List<dynamic>()
                    {
                        new {areaCode = "area001", nodeList = new string[] { "10", "11" } },
                        new {areaCode = "area002", nodeList = new string[] { "20", "21", "22" } },
                        new {areaCode = "area003", nodeList = new string[] { "30", "31", "32", "33", "34" } }
                    };
                    return new JObject { ["data"] = JArray.FromObject(nodes_sim) };
                case "robotQuery":
                    return null;

            }

            return null;
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
            Log.Append("已加入 /areaQuery 於請求等待列表", "KAPI", "KukaAPiHandle");
        }

        /// <summary>
        /// 將區域節點狀態查詢請求加入 API 等待列表
        /// </summary>
        public void AppendNodesTask()
        {
            if (_raw_areas == null)
            {
                Log.Append($"查詢節點資訊前，請先查詢區域資訊", "KAPI", "KukaAPiHandle");
                return;
            }

            var request_body = new
            {
                areaCodes = _raw_areas.Select(a => a.AreaCode).ToList()
            };

            _api_queue.Enqueue(() => RequestApiAsync("areaNodesQuery", request_body, HandleNodesResponse));
            Log.Append("已加入 /areaNodesQuery 於請求等待列表", "KAPI", "KukaAPiHandle");
        }

        /// <summary>
        /// 強制取消派車任務
        /// </summary>
        public void ApplyCarryCancel(string mission_code)
        {
            var request_body = new
            {
                requestId = $"request{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                missionCode = mission_code,
                containerCode = "",
                position = "",
                cancelMode = "FORCE",
                reason = ""
            };

            _api_queue.Enqueue(() => RequestApiAsync("missionCancel", request_body, HandleCarryResponse));

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(request_body);
            Log.Append($"已加入 /missionCancel 於請求等待列表\n{json}", "KAPI", "KukaAPiHandle");
        }

        /// <summary>
        /// 將派車任務請求加入 API 等待列表
        /// </summary>
        public void AppendCarryTask(KukaModel.CarryTask carry_task)
        {
            dynamic[] mission_data = new dynamic[2]
            {
                new
                {
                    sequence = 1,
                    position = carry_task.StartNode.NodeModel.NodeCode,     //"A000000002",
                    type = "NODE_POINT",     // "NODE_AREA",
                    putDown = false,
                    passStrategy = "AUTO",
                    waitingMillis = 0
                },
                new
                {
                    sequence = 2,
                    position = carry_task.GoalNode.NodeModel.NodeCode,     //"A000000002",
                    type = "NODE_POINT",     // "NODE_AREA",
                    putDown = true,
                    passStrategy = "AUTO",
                    waitingMillis = 0
                }
            };

            var request_body = new
            {
                orgId = "chump",     //"9001",
                requestId = $"request{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                missionCode = carry_task.MissionCode,
                missionType = "RACK_MOVE",
                viewBoardType = "",
                robotType = "LIFT",
                robotModels = new string[] { },
                //robotIds = Debugger.IsAttached ? new string[] { "1" } : new string[] { },        // Debug模式下，派發虛擬機器人
                robotIds = new string[] { },
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

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(request_body);
            Log.Append($"已加入 /submitMission 於請求等待列表\n{json}", "KAPI", "KukaAPiHandle");
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
            Log.Append("嘗試處理API獲取區域資料", "KAPI", "KukaAPiHandle");
            _raw_areas = resp_json["data"].ToObject<List<KukaModel.Area>>();

            // 加入節點查詢
            //AppendNodesTask();
        }

        private void HandleNodesResponse(JObject resp_json)
        {
            Log.Append("嘗試處理API獲取節點資料", "KAPI", "KukaAPiHandle");
            var node_data = resp_json["data"].ToObject<List<dynamic>>();

            // List<KukaAreaModel> _kuka_areas = KukaParm.KukaAreaModels.Select(area => (KukaAreaModel)area.Clone()).ToList();

            // 將第二個 JSON 的 nodeList 合併進 areas
            foreach (var area in _raw_areas)
            {
                List<KukaModel.Node> models = new List<KukaModel.Node>();

                // 根據 areaCode 尋找匹配的 nodeList
                var matching_data = node_data.FirstOrDefault(x => x.areaCode == area.AreaCode);
                
                if (matching_data != null)
                {
                    foreach (string node_id in matching_data.nodeList)
                    {
                        models.Add(new KukaModel.Node(node_id));
                    }
                    area.UpdateNodes(models.ToArray());
                }
            }
            // KukaParm.KukaAreaModels = _kuka_areas;

            KukaParm.SetRawAreaModels(_raw_areas);
            Log.Append("成功從API更新區域狀態", "KAPI", "KukaAPiHandle");
        }

        private void HandleCarryResponse(JObject resp_json)
        {
            Log.Append($"已成功派發任務", "KAPI", "");
        }

    }
}
