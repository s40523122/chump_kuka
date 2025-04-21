using Chump_kuka.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chump_kuka.Dispatchers
{
    internal class UdpDispatcher
    {
        private string _host_ip;
        private UdpManager _udp_manager;
        private IPEndPoint _server_ipep;

        public bool IsStart
        {
            get => false;
        }

        public event EventHandler<MessageIPEventArgs> MessageReceived;     // 接收客戶端訊息時觸發的事件

        public UdpDispatcher(string host_ip, IPEndPoint server_ipep)
        {
            this._host_ip = host_ip;
            this._server_ipep = server_ipep;

            _udp_manager = new UdpManager(host_ip, server_ipep.Port);

            
            _udp_manager.StartListening();

            _udp_manager.MessageReceived += _udp_manager_MessageReceived; 
        }

        public void JoinGroup()
        {
            try
            {
                _udp_manager.JoinMulticast(_host_ip, "239.0.0.1"); // 加入組播群組
            }
            catch (Exception e)
            {
                MessageBox.Show("無法加入組播群組，因為" + e.Message);
                Close();
            }
            
        }

        private void _udp_manager_MessageReceived(object sender, MessageIPEventArgs e)
        {
            this.MessageReceived.Invoke(sender, e);
            //string recrive_msg = e.Message;
            //SendToServer("receive: " + recrive_msg);
        }

        public void SendToServer(string msg)
        {
            if (_server_ipep != null)
                _udp_manager.SendUnicast(_server_ipep.Address.ToString(), _server_ipep.Port, msg);
            else
                Log.Append("尚未指定 UDP 伺服器", "Error", "UdpDispatcher");
        }

        public void SendToGroup(string msg)
        {
            // 如果為伺服器，發送組播

            _udp_manager.SendMulticast("239.0.0.1", _server_ipep.Port, msg);
  
        }

        public void Close()
        {
            _udp_manager.Stop();
            _udp_manager = null;
        } 
    }
}
