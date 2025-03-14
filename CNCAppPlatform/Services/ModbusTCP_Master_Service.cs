using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.DevTools.IO;
using Modbus.Device;

namespace Chump_kuka
{
    /// <summary>
    /// 負責 Modbus TCP 通訊的後端邏輯，獨立於 UI 介面。
    /// </summary>
    public class ModbusTCP_Master_Service
    {
        public bool isConnected = false;

        private ModbusIpMaster master;
        private TcpClient tcpClient;
        private byte slaveID = 1; // 預設 Modbus Slave ID

        /// <summary>
        /// 嘗試連接到 Modbus TCP 伺服器。
        /// </summary>
        /// <param name="ipAddress">伺服器 IP 地址</param>
        /// <returns>是否連線成功</returns>
        public async Task<bool> Connect(string ipAddress, int port=502)
        {
            try
            {
                tcpClient = new TcpClient();
                //bool isConnected = await ExecuteWithTimeout(async () =>
                //{
                //    try
                //    {
                //        await tcpClient.ConnectAsync(ipAddress, 502); // 確保支援取消
                //        return true;
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //        return false;
                //    }
                //}, 3000);

                var timeOut = TimeSpan.FromSeconds(3);
                var cancellationCompletionSource = new TaskCompletionSource<bool>();
                using (var cts = new CancellationTokenSource(timeOut))
                {
                    var task = tcpClient.ConnectAsync(ipAddress, port);

                    using (cts.Token.Register(() => cancellationCompletionSource.TrySetResult(true)))
                    {
                        if (task != await Task.WhenAny(task, cancellationCompletionSource.Task))
                        {
                            throw new OperationCanceledException(cts.Token);
                        }
                    }

                }
                isConnected = tcpClient.Connected;
                if (!isConnected) return false;

                master = ModbusIpMaster.CreateIp(tcpClient);
                master.Transport.Retries = 0;       // 請求失敗重試次數
                master.Transport.ReadTimeout = 1500;        // 設定 Modbus 訊息的讀取超時時間（毫秒）

                //isConnected = true;
                return true;
            }
            catch (TimeoutException)
            {
                // MessageBox.Show("ModbusService Error: 連線超過時間，已被中斷");
                Log.Append("ModbusService Error: 連線超過時間，已被中斷", "Error", this.GetType().Name);
            }

            catch (Exception ex)
            {
                MessageBox.Show("ModbusService Error: 連線失敗: " + ex.Message);
            }
            Disconnect();
            return false;
        }

        // 設定超時的函數，接受 Task 類型的 lambda
        static async Task<bool> ExecuteWithTimeout(Func<Task<bool>> taskFunc, int timeout)
        {

            var task = taskFunc();
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await task; // 執行成功，回傳結果
            }
            else
            {
                return false; // 超時回傳 false
            }

        }

        /// <summary>
        /// 斷開與 Modbus TCP 伺服器的連線。
        /// </summary>
        public void Disconnect()
        {
            isConnected = false;
            master?.Dispose();
            if (tcpClient.Connected) tcpClient?.Close();
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
