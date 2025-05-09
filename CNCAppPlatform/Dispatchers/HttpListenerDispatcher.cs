﻿using iCAPS.Managers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using Chump_kuka.Services.Managers;

namespace Chump_kuka.Dispatchers
{
    internal class HttpListenerDispatcher
    {
        private static HttpListenerManager _kuka_listener;
        private static int _area_step = 0;

        public static event EventHandler<HeardEventArgs> Heard;

        public static async Task<bool> StartKukaListener(string url)
        {
            if (_kuka_listener != null && _kuka_listener.IsRunning) return true;

            _kuka_listener = new HttpListenerManager(url);
            _kuka_listener.Start();

            await _kuka_listener.WaitForServerToStartAsync();
            _kuka_listener.MessageReceived += _kuka_listener_MessageReceived;

            return _kuka_listener.IsRunning;
        }

        private static void CalcAreaStep(string task_status)
        {
            switch (_area_step)
            {
                case 0:
                    if (task_status == "MOVE_BEGIN")
                    {
                        area_code = "";
                        _area_step += 1;
                        Log.Append("MOVE_BEGIN", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("Start -> " + task_status);
                    break;
                case 1:
                    if (task_status == "ARRIVE")
                    {
                        _area_step += 1;
                        Log.Append("ARRIVED", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("MOVE_BEGIN1 -> " + task_status);
                    break;
                case 2:
                    if (task_status == "UP_CONTAINER")
                    {
                        _area_step += 1;
                        Log.Append("UP_CONTAINER", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("ARRIVE1 -> " + task_status);
                    break;
                case 3:
                    if (task_status == "MOVE_BEGIN")
                    {
                        _area_step += 1;
                        Log.Append("MOVE_BEGIN", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("UP_CONTAINER -> " + task_status);
                    break;
                case 4:
                    if (task_status == "ARRIVE")
                    {
                        _area_step += 1;
                        Log.Append("ARRIVE", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("MOVE_BEGIN2 -> " + task_status);
                    break;
                case 5:
                    if (task_status == "DOWN_CONTAINER")
                    {
                        _area_step += 1;
                        Log.Append("DOWN_CONTAINER", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("ARRIVE2 -> " + task_status);
                    break;
                case 6:
                    if (task_status == "COMPLETED")
                    {
                        _area_step += 1;
                        Log.Append("COMPLETED", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("DOWN_CONTAINER -> " + task_status);
                    break;
                case 7:
                    // 等同於 case 0
                    area_code = "";
                    if (task_status == "MOVE_BEGIN")
                    {
                        _area_step = 1;
                        Log.Append("MOVE_BEGIN", "INFO", "HttpListenerDispatcher");
                    }
                    else
                        MessageBox.Show("Start -> " + task_status);
                    break;
            }
        }

        static string area_code = "";      // 任務起始區域編碼

        private static void _kuka_listener_MessageReceived(object sender, HttpMessageEventArgs e)
        {
            // TODO 當前現在是否於綁定區域
            
            // 將 JSON 解析為 JObject
            JObject jsonObj = JObject.Parse(e.Message);

            // 設定一個映射字典，鍵是原來的值，值是要替換的值
            var valueMapping = new Dictionary<string, string>
                {
                    { "MOVE_BEGIN", "开始移动" },
                    { "ARRIVED", "到达任务节点" },
                    { "UP_CONTAINER", "顶升完成" },
                    { "DOWN_CONTAINER", "放下完成" },
                    { "COMPLETED", "任务完成" },
                    { "CANCELED", "任务取消完成" },
                    { "ERROR", "任务执行报错" }
                };

            // 設定一個鍵名映射字典
            var keyMapping = new Dictionary<string, string>
                {
                    { "missionCode", "作业id " },
                    { "viewBoardType", "作业类型 " },
                    { "containerCode", "容器编号" },
                    { "currentPosition", "容器当前位置 " },
                    { "slotCode", "当前所在槽位" },
                    { "robotId", "执行当前任务的机器人id " },
                    { "missionStatus", "作业当前状态 " },
                    { "message", "说明信息" },
                    { "missionData", "需要上报的定制信息对象" }
                };

            string task_status = jsonObj["missionStatus"].ToString();
            CalcAreaStep(task_status);      // 計算當前步數

            
            
            // 從第2步(到達區域)判斷目前區域編碼
            if (_area_step == 2)
            {
                string current_position = jsonObj["currentPosition"].ToString();
                KukaAreaModel area_model = KukaParm.KukaAreaModels.FirstOrDefault(area => area.NodeList.Contains(current_position));
                area_code = area_model.AreaCode;
            }

            // 觸發接收事件
            if (area_code != "")
            {
                Heard.Invoke(sender, new HeardEventArgs(area_code, _area_step));
            }
        }
        public static void ManualHeardEvent(string area_code,int step)
        {
            Heard.Invoke(null, new HeardEventArgs(area_code, step));
        }

        private static void _kuka_listener_MessageReceived1(object sender, HttpMessageEventArgs e)
        {
            // 將 JSON 解析為 JObject
            JObject jsonObj = JObject.Parse(e.Message);

            // 設定一個映射字典，鍵是原來的值，值是要替換的值
            var valueMapping = new Dictionary<string, string>
                {
                    { "MOVE_BEGIN", "开始移动" },
                    { "ARRIVED", "到达任务节点" },
                    { "UP_CONTAINER", "顶升完成" },
                    { "DOWN_CONTAINER", "放下完成" },
                    { "COMPLETED", "任务完成" },
                    { "CANCELED", "任务取消完成" },
                    { "ERROR", "任务执行报错" }
                };

            // 設定一個鍵名映射字典
            var keyMapping = new Dictionary<string, string>
                {
                    { "missionCode", "作业id " },
                    { "viewBoardType", "作业类型 " },
                    { "containerCode", "容器编号" },
                    { "currentPosition", "容器当前位置 " },
                    { "slotCode", "当前所在槽位" },
                    { "robotId", "执行当前任务的机器人id " },
                    { "missionStatus", "作业当前状态 " },
                    { "message", "说明信息" },
                    { "missionData", "需要上报的定制信息对象" }
                };

            // 使用映射字典進行鍵名與值的轉換
            foreach (var key in keyMapping.Keys)
            {
                if (jsonObj.ContainsKey(key))
                {
                    // 轉換鍵名
                    jsonObj[keyMapping[key]] = jsonObj[key];
                    jsonObj.Remove(key); // 刪除舊的鍵

                    // 如果有值轉換，轉換值
                    if (jsonObj[keyMapping[key]] != null && valueMapping.ContainsKey(jsonObj[keyMapping[key]].ToString()))
                    {
                        jsonObj[keyMapping[key]] = valueMapping[jsonObj[keyMapping[key]].ToString()];
                    }
                }
            }

            // 格式化 JSON 並顯示
            string formattedJson = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            MessageBox.Show(formattedJson, "JSON 格式化顯示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // MessageBox.Show($"收到 POST 請求: {postData}");
        }

        public class HeardEventArgs : EventArgs
        {
            public string AreaCode { get; }
            public int Step { get; }

            public HeardEventArgs(string area_code, int step)
            {
                AreaCode = area_code;
                Step = step;
            }
        }
    }
}
