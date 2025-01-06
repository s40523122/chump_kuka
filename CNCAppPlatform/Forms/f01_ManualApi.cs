using CefSharp.DevTools.CSS;
using Chump_kuka.Controls;
using iCAPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Forms
{
    public partial class f01_ManualApi : Form
    {
        public Container AddSeleted 
        { 
            set 
            {   
                if (two_selected.Count == 2)
                {
                    value.Checked = false;
                    MsgBox.Show("已選擇2個節點");
                    return;
                }
                //two_selected.Clear();
                two_selected.Add(value);
                Refresh_Selected();
            }
        }
        public Container RemoveSeleted
        {
            set
            {
                if (two_selected.Contains(value)){
                    two_selected.Remove(value);
                }
            }
        }
        private static List<Container> two_selected = new List<Container>();
        
        iCAPS.HttpRequest api = new iCAPS.HttpRequest("http://192.168.68.84:10870/interfaces/api/amr/");

        public f01_ManualApi()
        {
            InitializeComponent();

            Load += F01_ManualApi_Load;
        }

        private void F01_ManualApi_Load(object sender, EventArgs e)
        {
            
            foreach(kuka_area area in tableLayoutPanel2.Controls)
            {
                area.ContainerClick += Kuka_area1_ContainerClick;
            }
        }

        private void Kuka_area1_ContainerClick(object sender, ContainerClickEventArgs e)
        {
            Container container = e.Container as Container;

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

        private static void Refresh_Selected()
        {
   
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var body = new
            {
                orgId = "9001",
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
                        position = "A000000002",
                        type = "NODE_AREA",
                        putDown = false,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    },
                    new
                    {
                        sequence = 2,
                        position = "A000000003",
                        type = "NODE_AREA",
                        putDown = true,
                        passStrategy = "AUTO",
                        waitingMillis = 0
                    }
                }
            };

            string responseBody = await api.PostRequest("submitMission", body);
            MessageBox.Show(responseBody);
        }

    }
}
