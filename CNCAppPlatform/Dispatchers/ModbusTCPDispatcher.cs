using Chump_kuka.Controller;
using Chump_kuka.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Chump_kuka
{
    internal class ModbusTCPDispatcher
    {
        // public static string BindArea = "倉庫區";
        // public static IPEndPoint ConnectInfo = null;        // 連線資訊

        private static bool _try_conn = false;      // 是否已嘗試連線

        // Api 啟用狀態
        public static bool Enable
        {
            get => _enable;
            set
            {
                if (value == _enable) return;       // 防止重複設定

                _enable = value;


                if (_enable)
                {
                    if (!requestTimer.Enabled) requestTimer.Start();
                }
                else
                {
                    if (requestTimer.Enabled) requestTimer.Stop();
                    modbusService?.Disconnect();        // 如果已建立連線則中斷連線
                }
            }
        }
        private static bool _enable = false;

        private static System.Windows.Forms.Timer requestTimer;

        private static ModbusTCPMasterManager modbusService;
        static ModbusTCPDispatcher()
        {
            // 設定計時器
            requestTimer = new System.Windows.Forms.Timer();
            requestTimer.Interval = 1000; // 每 1 秒請求一次
            requestTimer.Tick += RequestTimer_Tick; ;

            modbusService = new ModbusTCPMasterManager();
        }

        /// <summary>
        /// 確認通訊是否正常
        /// </summary>
        public static async Task<bool> CheckConnect()
        {
            // 透過向 /areaQuery 請求，判定是否通訊正常，
            // 此函數執行時會中斷計時器，管理器的所有動作將會失效
            //requestTimer.Stop();

            int elapsed = 0;
            int interval = 100; // 每 100ms 檢查一次
            while (elapsed < 10000)
            {
                if (_try_conn)
                {
                    _try_conn = false;
                    return modbusService.isConnected; // 變數變成 true，立即返回
                }
                await Task.Delay(interval);
                elapsed += interval;
            }
            Log.Append($"Modbus 連線超時", "ERROR", "Modbus Test");
            Enable = false;
            return false; // 超時
        }

        private static async void RequestTimer_Tick(object sender, EventArgs e)
        {
            if (!modbusService.isConnected)
            {
                requestTimer.Stop();
                if (Env.SensorModbusTcp == null)
                {
                    Log.Append("尚未設定 Modbus TCP 連接資訊", "Error", "ModbusTCPManager.cs");
                    return;
                }
                bool conn = await modbusService.Connect(Env.SensorModbusTcp.Address.ToString(), Env.SensorModbusTcp.Port);
                _try_conn = true;
                if (!conn)
                {
                    Log.Append($"無法建立 Modbus TCP 連線。({Env.SensorModbusTcp.ToString()})", "Error", "ModbusTCPManager.cs");
                    return;
                }
                else requestTimer.Start();
            }

            // 若無綁定區域，開啟彈出是視窗進行綁定
            if (string.IsNullOrEmpty(Env.BindAreaName))
            {
                requestTimer.Stop();
                // MessageBox.Show("請先指定綁定區域。(IOHandle.BindArea = \"\")");
                Form form = new Form() { StartPosition = FormStartPosition.CenterParent };
                
                ComboBox comboBox = new ComboBox();
                foreach (KukaAreaModel area in KukaParm.KukaAreaModels)
                {
                    comboBox.Items.Add(area.AreaName);
                }
                comboBox.SelectedValueChanged += (_sender, _e) => LocalAreaController.BindArea = KukaAreaModel.Find(comboBox.Text, KukaParm.KukaAreaModels);

                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Controls.AddRange(new Control[2] { new Label() { Text = "請選擇綁定區域" }, comboBox });

                form.Controls.Add(flowLayoutPanel);

                form.ShowDialog();

                if (string.IsNullOrEmpty(LocalAreaController.BindArea?.AreaName))
                {
                    Enable = false;
                    return;
                }
                else 
                    requestTimer.Start();
            }
            else
            {
                LocalAreaController.BindArea = KukaAreaModel.Find(Env.BindAreaName, KukaParm.KukaAreaModels);
                if (string.IsNullOrEmpty(LocalAreaController.BindArea?.AreaName))
                {
                    Enable = false;
                    return;
                }
                else
                    requestTimer.Start();
            }

            bool[] status = modbusService.ReadDI(LocalAreaController.BindArea.NodeList.Count * 2);

            //KukaAreaControl control = KukaAreaControl.Find(KukaParm.BindArea.AreaName, KukaParm.AreaControls);
            //areas.FirstOrDefault(area => area.AreaName == target_area);
            if (LocalAreaController.BindArea == null) Log.Append("綁定區域不存在", "Error", "IOHandle.cs");
            // else KukaParm.BindArea.UpdateContainerImage(ToNodeStatus(status).ToArray());
            else LocalAreaController.BindArea.NodeStatus = ToNodeStatus(status);

            // Console.WriteLine(string.Join("", status.Select(b => b ? "1" : "0")));
        }

        private static List<int> ToNodeStatus(bool[] input_readers)
        {
            List<int> status = new List<int>();
            for (int i = 0; i < input_readers.Length-1; i+=2)
            {
                bool turtle_sensor = input_readers[i];    // 先取出當前索引的值，並遞增索引
                bool rack_sensor = input_readers[i+1];    // 取得遞增後的索引值

                /*
                - 若 rack_sensor 為 false，代表無貨架，狀態設定為 0。
                - 若 rack_sensor 為 true 且 turtle_sensor 為 false，代表有貨架但無烏龜車，狀態設定為 1。
                - 若 rack_sensor 為 true 且 turtle_sensor 為 true，代表有貨架且有烏龜車，狀態設定為 2。
                */
                status.Add(!rack_sensor ? 0 : (!turtle_sensor ? 1 : 2));
            }
            return status;
        }
    }
}
