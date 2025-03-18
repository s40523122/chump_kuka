using CefSharp.DevTools.Profiler;
using iCAPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    internal class SocketDispatcher
    {
        private const int PORT = 6500;      // 設定連線 port
        private static SocketManager.Server _server = new SocketManager.Server(PORT);     // 設定 port 為 5000

        static SocketDispatcher()
        {
            // 訂閱訊息接收事件，接收訊息為 e.Message
            _server.MessageReceived += ServerMessageReceived;
        }
        
        public static async Task<bool> StartServer()
        {
            if (_server.Connected) return true;

            _server.Start();

            // 等待伺服器啟動並開始監聽
            await _server.WaitForServerToStartAsync();
            return _server.Connected;
        }
        public static void Send(string msg)
        {
            // 不論如何，送送訊息給已連接的 client
            _server.SendToAllClient(msg);
        }
        private static void ServerMessageReceived(object sender, SocketManager.MessageEventArgs e)
        {
            //string response = "Received message: " + e.Message;
            if (e.Message == "station1_call")
            {
                _server.SendToClient(e.SendClient, "station1_agv_ready");
                _server.AddToBlacklist(e.SendClient);       // 加入黑名單後不會接收廣播消息
            }
            else if (e.Message == "station2_call")
            {
                _server.SendToClient(e.SendClient, "station2_agv_ready");
                _server.AddToBlacklist(e.SendClient);       // 加入黑名單後不會接收廣播消息
            }  
        }
    }
}
