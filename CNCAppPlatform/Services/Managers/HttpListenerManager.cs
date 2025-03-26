using Chump_kuka;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCAPS.Managers
{
    internal class HttpListenerManager
    {
        private HttpListener _listener;
        private TaskCompletionSource<bool> startCompletionSource = new TaskCompletionSource<bool>(); // 用來通知外部程式伺服器啟動完成
        private string _listen_url;

        public bool IsRunning { get; private set; } = false;

        public event EventHandler<HttpMessageEventArgs> MessageReceived;     // 接收客戶端訊息時觸發的事件
        public event EventHandler<HttpConnectionEventArgs> ClientConnected;      // 新的客戶端連線時觸發的事件

        public HttpListenerManager(string listen_url)
        {
            _listen_url = listen_url;
        }
        public void Start()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(_listen_url); // 設定伺服器監聽的地址

            _listener.Start();
            IsRunning = _listener.IsListening;

            // 當伺服器啟動並開始監聽時，設定 TaskCompletionSource 為成功
            startCompletionSource.SetResult(true);
            Console.WriteLine("C# TCP Server started...");

            Task.Run(async () =>
            {
                while (true)
                {
                    // 等待 HTTP 請求
                    HttpListenerContext context = _listener.GetContext();

                    // 獲取客戶端的 IP 地址
                    //IPEndPoint remoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    //ClientConnected?.Invoke(this, new TcpConnectionEventArgs(client, remoteEndPoint));
                    
                    
                    // 開新 Task 處理客戶端
                    await HandleClientAsync(context);
                }
            });
        }

        /// <summary>
        /// 這個方法用來等待伺服器啟動並開始監聽
        /// </summary>
        /// <returns></returns>
        public Task WaitForServerToStartAsync()
        {
            return startCompletionSource.Task; // 回傳 Task，讓外部程式可以 await 這個 Task，直到伺服器啟動完成
        }

        // 等待並處理請求
        private async Task HandleClientAsync(HttpListenerContext context)
        {                
            var request = context.Request;

            if (request.HttpMethod == "POST")
            {
                // 讀取 POST 資料
                string postData;
                using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                {
                    postData = await reader.ReadToEndAsync();
                }

                MessageReceived?.Invoke(this, new HttpMessageEventArgs(context, postData));
            }
        }

        public void MessageResponse(HttpListenerContext context)
        {
            // 回應客戶端
            var response = context.Response;
            string responseString = "<html><body>請求已接收</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        // 關閉 Listener
        public void Close(FormClosingEventArgs e)
        {
            _listener?.Stop();
        }
    }

    public class HttpMessageEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }
        public string Message { get; }

        public HttpMessageEventArgs(HttpListenerContext context, string message)
        {
            Context = context;
            Message = message;
        }
    }

    public class HttpConnectionEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public IPEndPoint RemoteEndPoint { get; }

        public HttpConnectionEventArgs(TcpClient client, IPEndPoint remoteEndPoint = null)
        {
            Client = client;
            RemoteEndPoint = remoteEndPoint;
        }
    }
}
