using CefSharp.DevTools.CSS;
using Chump_kuka.Controls;
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
using System.Web.Management;
using System.Windows.Forms;

namespace Chump_kuka.Forms
{
    public partial class f01_ManualApi : Form
    {
        
        public f01_ManualApi()
        {
            InitializeComponent();

            Load += F01_ManualApi_Load;
            VisibleChanged += F01_ManualApi_VisibleChanged;
        }

        private void F01_ManualApi_Load(object sender, EventArgs e)
        {
            foreach (kuka_area area in tableLayoutPanel2.Controls)
            {
                area.ContainerClick += Kuka_area1_ContainerClick;
                area.AreaClick += Area_AreaClick;
            }
        }

        private async void F01_ManualApi_VisibleChanged(object sender, EventArgs e)
        {
            // 當切換至當前分頁時，執行以下動作
            // if (Visible) MessageBox.Show("Show");

            if (!Debugger.IsAttached)
            {
                tableLayoutPanel2.Controls.Clear();


                /* 取得區域列表   */
                string area_responseBody = await Env.kuka_api.GetResponse("areaQuery");     // 等待 api 回應
                /* 沒連接 api 時，測試用
                string area_responseBody = @"{""code"": ""0"", 
                                              ""message"": """", 
                                              ""success"": true, 
                                              ""data"":[{ 
                                                            ""areaName"":""加工區"", 
                                                            ""areaType"":2, 
                                                            ""areaCode"" : ""area001"" 
                                                        },{ 
                                                            ""areaName"":""成品區"", 
                                                            ""areaType"":2, 
                                                            ""areaCode"" : ""area002"" 
                                                        }]}";*/

                JObject resp_json = JObject.Parse(area_responseBody);

                if (!(bool)resp_json["success"])
                {
                    MsgBox.Show($"訪問 /areaQuery 時發生異常 [{(string)resp_json["code"]}] {(string)resp_json["message"]}");
                    return;
                }

                JArray area_infos = (JArray)resp_json["data"];
                List<string> area_codes = area_infos.Select(area => (string)area["areaCode"]).ToList();      // 集合查詢到的區域代碼為列表
                Dictionary<string, string> areaDictionary = area_infos
                .ToDictionary(
                    area => (string)area["areaCode"],
                    area => (string)area["areaName"]
                );

                /* 取得區域內的節點 */
                var body = new
                {
                    areaCodes = area_codes
                };

                string node_responseBody = await Env.kuka_api.PostRequest("areaNodesQuery", body);       // 等待 api 回應
                /* 沒連接 api 時，測試用
                string node_responseBody = @"{""code"": ""0"", 
                                              ""message"": """", 
                                              ""success"": true, 
                                              ""data"":[{ 
                                                            ""areaCode"" : ""area001"", 
                                                            ""nodeList"":[""node1"",""node2""] 
                                                        },{ 
                                                            ""areaCode"" : ""area002"", 
                                                            ""nodeList"":[""node3"",""node4""] 
                                                        }]} ";*/

                resp_json = JObject.Parse(node_responseBody);
                if (!(bool)resp_json["success"])
                {
                    MsgBox.Show($"訪問 /areaNodesQuery 時發生異常 [{(string)resp_json["code"]}] {(string)resp_json["message"]}");
                    return;
                }

                JArray node_infos = (JArray)resp_json["data"];

                /* 加入區域 Control */
                // 目前只支援到 4 組，超過可能會有 UI 顯示問題
                foreach (JObject node_info in node_infos)
                {
                    tableLayoutPanel2.Controls.Add(new kuka_area
                    {
                        AreaName = areaDictionary[(string)node_info["areaCode"]],
                        Dock = DockStyle.Fill,
                        Margin = new Padding(10),
                        AreaCode = (string)node_info["areaCode"],
                        AreaNode = ((JArray)node_info["nodeList"]).Select(item => (string)item).ToArray()
                    });
                }

                // 加上點擊事件
                foreach (kuka_area area in tableLayoutPanel2.Controls)
                {
                    area.ContainerClick += Kuka_area1_ContainerClick;
                    area.AreaClick += Area_AreaClick;
                }
            }
        }

