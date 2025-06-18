using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using iCAPS;
using System.Windows.Documents;
using System.Net.Sockets;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Chump_kuka.Controls;
using Chump_kuka.Dispatchers;
using Newtonsoft.Json;
using Chump_kuka.Services;
using System.Drawing.Drawing2D;

namespace Chump_kuka
{
    public partial class Form1 : iCAPS.Form1
    {
        private UdpChatRoom _udp_chat_room = new UdpChatRoom();
        private SubBubble _sub_bubble;
        public Form1()
        {
            InitializeComponent();
            
            // Debug模式下，手動開啟 api 連線
            if (!Debugger.IsAttached) { enable_api_btn.Visible = false; }

            _udp_chat_room.Show();
            _udp_chat_room.Hide();

            Load += Form1_Load;
            SizeChanged += Form1_SizeChanged;

            //string binPath = Path.Combine(Application.StartupPath, "config\\layout.ini");
            //MessageBox.Show("Bin 資料夾路徑：" + binPath);
            _sub_bubble = new SubBubble(this);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                _sub_bubble.Show();
                this.Hide();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Env.enble_kuka_api = true;
            //KukaApiController.Enable = true;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            logWindow1.Visible = open_log_button.Checked;
        }

        
        private void btnUdpLog_Click(object sender, EventArgs e)
        {
            if (_udp_chat_room.Visible)
            {
                _udp_chat_room.Hide();
            }
            _udp_chat_room.Show();
        }
    }
}
