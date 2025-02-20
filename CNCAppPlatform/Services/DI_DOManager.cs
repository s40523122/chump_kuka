using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;

namespace ModbusTCP_Master
{
    /// <summary>
    /// 負責 Modbus TCP 通訊的後端邏輯，獨立於 UI 介面。
    /// </summary>
    public class ModbusService
    {
        private ModbusIpMaster master;
        private TcpClient tcpClient;
        private byte slaveID = 1; // 預設 Modbus Slave ID

        /// <summary>
        /// 嘗試連接到 Modbus TCP 伺服器。
        /// </summary>
        /// <param name="ipAddress">伺服器 IP 地址</param>
        /// <returns>是否連線成功</returns>
        public async Task<bool> Connect(string ipAddress)
        {
            try
            {
                tcpClient = new TcpClient();
                await ExecuteWithTimeout(() => Task.Run(() => tcpClient.Connect(ipAddress, 502)), 3000); // 設定 timeout 為 3 秒
                //await Task.Run(() => tcpClient.Connect(ipAddress, 502)); // 設定 timeout 為 3 秒
                master = ModbusIpMaster.CreateIp(tcpClient);
                master.Transport.Retries = 0;
                master.Transport.ReadTimeout = 1500;
                return true;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("ModbusService Error: 連線超過時間，已被中斷");
            }

            catch (Exception ex)
            {
                MessageBox.Show("ModbusService Error: 連線失敗: " + ex.Message);
            }
            Disconnect();
            return false;
        }

        // 設定超時的函數，接受 Task 類型的 lambda
        static async Task ExecuteWithTimeout(Func<Task> taskFunc, int timeout)
        {
            var task = taskFunc(); // 呼叫傳入的 lambda 以獲得 Task
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                await task; // 只有當 task 完成時才繼續
            }
            else
            {
                throw new TimeoutException("操作超過指定時間");
            }
        }

        /// <summary>
        /// 斷開與 Modbus TCP 伺服器的連線。
        /// </summary>
        public void Disconnect()
        {
            master?.Dispose();
            tcpClient?.Close();
        }

        /// <summary>
        /// 讀取 DI 狀態。
        /// </summary>
        /// <param name="count">要讀取的輸入數量</param>
        /// <returns>DI 狀態陣列</returns>
        public bool[] ReadDI(int count)
        {
            if (master == null) return new bool[count];
            return master.ReadInputs(slaveID, 0, (ushort)count);
        }

        /// <summary>
        /// 讀取 DO 狀態。
        /// </summary>
        /// <param name="count">要讀取的輸出數量</param>
        /// <returns>DO 狀態陣列</returns>
        public bool[] ReadDO(int count)
        {
            if (master == null) return new bool[count];
            return master.ReadCoils(slaveID, 0, (ushort)count);
        }

        /// <summary>
        /// 設定 DO 狀態。
        /// </summary>
        /// <param name="index">DO 索引</param>
        /// <param name="state">設定狀態</param>
        public void WriteDO(int index, bool state)
        {
            master?.WriteSingleCoil(slaveID, (ushort)index, state);
        }
    }
}