        private void Area_AreaClick(object sender, ControlClickEventArgs e)
        {
            kuka_area area = e.Control as kuka_area;

            if (area.Checked)
            {
                if (selected_1.Tag == null)
                {
                    selected_1.Tag = area;
                    selected_1.Text = area.AreaName;
                }
                else if (selected_2.Tag == null)
                {
                    selected_2.Tag = area;
                    selected_2.Text = area.AreaName;
                }
                else
                {
                    area.Checked = false;
                    MsgBox.Show("已選擇2個節點");
                }
            }
            else
            {
                // 如果觸發點擊的容器被取消選取，
                // 判斷該容器是否存在於 selected_1 或 selected_2 的 Tag中，
                // 若存在，清空該 selected_1 或 selected_2
                new List<Label> { selected_1, selected_2 }
                .Where(c => c.Tag == area)
                .ToList()
                .ForEach(c =>
                {
                    c.Tag = null;
                    c.Text = "null";
                });
            }
        }

        private void Kuka_area1_ContainerClick(object sender, ControlClickEventArgs e)
        {
            Container container = e.Control as Container;

            if (container.Checked)
            {
                if (selected_1.Tag == null)
                {
                    selected_1.Tag = container;
                    selected_1.Text = container.ContainerName;
                }
                else if (selected_2.Tag == null)
                {
                    selected_2.Tag = container;
                    selected_2.Text = container.ContainerName;
                }
                else
                {
                    container.Checked = false;
                    MsgBox.Show("已選擇2個節點");
                }
            }
            else
            {
                // 如果觸發點擊的容器被取消選取，
                // 判斷該容器是否存在於 selected_1 或 selected_2 的 Tag中，
                // 若存在，清空該 selected_1 或 selected_2
                new List<Label> { selected_1, selected_2 }
                .Where(c => c.Tag == container)
                .ToList()
                .ForEach(c =>
                {
                    c.Tag = null;
                    c.Text = "null";
                });
            }
        }

        private string GetTagType(object tag)
        {
            if (tag is Container obj1) return obj1.Type;
            if (tag is kuka_area obj2) return obj2.Type;
            return string.Empty; // 預設值
        }

        private string GetTagCode(object tag)
        {
            if (tag is Container obj1) return obj1.ContainerName;
            if (tag is kuka_area obj2) return obj2.AreaCode;
            return string.Empty; // 預設值
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (selected_1.Tag == null || selected_2.Tag == null)
            {
                MsgBox.Show("派車前，必須先選取兩節點。");
                return;
            }

            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            List<string> positions = new List<string>() { GetTagCode(selected_1.Tag), GetTagCode(selected_2.Tag) };
            List<string> types = new List<string>() { GetTagType(selected_1.Tag), GetTagType(selected_2.Tag) };
            List<string> texts = new List<string>() { selected_1.Text, selected_2.Text };

            if (go_direction.Change)
            {
                positions.Reverse();
                types.Reverse();
                texts.Reverse();
            }

            DialogResult dialogResult = MessageBox.Show($"是否執行派車任務?\n{texts[0]} -> {texts[1]}", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var body = new
            {
                orgId = "chump",     //"9001",
                requestId = $"request{timestamp}",
                missionCode = $"mission{timestamp}",
                missionType = "RACK_MOVE",
                viewBoardType = "",
                robotType = "LIFT",
                robotModels = new string[] { },
                robotIds = new string[] { },
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
                        position = positions[0],     //"A000000002",
                        type = types[0],     // "NODE_AREA",
                        putDown = false,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    },
                    new
                    {
                        sequence = 2,
                        position = positions[1],     //"A000000003",
                        type = types[1],     // "NODE_AREA",
                        putDown = true,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    }
                }
            };
            
            string responseBody = await Env.kuka_api.PostRequest("submitMission", body);

            JObject resp_json = JObject.Parse(responseBody);
            if (!(bool)resp_json["success"])
            {
                MsgBox.Show($"訪問 /submitMission 時發生異常 [{(string)resp_json["code"]}] {(string)resp_json["message"]}");
                return;
            }
            MessageBox.Show("OK");
        }
    }
}
