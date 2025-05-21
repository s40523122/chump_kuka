using CefSharp.DevTools.Profiler;
using Chump_kuka.Controller;
using iCAPS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace Chump_kuka
{
    internal class FeedbackDispatcher
    {
        //private const int PORT = 6500;      // 設定連線 port
        //private static WebSocketManager.Server _server = new WebSocketManager.Server(PORT);     // 創建伺服器實例，監聽區域網路的所有IP
        private static TcpListenerManager order_record_listener;

        public static event EventHandler<TextEventArgs> Called;

        public static async Task<bool> StartRecordListener(int listen_port)
        {
            if (order_record_listener != null && order_record_listener.IsRunning) return true;

            order_record_listener = new TcpListenerManager(listen_port);       // 6400
            
            // 開啟監聽
            order_record_listener.Start();
            
            await order_record_listener.WaitForServerToStartAsync();

            order_record_listener.MessageReceived += Order_record_listener_MessageReceived;
            return order_record_listener.IsRunning;
        }

        public static async void SendToRecordSystem(string msg)
        {
            // 不論如何，送送訊息給已連接的 client
            //await _server.SendToAllClients(msg);

            await order_record_listener.SendMessageAsync(msg);
        }

        private static async void Order_record_listener_MessageReceived(object sender, TcpMessageEventArgs e)
        {
            string message = e.Message.Trim().ToLower();
            TcpListenerManager listener = sender as TcpListenerManager;

            Log.Append($"收到報工通知{message}", "INFO", "FeedBackDispatcher");

            switch (message)
            {
                case "exit":
                    Console.WriteLine("Client requested to close connection.");
                    break;
                case "station1_call":
                    // 發送 station1_agv_ready
                    // await listener.SendMessageAsync("station1_agv_ready");
                    Called?.Invoke(sender, new TextEventArgs(KukaParm.KukaAreaModels[0].AreaCode));     // 傳遞第一區域發車命令

                    break;
                case "station2_call":
                    // 發送 station1_agv_ready
                    // await listener.SendMessageAsync("station2_agv_ready");
                    Called?.Invoke(sender, new TextEventArgs(KukaParm.KukaAreaModels[1].AreaCode));     // 傳遞第二區域發車命令
                    break;
                default:
                    // 回傳確認訊息
                    await listener.SendMessageAsync("ACK: " + message);
                    break;
            }
        }
    }
}
