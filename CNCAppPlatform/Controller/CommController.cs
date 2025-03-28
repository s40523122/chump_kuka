using CefSharp.DevTools.CSS;
using Chump_kuka.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chump_kuka.Controller
{
    internal class CommController
    {
        private static UdpDispatcher _udp_listener;
        private static bool _is_server = false;

        public static void Init(bool  is_server, IPEndPoint listen_info)
        {
            _is_server = is_server;

            _udp_listener?.Close();
            _udp_listener = new UdpDispatcher(listen_info);
            if (!is_server)
                _udp_listener?.JoinGroup();

            _udp_listener.MessageReceived += _udp_listener_MessageReceived;
        }

        private static void _udp_listener_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {
            MessageBox.Show($"{e.Client.ToString()} : {e.Message}");
        }

        public static void Send(string msg)
        {
            if (_is_server)
            {
                _udp_listener?.SendToGroup(msg);
            }
            else
            {
                _udp_listener?.SendToServer(msg);
            }
        }
    }
}
