using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chump_kuka
{
    public class WebSocketManager
    {
        public class Server
        {
            private readonly string _url;
            private System.Collections.Generic.List<WebSocket> connect_clients = new System.Collections.Generic.List<WebSocket>() { };
            private System.Collections.Generic.List<WebSocket> blacklist = new System.Collections.Generic.List<WebSocket>() { };
            private HttpListener _listener;
            private bool _isRunning;
            private CancellationTokenSource _cts;
            private TaskCompletionSource<bool> startCompletionSource = new TaskCompletionSource<bool>(); // 用來通知外部程式伺服器啟動完成

            public bool IsRunning {  get { return _isRunning; } }

            /// <summary>
            /// 當接收到客戶端訊息時觸發的事件
            /// </summary>
            public event EventHandler<WebSocketMessageEventArgs> MessageReceived;

            /// <summary>
            /// 當有新的客戶端連線時觸發的事件
            /// </summary>
            public event EventHandler<WebSocketConnectionEventArgs> ClientConnected;

            /// <summary>
            /// 當客戶端連線關閉時觸發的事件
            /// </summary>
            public event EventHandler<WebSocketConnectionEventArgs> ClientDisconnected;

            /// <summary>
            /// 當發生錯誤時觸發的事件
            /// </summary>
            public event EventHandler<WebSocketErrorEventArgs> ErrorOccurred;

            /// <summary>
            /// 建構子
            /// </summary>
            /// <param name="host">監聽的主機</param>
            /// <param name="port">監聽的端口</param>
            public Server(int port = 6500)
            {
                // 設定監聽端點，監聽所有IP上的 port 端口
                _url = $"http://+:{port}/";
                _isRunning = false;
                _cts = new CancellationTokenSource();
            }

            /// <summary>
            /// 啟動 WebSocket 伺服器
            /// </summary>
            public void Start()
            {
                if (_isRunning)
                    return;

                _listener = new HttpListener();
                _listener.Prefixes.Add(_url);
                _listener.Start();
                _isRunning = true;

                // 當伺服器啟動並開始監聽時，設定 TaskCompletionSource 為成功
                startCompletionSource.SetResult(true);
                LogMessage($"WebSocket 伺服器啟動於 {_url}");

                // 開始接受連線
                Task.Run(AcceptConnectionsAsync);
            }

            /// <summary>
            /// 停止 WebSocket 伺服器
            /// </summary>
            public void Stop()
            {
                if (!_isRunning)
                    return;

                _cts.Cancel();
                _listener.Stop();
                _isRunning = false;

                LogMessage("WebSocket 伺服器已停止");
            }

            /// <summary>
            /// 這個方法用來等待伺服器啟動並開始監聽
            /// </summary>
            /// <returns></returns>
            public Task WaitForServerToStartAsync()
            {
                return startCompletionSource.Task; // 回傳 Task，讓外部程式可以 await 這個 Task，直到伺服器啟動完成
            }

            /// <summary>
            /// 發送消息至指定客戶端
            /// </summary>
            public async Task SendToClient(WebSocket _client, string message)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                if (_client.State == WebSocketState.Open)
                {
                    try
                    {
                        await _client.SendAsync(
                            new ArraySegment<byte>(buffer),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred(new WebSocketErrorEventArgs(ex, _client));
                    }
                }
            }

            /// <summary>
            /// 向所有客戶端發送訊息
            /// </summary>
            /// <param name="message">要發送的訊息</param>
            public async Task SendToAllClients(string message)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);

                foreach (var client in connect_clients)
                {
                    if (blacklist.Contains(client))continue;        // 跳過黑名單

                    if (client.State == WebSocketState.Open)
                    {
                        try
                        {
                            await client.SendAsync(
                                new ArraySegment<byte>(buffer),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None);
                        }
                        catch (Exception ex)
                        {
                            OnErrorOccurred(new WebSocketErrorEventArgs(ex, client));
                        }
                    }
                }
            }

            /// <summary>
            /// 將指定客戶端加入黑名單，廣播時便不會接收內容
            /// </summary>
            /// <param name="client"></param>
            public void AddToBlacklist(WebSocket client)
            {
                blacklist.Add(client);
            }

            private async Task AcceptConnectionsAsync()
            {
                try
                {
                    while (_isRunning)
                    {
                        HttpListenerContext listenerContext = await _listener.GetContextAsync();

                        if (listenerContext.Request.IsWebSocketRequest)
                        {
                            // 處理 WebSocket 請求
                            _ = ProcessWebSocketRequestAsync(listenerContext);
                        }
                        else
                        {
                            // 回應非 WebSocket 請求
                            listenerContext.Response.StatusCode = 400;
                            listenerContext.Response.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!_cts.IsCancellationRequested)
                    {
                        OnErrorOccurred(new WebSocketErrorEventArgs(ex));
                    }
                }
            }

            private async Task ProcessWebSocketRequestAsync(HttpListenerContext listenerContext)
            {
                WebSocketContext webSocketContext = null;
                try
                {
                    // 接受 WebSocket 連線
                    webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: null);
                    WebSocket webSocket = webSocketContext.WebSocket;
                    connect_clients.Add(webSocket);

                    // 觸發連線事件
                    OnClientConnected(new WebSocketConnectionEventArgs(webSocket, listenerContext.Request.RemoteEndPoint));

                    // 處理連線
                    await HandleWebSocketConnectionAsync(webSocket);
                }
                catch (Exception ex)
                {
                    OnErrorOccurred(new WebSocketErrorEventArgs(ex, webSocketContext?.WebSocket));

                    // 關閉連線
                    if (webSocketContext?.WebSocket != null && webSocketContext.WebSocket.State != WebSocketState.Closed)
                    {
                        webSocketContext.WebSocket.Abort();
                    }
                }
            }

            private async Task HandleWebSocketConnectionAsync(WebSocket webSocket)
            {
                byte[] buffer = new byte[4096];

                try
                {
                    while (webSocket.State == WebSocketState.Open && !_cts.Token.IsCancellationRequested)
                    {
                        // 接收訊息
                        WebSocketReceiveResult result = await webSocket.ReceiveAsync(
                            new ArraySegment<byte>(buffer), _cts.Token);

                        // 處理關閉連線的請求
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseAsync(
                                WebSocketCloseStatus.NormalClosure,
                                "關閉連線",
                                CancellationToken.None);

                            OnClientDisconnected(new WebSocketConnectionEventArgs(webSocket));
                        }
                        else
                        {
                            // 處理收到的訊息
                            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                            Console.WriteLine("接收到客戶端消息" + message);
                            // 觸發訊息接收事件
                            OnMessageReceived(new WebSocketMessageEventArgs(webSocket, message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!_cts.IsCancellationRequested)
                    {
                        OnErrorOccurred(new WebSocketErrorEventArgs(ex, webSocket));
                    }
                }
                finally
                {
                    // 確保關閉 WebSocket
                    if (webSocket.State != WebSocketState.Closed && !_cts.IsCancellationRequested)
                    {
                        try
                        {
                            await webSocket.CloseAsync(
                                WebSocketCloseStatus.NormalClosure,
                                "關閉連線",
                                CancellationToken.None);
                        }
                        catch { }

                        OnClientDisconnected(new WebSocketConnectionEventArgs(webSocket));
                    }
                }
            }

            private void LogMessage(string message)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            }

            protected virtual void OnMessageReceived(WebSocketMessageEventArgs e)
            {
                MessageReceived?.Invoke(this, e);
            }

            protected virtual void OnClientConnected(WebSocketConnectionEventArgs e)
            {
                ClientConnected?.Invoke(this, e);
            }

            protected virtual void OnClientDisconnected(WebSocketConnectionEventArgs e)
            {
                ClientDisconnected?.Invoke(this, e);
            }

            protected virtual void OnErrorOccurred(WebSocketErrorEventArgs e)
            {
                ErrorOccurred?.Invoke(this, e);
            }
        }

        public class WebSocketMessageEventArgs : EventArgs
        {
            public WebSocket Client { get; }
            public string Message { get; }

            public WebSocketMessageEventArgs(WebSocket webSocket, string message)
            {
                Client = webSocket;
                Message = message;
            }
        }

        public class WebSocketConnectionEventArgs : EventArgs
        {
            public WebSocket Client { get; }
            public IPEndPoint RemoteEndPoint { get; }

            public WebSocketConnectionEventArgs(WebSocket webSocket, IPEndPoint remoteEndPoint = null)
            {
                Client = webSocket;
                RemoteEndPoint = remoteEndPoint;
            }
        }

        public class WebSocketErrorEventArgs : EventArgs
        {
            public Exception Exception { get; }
            public WebSocket Client { get; }

            public WebSocketErrorEventArgs(Exception exception, WebSocket webSocket = null)
            {
                Exception = exception;
                Client = webSocket;
            }
        }
    }
        
}