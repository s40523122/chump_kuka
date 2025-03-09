using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Forms
{
    public partial class f00_test : Form
    {
        private HttpListener _listener;
        private Thread _listenerThread;

        public f00_test()
        {
            InitializeComponent();
            StartListener();
        }

        // 開啟 HTTP Listener
        private void StartListener()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://192.168.68.66:8899/missionStateCallback/"); // 設定伺服器監聽的地址

            _listenerThread = new Thread(new ThreadStart(ListenForRequests));
            _listenerThread.Start();
        }

        // 等待並處理請求
        private void ListenForRequests()
        {
            // TODO 關閉表單時釋放
            return;
            _listener.Start();
            while (true)
            {
                // 等待 HTTP 請求
                var context = _listener.GetContext();
                var request = context.Request;

                if (request.HttpMethod == "POST")
                {
                    // 讀取 POST 資料
                    string postData;
                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        postData = reader.ReadToEnd();
                    }

                    // 將 JSON 解析為 JObject
                    JObject jsonObj = JObject.Parse(postData);

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

                // 回應客戶端
                var response = context.Response;
                string responseString = "<html><body>請求已接收</body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }

        // 關閉 Listener
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _listener.Stop();
            _listenerThread.Abort();
            base.OnFormClosing(e);
        }
    }
}
