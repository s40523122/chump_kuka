﻿using CookComputing.XmlRpc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Chump_kuka.Services
{
    internal class KukaApiHandle
    {
        // Api 啟用狀態
        public static bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if (value == _enable) return;

                if (value) apiTimer.Start();
                else apiTimer.Stop();
            }
        }
        private static bool _enable = false;

        private static Timer apiTimer;
        private static Queue<Func<Task>> apiQueue = new Queue<Func<Task>>(); // API 任務佇列

        static KukaApiHandle() 
        {
            // 設定計時器
            apiTimer = new System.Windows.Forms.Timer();
            apiTimer.Interval = 1000; // 每 1 秒請求一次
            apiTimer.Tick += ProcessNextApiAsync;

            //apiTimer.Start();

            // 加入 API 任務到佇列
            AppendRobotStatusTask();        // 加入機器人狀態任務查詢
            //apiQueue.Enqueue(() => RequestApiAsync("API2", "https://api.example.com/data2", HandleApi2Response));
            //apiQueue.Enqueue(() => RequestApiAsync("API3", "https://api.example.com/data3", HandleApi3Response));
        }

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
            Log.Append("已加入 /robotQuery 於請求等待列表", "INFO", "KukaAPiHandle");
        }

        /// <summary>
        /// 依序處理 API 請求
        /// </summary>
        private static async void ProcessNextApiAsync(object sender, EventArgs e)
        {            
            // 停止計時器，確保在請求處理中不會再觸發計時器
            apiTimer.Stop();

            while (apiQueue.Count > 0)
            {
                var apiTask = apiQueue.Dequeue();
                await apiTask();
            }

            AppendRobotStatusTask();        // 機器人狀態查詢為固定行程
            apiTimer.Start();
        }

        /// <summary>
        /// 發送 API 請求
        /// </summary>
        private static async Task RequestApiAsync(string apiName, object requestBody, Action<string> handleResponse)
        {
            if (!Enable) return;
            
            try
            {
                int response_code = 0;
                if (requestBody == null) response_code = await Env.kuka_api.GetResponse(apiName); 
                else response_code = await Env.kuka_api.PostRequest(apiName, requestBody);

                string responseBody = Env.kuka_api.ResponseMessage;

                // 處理 API 回應
                handleResponse(responseBody);
            }
            catch (Exception ex)
            {
                Log.Append($"訪問 KUKA API 失敗: {ex.Message}", "ERROR", "KukaAPiHandle");
            }
        }

        /// <summary>
        /// API1 的回應處理
        /// </summary>
        private static void HandleRobotStatusResponse(string responseBody)
        {
            try
            {
                JObject resp_json = JObject.Parse(responseBody);
                if (!(bool)resp_json["success"])
                {
                    Log.Append($"訪問 KUKA API 發生異常。 [{(string)resp_json["code"]}] {(string)resp_json["message"]}", "ERROR", "/robotQuery");
                    return;
                }

                JArray robot_infos = (JArray)resp_json["data"];

                JObject updete_time = new JObject();
                for (int index = 0; index < robot_infos.Count; index++)
                {
                    updete_time[index]["updeteTime"] = robot_infos.Count != 0 ? DateTime.Now.ToString(@"G") : "--";
                }
                robot_infos.Add(updete_time);
                //KukaParm.RobotStatusFeedbackTime = robot_infos.Count != 0 ? DateTime.Now.ToString() : "--";
                KukaParm.RobotStatusInfos = robot_infos;
            }
            catch
            {
                // MessageBox.Show($"KukaRobotStatus控制項嘗試訪問 /areaQuery 時發生異常。{responseBody}", "robot_state error");
                Log.Append($"訪問 KUKA API 發生異常。 [{responseBody}]", "ERROR", "/robotQuery");
            }
        }

        
    }
}
