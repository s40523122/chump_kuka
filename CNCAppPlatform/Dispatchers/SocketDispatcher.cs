﻿using CefSharp.DevTools.Profiler;
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
    internal class SocketDispatcher
    {
        //private const int PORT = 6500;      // 設定連線 port
        //private static WebSocketManager.Server _server = new WebSocketManager.Server(PORT);     // 創建伺服器實例，監聽區域網路的所有IP
        private static TcpListenerManager order_record_listener;

        public static async Task<bool> StartRecordListener(int listen_port)
        {
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

            switch (message)
            {
                case "exit":
                    Console.WriteLine("Client requested to close connection.");
                    break;
                case "station1_call":
                    // 發送 station1_agv_ready
                    await listener.SendMessageAsync("station1_agv_ready");
                    break;
                case "station2_call":
                    // 發送 station1_agv_ready
                    await listener.SendMessageAsync("station2_agv_ready");
                    break;
                default:
                    // 回傳確認訊息
                    await listener.SendMessageAsync("ACK: " + message);
                    break;
            }
        }
    }
}
