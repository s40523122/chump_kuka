using iCAPS.Managers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chump_kuka.Services.Managers
{
    internal class UdpManager
    {
        private UdpClient _udp_client;
        //private static UdpClient _static_client = new UdpClient();      // 靜態發布使用的客戶端，透過靜態發布即便不監聽也可以發布
        private int _listen_port;

        public bool Enable {  get; set; } = false;

        public event EventHandler<MessageIPEventArgs> MessageReceived;     // 接收客戶端訊息時觸發的事件


        /// <summary>
        /// 建立 UDP 監聽器
        /// </summary>
        /// <param name="port">監聽的 Port</param>
        public UdpManager(string host_ip, int port)
        {
            _listen_port = port;
            this._udp_client = new UdpClient(port);

            IPAddress localAddress = IPAddress.Parse(host_ip);
            _udp_client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, localAddress.GetAddressBytes());
            _udp_client.Ttl = 255; // 或更大值


            Enable = true;
        }

        /// <summary>
        /// 開始監聽 UDP 訊息（適用於 Unicast, Broadcast, Multicast）
        /// </summary>
        public void StartListening()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        UdpReceiveResult result = await _udp_client.ReceiveAsync();
                        string message = Encoding.UTF8.GetString(result.Buffer);
                        MessageReceived?.Invoke(this, new MessageIPEventArgs(result.RemoteEndPoint, message));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        Enable = false;
                    }
                    
                }
            });
        }

        /// <summary>
        /// 發送 UDP 訊息（Unicast 單播）
        /// </summary>
        /// <param name="ip">目標 IP</param>
        /// <param name="port">目標 Port</param>
        /// <param name="message">要發送的訊息</param>
        public void SendUnicast(string ip, int port, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            // _static_client.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(ip), port));
            _udp_client.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(ip), port));
        }

        /// <summary>
        /// 發送 UDP 廣播（Broadcast）
        /// </summary>
        /// <param name="port">目標 Port</param>
        /// <param name="message">要發送的訊息</param>
        public void SendBroadcast(int port, string message)
        {

            //_static_client.EnableBroadcast = true; // 開啟廣播
            //byte[] data = Encoding.UTF8.GetBytes(message);
            //_static_client.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, port));
            //_static_client.EnableBroadcast = false;

            _udp_client.EnableBroadcast = true; // 開啟廣播
            byte[] data = Encoding.UTF8.GetBytes(message);
            _udp_client.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, port));
            _udp_client.EnableBroadcast = false;

        }

        /// <summary>
        /// 發送 UDP 組播（Multicast）
        /// </summary>
        /// <param name="multicastIp">組播 IP（範圍 224.0.0.0 ~ 239.255.255.255）</param>
        /// <param name="port">目標 Port</param>
        /// <param name="message">要發送的訊息</param>
        public void SendMulticast(string multicastIp, int port, string message)
        {
            try
            {
                IPAddress multicastAddress = IPAddress.Parse(multicastIp);
                IPEndPoint remoteEP = new IPEndPoint(multicastAddress, port);

                byte[] data = Encoding.UTF8.GetBytes(message);
                // IPAddress localAddress = IPAddress.Parse("192.168.68.64");

                //_static_client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, localAddress.GetAddressBytes());
                //_static_client.Ttl = 255; // 或更大值
                //_static_client.Send(data, data.Length, remoteEP);

                _udp_client.Send(data, data.Length, remoteEP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Enable = false;
            }

        }

        /// <summary>
        /// 加入 UDP 組播監聽
        /// </summary>
        /// <param name="multicastIp">組播 IP</param>
        public void JoinMulticast(string host_ip, string multicastIp)
        {
            IPAddress localInterface = IPAddress.Parse(host_ip); // 你有線網卡的 IP
            IPAddress multicastAddress = IPAddress.Parse(multicastIp);
            _udp_client.JoinMulticastGroup(multicastAddress, localInterface);
        }

        /// <summary>
        /// 關閉 UDP 監聽
        /// </summary>
        public void Stop()
        {
            _udp_client?.Close();
        }
    }

    public class MessageIPEventArgs : EventArgs
    {
        public IPEndPoint Client { get; }
        public string Message { get; }

        public MessageIPEventArgs(IPEndPoint client, string message)
        {
            Client = client;
            Message = message;
        }
    }
}
