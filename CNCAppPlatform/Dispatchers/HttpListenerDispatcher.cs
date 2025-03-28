using iCAPS.Managers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Dispatchers
{
    internal class HttpListenerDispatcher
    {
        private static HttpListenerManager _kuka_listener;

        public static async Task<bool> StartKukaListener(string url)
        {
            if (_kuka_listener != null && _kuka_listener.IsRunning) return true;

            _kuka_listener = new HttpListenerManager(url);
            _kuka_listener.Start();

            await _kuka_listener.WaitForServerToStartAsync();
            _kuka_listener.MessageReceived += _kuka_listener_MessageReceived;

            return _kuka_listener.IsRunning;
        }

        private static void _kuka_listener_MessageReceived(object sender, HttpMessageEventArgs e)
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
    }
}
