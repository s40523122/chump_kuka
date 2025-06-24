using iCAPS.Managers;
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
using Chump_kuka.Controller;

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
            string status = "INFO";
            string message = "";
            switch (_area_step)
            {
                case 0:
                    if (task_status == "MOVE_BEGIN")
                    {
                        area_code = "";
                        _area_step += 1;
                        message = "接收任務";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[MOVE_BEGIN]，實際狀態[{task_status}]。";
                    }
                    break;
                case 1:
                    if (task_status == "ARRIVE")
                    {
                        _area_step += 1;
                        message = "到達起點";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[ARRIVE]，實際狀態[{task_status}]。";
                    }
                    break;
                case 2:
                    if (task_status == "UP_CONTAINER")
                    {
                        _area_step += 1;
                        message = "頂升貨架";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[UP_CONTAINER]，實際狀態[{task_status}]。";
                    }
                    break;
                case 3:
                    if (task_status == "MOVE_BEGIN")
                    {
                        _area_step += 1;
                        message = "離開起點";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[MOVE_BEGIN]，實際狀態[{task_status}]。";
                    }
                    break;
                case 4:
                    if (task_status == "ARRIVE")
                    {
                        _area_step += 1;
                        message = "到達終點";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[ARRIVE]，實際狀態[{task_status}]。";
                    }
                    break;
                case 5:
                    if (task_status == "DOWN_CONTAINER")
                    {
                        _area_step += 1;
                        message = "放下貨架";
                    }
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[DOWN_CONTAINER]，實際狀態[{task_status}]。";
                    }
                    break;
                case 6:
                    if (task_status == "COMPLETED")
                    {
                        _area_step += 1;
                        message = "完成任務";
                    }
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[COMPLETED]，實際狀態[{task_status}]。";
                    }
                    break;
                case 7:
                    // 等同於 case 0
                    area_code = "";
                    if (task_status == "MOVE_BEGIN")
                    {
                        _area_step = 1;
                        message = "接收任務";
                    }
                    else
                    {
                        status = "ERROR";
                        message = $"欲接收狀態[MOVE_BEGIN]，實際狀態[{task_status}]。";
                    }
                    break;
            }

            Log.Append(message, status, "HttpListenerDispatcher");
            CarryTaskController.AppendTaskLog(message);
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
            
            // 若任務取消
            if (task_status == "CANCELED")
            {
                CarryTaskController.FeedbackFail();     // 回報任務失敗
                return;
            }
            
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
            public string AreaCode { get; set; }
            public int Step { get; set; }

            public HeardEventArgs(string area_code, int step)
            {
                AreaCode = area_code;
                Step = step;
            }
        }
    }
}
