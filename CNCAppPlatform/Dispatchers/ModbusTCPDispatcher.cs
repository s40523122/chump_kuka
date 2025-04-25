using Chump_kuka.Controller;
using Chump_kuka.Controls;
using Chump_kuka.Services.Managers;
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
        private ModbusTCPMasterManager modbusService;
        private System.Windows.Forms.Timer requestTimer;

        public int RegisterCount { get; set; } = 0;

        public event EventHandler<SensorDataEventArgs> SensorRead;     // 讀取感測器數據時觸發的事件
        
        public ModbusTCPDispatcher()
        {
            // 設定計時器
            requestTimer = new System.Windows.Forms.Timer();
            requestTimer.Interval = 1000; // 每 1 秒請求一次
            requestTimer.Tick += RequestTimer_Tick; ;

            modbusService = new ModbusTCPMasterManager();
        }

        public async Task<bool> Start(IPEndPoint sensor_modbus_tcp)
        {
            if (!modbusService.isConnected)
            {
                if (sensor_modbus_tcp == null)
                {
                    Log.Append("尚未設定 Modbus TCP 連接資訊", "Error", "ModbusTCPManager.cs");
                    return false;
                }
                bool conn = await modbusService.Connect(sensor_modbus_tcp.Address.ToString(), sensor_modbus_tcp.Port);
                if (!conn)
                {
                    Log.Append($"無法建立 Modbus TCP 連線。({sensor_modbus_tcp.ToString()})", "Error", "ModbusTCPManager.cs");
                    return false;
                }
            }

            if (!requestTimer.Enabled) 
                requestTimer.Start();

            return modbusService.isConnected;
        }

        public void LightControl(bool state)
        {
            modbusService.WriteDO(0, state);
        }

        private void RequestTimer_Tick(object sender, EventArgs e)
        {
            if (RegisterCount == 0) return;
            
            try 
            {
                bool[] status = modbusService.ReadDI(RegisterCount);
                SensorRead.Invoke(KukaParm.BindAreaModel, new SensorDataEventArgs(status));
            }
            catch (Exception ex) 
            {
                Log.Append($"Modbus TCP 連線發生意外。({ex.Message})", "Error", "ModbusTCPManager.cs");
                requestTimer.Stop();
                modbusService?.Disconnect();        // 如果已建立連線則中斷連線
            }
        }
    }

    public class SensorDataEventArgs : EventArgs
    {
        public bool[] Data { get; }
        

        public SensorDataEventArgs(bool[] data)
        {
            Data = data;
        }
    }
}
