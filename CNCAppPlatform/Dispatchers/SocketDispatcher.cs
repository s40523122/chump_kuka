using CefSharp.DevTools.Profiler;
using iCAPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Chump_kuka
{
    internal class SocketDispatcher
    {
        private const int PORT = 6500;      // 設定連線 port
        //private static WebSocketManager.Server _server = new WebSocketManager.Server(PORT);     // 創建伺服器實例，監聽區域網路的所有IP

        public static async Task<bool> StartServer()
        {
            //if (_server.IsRunning) return true;

            //// 等待伺服器啟動並開始監聽
            //_server.Start();
            //await _server.WaitForServerToStartAsync();
            //if (_server.IsRunning)
            //{
            //    Console.WriteLine("已開啟 WebSocket 伺服器!");

            //    // 註冊事件處理並啟動伺服器
            //    _server.ClientConnected += (sender, e) =>
            //    {
            //        Console.WriteLine($"客戶端 {e.RemoteEndPoint} 已連線");
            //    };

            //    _server.MessageReceived += ServerMessageReceived;
            //}

            //return _server.IsRunning;

            TcpTest.Start();
            await TcpTest.WaitForServerToStartAsync();
            return true;
        }

        public static async void Send(string msg)
        {
            // 不論如何，送送訊息給已連接的 client
            //await _server.SendToAllClients(msg);

            await TcpTest.SendMessageAsync(msg);
        }
        private static async void ServerMessageReceived(object sender, WebSocketManager.WebSocketMessageEventArgs e)
        {
            //string response = "Received message: " + e.Message;
            //if (e.Message == "station1_call")
            //{
            //    await _server.SendToClient(e.Client, "station1_agv_ready");
            //}
            //else if (e.Message == "station2_call")
            //{
            //    await _server.SendToClient(e.Client, "station2_agv_ready");
            //}
        }
    }
}
