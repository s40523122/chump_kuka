using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using iCAPS;
using System.Windows.Documents;
using Modbus.Device;
using System.Net.Sockets;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Chump_kuka
{
    public partial class Form1 : iCAPS.Form1
    {
        public Form1()
        {
            InitializeComponent();
            
            // Debug模式下，手動開啟 api 連線
            if (!Debugger.IsAttached) { enable_api_btn.Visible = false; }


            modbusService = new ModbusTCPMasterManager();
            Load += Form1_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Env.enble_kuka_api = true;
            KukaApiController.Enable = true;

            //IOHandle.Enable = true;
        }


        private ModbusTCPMasterManager modbusService;
        private bool NetworkIsOk = false;
        private List<PictureBox> listDI;
        private List<PictureBox> listDO;

        private void Form1_Load(object sender, EventArgs e)
        {
            listDI = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };
            //listDO = new List<PictureBox> { DO0, DO1, DO2, DO3 };
        }

        private async void btStart_Click(object sender, EventArgs e)
        {
            // NetworkIsOk = modbusService.Connect(txtIP.Text);
            NetworkIsOk = await modbusService.Connect("169.254.64.100");     // "192.168.255.1"
            if (NetworkIsOk)
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
                btStart.Enabled = false;
                btStop.Enabled = true;
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            modbusService.Disconnect();
            btStart.Enabled = true;
            btStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (NetworkIsOk)
            {
                UpdateDI();
                //UpdateDO();
            }
        }

        private void UpdateDI()
        {
            bool[] status = modbusService.ReadDI(listDI.Count);
            for (int i = 0; i < listDI.Count; i++)
            {
                listDI[i].BackColor = status[i] ? Color.Lime : Color.Green;
            }
        }

        private void UpdateDO()
        {
            bool[] coils = modbusService.ReadDO(listDO.Count);
            for (int i = 0; i < listDO.Count; i++)
            {
                listDO[i].BackColor = coils[i] ? Color.Red : Color.Maroon;
            }
        }

        private void DO_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureBox)sender).Tag.ToString());
            bool currentState = listDO[index].BackColor == Color.Red;
            modbusService.WriteDO(index, !currentState);
            listDO[index].BackColor = !currentState ? Color.Red : Color.Maroon;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log.Append("這是一個測試訊息", "Test", "Form1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            JArray robot_infos = (JArray)JObject.Parse($@"{{""code"": ""0"", 
                                                                     ""message"": """", 
                                                                      ""success"": true, 
                                                                      ""data"":[{{ 
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
                                                                            ""missionCode"" : ""missionCode111"", 
                                                                            ""updateTime"" : ""{DateTime.Now.ToString(@"G")}""
                                                                           }},
                                                                           {{ 
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
                                                                            ""missionCode"" : ""missionCode111"", 
                                                                            ""updateTime"" : ""{DateTime.Now.ToString(@"G")}""
                                                                           }}] 
                                                                    }} ")["data"];
        KukaParm.RobotStatusInfos = robot_infos;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            logWindow1.Visible = open_log_button.Checked;
        }
    }
}
