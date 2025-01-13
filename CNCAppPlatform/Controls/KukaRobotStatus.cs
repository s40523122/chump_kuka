using CookComputing.XmlRpc;
using iCAPS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;


namespace Chump_kuka.Controls
{
    public partial class KukaRobotStatus : UserControl
    {
        private static Timer timer1 = new Timer();

        // 靜態事件，用於通知所有實例
        public static event EventHandler TimerTick;

        // 靜態建構子，用於初始化靜態 Timer
        static KukaRobotStatus()
        {
            timer1 = new Timer();
            timer1.Interval = 1000; // 設定時間間隔（毫秒）
            timer1.Tick += Timer1_Tick;
        }

        private static string request_time = string.Empty;
        private static JArray robot_infos = new JArray();
        
        /*private static JArray robot_infos = (JArray)JObject.Parse(@"{""code"": ""0"", 
                                                                     ""message"": """", 
                                                                      ""success"": true, 
                                                                      ""data"":[{ 
                                                                            ""robotId"":""robot001"", 
                                                                            ""robotType"":""test001"", 
                                                                            ""containerCode"" : ""container001"", 
                                                                            ""mapCode"" : ""map01"", 
                                                                            ""floorNumber"" : ""floor01"", 
                                                                            ""buildingCode"" : ""9001"", 
                                                                            ""status"" : 4, 
                                                                            ""occupyStatus"" : 1, 
                                                                            ""batteryLevel"" : 60, 
                                                                            ""nodeCode"" : ""node001"", 
                                                                            ""missionCode"" : ""missionCode111"" 
                                                                           },
                                                                           { 
                                                                            ""robotId"":""robot002"", 
                                                                            ""robotType"":""test002"", 
                                                                            ""containerCode"" : ""container002"", 
                                                                            ""mapCode"" : ""map02"", 
                                                                            ""floorNumber"" : ""floor02"", 
                                                                            ""buildingCode"" : ""9002"", 
                                                                            ""status"" : 4, 
                                                                            ""occupyStatus"" : 1, 
                                                                            ""batteryLevel"" : 60, 
                                                                            ""nodeCode"" : ""node001"", 
                                                                            ""missionCode"" : ""missionCode111"" 
                                                                           }] 
                                                                    } ")["data"];*/
        private static bool isProcessing = false;
        private int page_index = 0;
        private async static void Timer1_Tick(object sender, EventArgs e)
        {
            // 靜態計時器更新，所有元件同步更新

            if (Env.enble_kuka_api)
            {
                if (isProcessing) return; // 如果正在處理，直接退出

                var body = new
                {
                    robotId = "",
                    robotType = "",
                    mapCode = "",
                    floorNumber = ""
                };

                // 等待 api 回應
                isProcessing = true;
                string responseBody = await Env.kuka_api.PostRequest("robotQuery", body);
                isProcessing = false;

                try
                {
                    JObject resp_json = JObject.Parse(responseBody);
                    if (!(bool)resp_json["success"])
                    {
                        MsgBox.Show($"訪問 /areaQuery 時發生異常 [{(string)resp_json["code"]}] {(string)resp_json["message"]}");
                        return;
                    }

                    robot_infos = (JArray)resp_json["data"];
                    request_time = robot_infos.Count != 0 ? DateTime.Now.ToString() : "--";
                }
                catch
                {
                    MessageBox.Show(responseBody, "robot_state");
                }
            }

            TimerTick?.Invoke(sender, e);
        }

        public KukaRobotStatus()
        {
            InitializeComponent();

            Load += KukaRobotStatus_Load;
            Resize += KukaRobotStatus_Resize;
            
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;       // 切換分頁時，顯示內容
        }

        private void InfoUPdate()
        {
            if (robot_infos.Count == 0) return;

            robot_id.Text = (string)robot_infos[page_index]["robotId"];
            robot_type.Text = (string)robot_infos[page_index]["robotType"];
            container_code.Text = (string)robot_infos[page_index]["containerCode"];
            map_code.Text = (string)robot_infos[page_index]["mapCode"];
            Dictionary<string, string> status_dict = new Dictionary<string, string>() { { "1", "離場" }, { "2", "離線" }, { "3", "空閒" }, { "4", "任務中" }, { "5", "充電中" }, { "6", "更新中" }, { "7", "異常" } };
            status.Text = status_dict[(string)robot_infos[page_index]["status"]];
            Dictionary<string, string> occupy_dict = new Dictionary<string, string>() { { "0", "未占用" }, { "1", "占用中" }};
            occupy_status.Text = occupy_dict[(string)robot_infos[page_index]["occupyStatus"]];
            battery_level.Text = (string)robot_infos[page_index]["batteryLevel"];
            node_code.Text = (string)robot_infos[page_index]["nodeCode"];
            update_time.Text = request_time;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 取得當前選中的 TabPage
            TabPage currentTab = tabControl1.SelectedTab;

            if (currentTab != null)
            {
                // 將按鈕的 Parent 設置為當前 TabPage
                tableLayoutPanel1.Parent = currentTab;

                page_index = tabControl1.SelectedIndex;
                InfoUPdate();

                // 動態調整按鈕的位置（保持相對位置，也可以自定義）
                //tableLayoutPanel1.Location = new Point(20, 20); // 可以改為根據需求計算位置
            }
        }

        private void KukaRobotStatus_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;     // 若在設計階段，不執行以下內容。( 需在 Load 事件中才有用)

            tabControl1.Controls.Clear();

            if (!timer1.Enabled) timer1.Start();        // 當靜態計時器未啟動時，啟動它。
            
            // 訂閱實例的計時器事件
            TimerTick += OnTimerTick;
        }

        private void KukaRobotStatus_Resize(object sender, EventArgs e)
        {
            tabControl1.Font = robot_id.Font;
        }

        private void OnTimerTick(object Sender, EventArgs e)
        {
            if (robot_infos.Count != tabControl1.TabCount || tabControl1.TabCount==0)
            {
                tabControl1.Controls.Clear();
                page_index = 0;
                tabControl1.Controls.Add(tabPage1);

                for (int i=0; i < robot_infos.Count; i++)
                {
                    TabPage page = new TabPage() 
                    { 
                        Text = (string)robot_infos[i]["robotId"], 
                        BackColor = Color.White, 
                        Padding = new Padding(15) 
                    };

                    tabControl1.Controls.Add(page);

                    if (i == 0) 
                    {
                        tableLayoutPanel1.Parent = page;        // 清空後記得加回來
                        tabControl1.Controls.Remove(tabPage1);
                    }
                }
            }

            InfoUPdate();
        }

        
        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                TimerTick -= OnTimerTick;       // 記得在釋放時取消訂閱靜態事件，避免內存洩漏
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
