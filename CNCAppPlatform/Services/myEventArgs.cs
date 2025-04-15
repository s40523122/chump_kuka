using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chump_kuka
{
    public class ControlClickEventArgs : EventArgs
    {
        public string ControlName { get; }
        public object Control { get; }

        public ControlClickEventArgs(string controlName, object control)
        {
            ControlName = controlName;
            Control = control;
        }
    }

    public class TcpMessageEventArgs : EventArgs
    {
        public NetworkStream Stream { get; }
        public string Message { get; }

        public TcpMessageEventArgs(NetworkStream stream, string message)
        {
            Stream = stream;
            Message = message;
        }
    }

    public class TcpConnectionEventArgs : EventArgs
    {
        public TcpClient Client { get; }
        public IPEndPoint RemoteEndPoint { get; }

        public TcpConnectionEventArgs(TcpClient client, IPEndPoint remoteEndPoint = null)
        {
            Client = client;
            RemoteEndPoint = remoteEndPoint;
        }
    }

    public class TextEventArgs : EventArgs
    {
        public string Message { get; }

        public TextEventArgs(string msg)
        {
            Message = msg;
        }
    }

    public delegate void CarryTasksEventHandler(object sender, SimpleCarryTask[] e);

}
