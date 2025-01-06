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
        public static Container AddSeleted 
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
        public static Container RemoveSeleted
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
