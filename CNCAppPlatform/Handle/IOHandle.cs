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

namespace Chump_kuka
{
    internal class IOHandle
    {
        public static string BindArea = "倉庫區";
        public static IPAddress ModbusTcpIp = IPAddress.Parse("192.168.255.1");

        // Api 啟用狀態
        public static bool Enable
        {
            get => _enable;
            set
            {
                if (string.IsNullOrEmpty(BindArea))
                {
                    MessageBox.Show("請先指定綁定區域。(IOHandle.BindArea = \"\")");
                    return;
                }
                
                if (value == _enable) return;       // 防止重複設定

                _enable = value;

                if (_enable) requestTimer.Start();
                else
                {
                    requestTimer.Stop();
                    modbusService?.Disconnect();        // 如果已建立連線則中斷連線
                }
            }
        }
        private static bool _enable = false;

        private static System.Windows.Forms.Timer requestTimer;

        private static ModbusTCP_Master_Service modbusService;
        static IOHandle()
        {
            // 設定計時器
            requestTimer = new System.Windows.Forms.Timer();
            requestTimer.Interval = 1000; // 每 1 秒請求一次
            requestTimer.Tick += RequestTimer_Tick; ;

            modbusService = new ModbusTCP_Master_Service();
        }

        private static async void RequestTimer_Tick(object sender, EventArgs e)
        {
            if (!modbusService.isConnected)
            {
                requestTimer.Stop();
                // 若無法與 tcp 建立通訊則禁用
                requestTimer.Enabled = Enable = await modbusService.Connect(ModbusTcpIp.ToString());
            }

            bool[] status = modbusService.ReadDI(4);

            // 更改圖片
            KukaAreaControl control = KukaAreaControl.Find(BindArea, KukaParm.AreaControls);
            if (control == null) Log.Append("綁定區域不存在", "Error", "IOHandle.cs");
            else control.UpdateContainerImage(ToNodeStatus(status).ToArray());

            Console.WriteLine(string.Join("", status.Select(b => b ? "1" : "0")));
        }

        private static List<int> ToNodeStatus(bool[] input_readers)
        {
            List<int> status = new List<int>();
            for (int i = 0; i < input_readers.Length; i ++)
            {
                bool turtle_sensor = input_readers[i++];    // 先取出當前索引的值，並遞增索引
                bool rack_sensor = input_readers[i];    // 取得遞增後的索引值

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
