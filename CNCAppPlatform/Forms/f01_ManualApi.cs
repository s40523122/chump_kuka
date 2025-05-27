using CefSharp.DevTools.CSS;
using Chump_kuka.Controller;
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
            // 避免重複呼叫導致過度綁定
            KukaParm.AreaChanged -= KukaParm_AreaChanged;
            KukaParm.AreaChanged += KukaParm_AreaChanged;
            KukaParm_AreaChanged(null, null);
            KukaParm.CarryChanged -= KukaParm_CarryChanged;
            KukaParm.CarryChanged += KukaParm_CarryChanged;
            KukaParm_CarryChanged(null, null);
        }

        private void F01_ManualApi_VisibleChanged(object sender, EventArgs e)
        {
            // 防止表單因為提早開啟導致顯示錯誤
            if(tableLayoutPanel2.Controls.Count == 0 && Visible)
            {
                F01_ManualApi_Load(null, null);
            }
            
            KukaParm.StartNode = null;
            KukaParm.GoalNode = null;
        }

        private void KukaParm_CarryChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                selected_1.Text = KukaParm.StartNode?.Name ?? "null";
                selected_2.Text = KukaParm.GoalNode?.Name ?? "null";
            }));
        }

        private void KukaParm_AreaChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {

                tableLayoutPanel2.Controls.Clear();

                KukaParm.StartNode = KukaParm.GoalNode = null;

                /* 加入區域 Control */
                // 目前只支援到 4 組，超過可能會有 UI 顯示問題
                foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
                {
                    KukaAreaControl kuka_area = new KukaAreaControl
                    {
                        AreaName = area.AreaName,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(10),
                        AreaCode = area.AreaCode,
                        AreaNode = area.NodeList?.ToArray()
                    };

                    kuka_area.ContainerClick += Kuka_area1_ContainerClick;
                    kuka_area.AreaClick += Area_AreaClick;

                    kuka_area.UpdateContainerImage(area.NodeStatus?.ToArray());        // 初次建立，更新圖片
                    area.ControlUI = kuka_area;       // 將建立的使用者控制項與模型綁定

                    // KukaParm.AreaControls.Add(kuka_area);
                    tableLayoutPanel2.Controls.Add(kuka_area);
                }
            }));
        }

        private void Area_AreaClick(object sender, ControlClickEventArgs e)
        {
            KukaAreaControl area = e.Control as KukaAreaControl;

            if (area.Checked)
            {
                
                if (KukaParm.StartNode == null)     //if (selected_1.Tag == null)
                {
                    //selected_1.Tag = area;
                    //selected_1.Text = area.AreaName;

                    // Test
                    KukaParm.StartNode = new CarryNode
                    {
                        Code = area.AreaCode,
                        Type = area.Type,
                        Name = area.AreaName,
                    };
                }
                else if (KukaParm.GoalNode == null)        // else if (selected_2.Tag == null)
                {
                    //selected_2.Tag = area;
                    //selected_2.Text = area.AreaName;

                    // Test
                    KukaParm.GoalNode = new CarryNode
                    {
                        Code = area.AreaCode,
                        Type = area.Type,
                        Name = area.AreaName,
                    };
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
                //new List<Label> { selected_1, selected_2 }
                //.Where(c => c.Tag == area)
                //.ToList()
                //.ForEach(c =>
                //{
                //    c.Tag = null;
                //    c.Text = "null";
                //});
                if (KukaParm.StartNode != null && KukaParm.StartNode.Name == area.AreaName)
                {
                    KukaParm.StartNode = null; // 把 StartNode 設為 null
                }

                else if (KukaParm.GoalNode != null && KukaParm.GoalNode.Name == area.AreaName)
                {
                    KukaParm.GoalNode = null; // 把 GoalNode 設為 null
                }
            }
        }

        private void Kuka_area1_ContainerClick(object sender, ControlClickEventArgs e)
        {
            Container container = e.Control as Container;

            if (container.Checked)
            {
                if (KukaParm.StartNode == null)     //if (selected_1.Tag == null)
                {
                    //selected_1.Tag = container;
                    //selected_1.Text = container.ContainerName;

                    // Test
                    KukaParm.StartNode = new CarryNode
                    {
                        Code = container.ContainerName,
                        Type = container.Type,
                        Name = container.ContainerName,
                    };
                }
                else if (KukaParm.GoalNode == null)     //else if (selected_2.Tag == null)
                {
                    //selected_2.Tag = container;
                    //selected_2.Text = container.ContainerName;

                    // Test
                    KukaParm.GoalNode = new CarryNode
                    {
                        Code = container.ContainerName,
                        Type = container.Type,
                        Name = container.ContainerName,
                    };
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
                //new List<Label> { selected_1, selected_2 }
                //.Where(c => c.Tag == container)
                //.ToList()
                //.ForEach(c =>
                //{
                //    c.Tag = null;
                //    c.Text = "null";
                //});
                if (KukaParm.StartNode != null && KukaParm.StartNode.Name == container.ContainerName)
                {
                    KukaParm.StartNode = null; // 把 StartNode 設為 null
                }

                else if (KukaParm.GoalNode != null && KukaParm.GoalNode.Name == container.ContainerName)
                {
                    KukaParm.GoalNode = null; // 把 GoalNode 設為 null
                }
            }
        }

       

        private void btn_send_carry_Click(object sender, EventArgs e)
        {
            //if (!KukaApiController.Enable)
            //{
            //    MsgBox.Show("尚未開啟 kuka api");
            //    return;
            //}

            if (KukaParm.StartNode == null || KukaParm.GoalNode == null)
            {
                MsgBox.Show("派車前，必須先選取兩節點。");
                return;
            }

            //long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            //List<string> positions = new List<string>() { GetTagCode(selected_1.Tag), GetTagCode(selected_2.Tag) };
            //List<string> types = new List<string>() { GetTagType(selected_1.Tag), GetTagType(selected_2.Tag) };
            //List<string> texts = new List<string>() { selected_1.Text, selected_2.Text };

            if (go_direction.Change)
            {
                //positions.Reverse();
                //types.Reverse();
                //texts.Reverse();
                (KukaParm.StartNode, KukaParm.GoalNode) = (KukaParm.GoalNode, KukaParm.StartNode);
                go_direction.Change = !go_direction.Change;
            }

            //DialogResult dialogResult = MessageBox.Show($"是否執行派車任務?\n{texts[0]} -> {texts[1]}", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            DialogResult dialogResult = MessageBox.Show($"是否執行派車任務?\n{KukaParm.StartNode.Name} -> {KukaParm.GoalNode.Name}", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // KukaApiController.PubCarryTask();
                ChatController.AppendCarryTask(false);
                MsgBox.ShowFlash("已加入等候任務", "手動派車", 1000);
            }

            

            //var body = new
            //{
            //    orgId = "chump",     //"9001",
            //    requestId = $"request{timestamp}",
            //    missionCode = $"mission{timestamp}",
            //    missionType = "RACK_MOVE",
            //    viewBoardType = "",
            //    robotType = "LIFT",
            //    robotModels = new string[] { },
            //    robotIds = new string[] { },
            //    priority = 1,
            //    containerType = "",
            //    containerCode = "",
            //    templateCode = "",
            //    lockRobotAfterFinish = false,
            //    unlockRobotId = "",
            //    unlockMissionCode = "",
            //    idleNode = "",
            //    missionData = new[]
            //    {
            //        new
            //        {
            //            sequence = 1,
            //            position = positions[0],     //"A000000002",
            //            type = types[0],     // "NODE_AREA",
            //            putDown = false,
            //            passStrategy = "AUTO",
            //            waitingMillis = 0
            //        },
            //        new
            //        {
            //            sequence = 2,
            //            position = positions[1],     //"A000000003",
            //            type = types[1],     // "NODE_AREA",
            //            putDown = true,
            //            passStrategy = "AUTO",
            //            waitingMillis = 0
            //        }
            //    }
            //};

            //int response_code = await Env.kuka_api.PostRequest("submitMission", body);       // 等待 api 回應
            //if (response_code != 200) return;

            //string responseBody = Env.kuka_api.ResponseMessage;

            //JObject resp_json = JObject.Parse(responseBody);
            //if (!(bool)resp_json["success"])
            //{
            //    MsgBox.Show($"訪問 /submitMission 時發生異常 [{(string)resp_json["code"]}] {(string)resp_json["message"]}");
            //    return;
            //}
            
        }

        private void scaleLabel2_Click(object sender, EventArgs e)
        {
            KukaApiController.GetAreaInfo();
        }
    }
}
