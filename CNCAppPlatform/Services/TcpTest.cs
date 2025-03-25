using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using CefSharp.DevTools.Security;

namespace Chump_kuka
{
    internal class TcpTest
    {
        private static TaskCompletionSource<bool> startCompletionSource = new TaskCompletionSource<bool>(); // 用來通知外部程式伺服器啟動完成
        private static NetworkStream stream;
        public static void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 6400);
            server.Start();
            // 當伺服器啟動並開始監聽時，設定 TaskCompletionSource 為成功
            startCompletionSource.SetResult(true);
            Console.WriteLine("C# TCP Server started...");

            Task.Run(async () =>
            {
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client); // 開新 Task 處理客戶端
                }
            });
        }

        /// <summary>
        /// 這個方法用來等待伺服器啟動並開始監聽
        /// </summary>
        /// <returns></returns>
        public static Task WaitForServerToStartAsync()
        {
            return startCompletionSource.Task; // 回傳 Task，讓外部程式可以 await 這個 Task，直到伺服器啟動完成
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true) // 允許持續接收多筆資料
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // 連線已關閉

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received from Node.js: " + message);

                    if (message.Trim().ToLower() == "exit")
                    {
                        Console.WriteLine("Client requested to close connection.");
                        break; // 結束與此客戶端的溝通
                    }

                    // 如果接收到 "station1_call"，開始發送回應訊息
                    if (message.Trim().ToLower() == "station1_call")
                    {
                        // 發送 station1_agv_ready 並停留 3 秒
                        await SendMessageAsync("station1_agv_ready");
                        await Task.Delay(3000); // 停留 3 秒
                    }
                    else if (message.Trim().ToLower() == "station2_call")
                    {
                        // 發送 station1_agv_ready 並停留 3 秒
                        await SendMessageAsync("station2_agv_ready");
                        await Task.Delay(3000); // 停留 3 秒
                    }
                    else
                    {
                        // 回傳確認訊息
                        byte[] response = Encoding.UTF8.GetBytes("ACK: " + message);
                        await stream.WriteAsync(response, 0, response.Length);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }

        // 發送訊息的輔助方法
        public static async Task SendMessageAsync(string message)
        {
            byte[] response = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(response, 0, response.Length);
            Console.WriteLine("Sent to Node.js: " + message);
        }
    }
    
}
