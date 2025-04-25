using Chump_kuka.Controller;
using iCAPS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chump_kuka.Controls
{
    public partial class UdpChatRoom : Form
    {
        private List<UdpLog> _udp_logs = new List<UdpLog>() { new UdpLog("127.0.0.1", "Hello")};
        private BindingSource _binding_source = new BindingSource();
        private List<RepeatMsg> _repeat_count = new List<RepeatMsg>();
        public UdpChatRoom()
        {
            InitializeComponent();
            _binding_source.DataSource = _udp_logs;
            //receiveGridView.DataSource = _binding_source;
            //receiveGridView.DataSource = new {IP="127.0.0.1", Message="Hello", Time="2024/03/01 08:00:00"};
            //receiveGridView.Refresh();

            ListViewItem item = new ListViewItem("Host");
            item.SubItems.Add(ChatController.HostIP);
            onlineUsers.Items.Add(item);
            //newUser();


            FormClosing += Form1_FormClosing;
            
            ChatController.MessageReceived += ChatController_MessageReceived;
        }

        private void ChatController_MessageReceived(object sender, Services.Managers.MessageIPEventArgs e)
        {            
            this.Invoke(new Action(() =>
            {
                string client_ip = e.Client.Address.ToString();
                string client_msg = e.Message;

                //ListViewItem item = new ListViewItem("訪客");
                //item.SubItems.Add(client_ip);
                //onlineUsers.Items.Add(item);

                // 解析回應字串
                if (client_msg.StartsWith("{\r\n  \"Type\": \"robot\""))
                {
                    RepeatCount("robot_status");
                }
                else
                {
                    // TODO 待解決重複訪客問題
                    // RemoteMessage(new UdpLog(client_ip, client_msg));
                }
            }));
            
        }

        public void newUser()
        {
            Random rnd = new Random();

            // 常見 emoji 範圍：U+1F600 ~ U+1F64F（表情符號區）
            int codePoint = rnd.Next(0x1F400, 0x1F4A0 + 1);

            // 將 Unicode 編碼轉成字元（用 char[] 接收 surrogate pair）
            string emoji = char.ConvertFromUtf32(codePoint);

            onlineUsers.View = View.Details;

            ListViewItem item = new ListViewItem(emoji);
            item.SubItems.Add("127.0.0.1");
            onlineUsers.Items.Add(item);
        }

        /// <summary>
        /// 攔截關閉事件，改為隱藏表單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 如果是使用者按了右上角 X 關閉
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;       // 取消關閉動作
                this.Hide();           // 隱藏表單
            }
        }

        public void LocalMessage(UdpLog udp_log)
        {
            FlowLayoutPanel msg_panel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Right,
                FlowDirection = FlowDirection.TopDown,
            };
            Label ip_txt = new Label()
            {
                Text = udp_log.IP,
                Font = localIpDemo.Font,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };
            myPanel msg_bubble = new myPanel()
            {
                BackColor = Color.FromArgb(255, 224, 192),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, 15, 0),
                Anchor = AnchorStyles.Right,
                MaximumSize = new Size(200, 0)
            };
            Label msg = new Label()
            {
                Text = udp_log.Message,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            Label time_txt = new Label()
            {
                Text = udp_log.Time.Substring(5),
                Font = localTimeDemo.Font,
                ForeColor = localTimeDemo.ForeColor,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 0, 15, 0)
            };
            msg_bubble.Controls.Add(msg);
            msg_panel.Controls.AddRange(new Control[] { ip_txt, msg_bubble, time_txt });

            chatroom.Controls.Add(msg_panel);

        }

        public void RemoteMessage(UdpLog udp_log)
        {
            FlowLayoutPanel msg_panel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.TopDown,
            };
            Label ip_txt = new Label()
            {
                Text = udp_log.IP,
                Font = remoteIpDemo.Font,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            myPanel msg_bubble = new myPanel()
            {
                BackColor = Color.FromArgb(128, 255, 128),
                AutoSize = true,
                AutoSizeMode=AutoSizeMode.GrowAndShrink,
                Margin = new Padding(15, 0, 0, 0),
                Anchor = AnchorStyles.Left,
                MaximumSize = new Size(200, 0)
            };
            Label msg = new Label()
            {
                Text = udp_log.Message,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            Label time_txt = new Label()
            {
                Text = udp_log.Time.Substring(5),
                Font = remoteTimeDemo.Font,
                ForeColor = remoteTimeDemo.ForeColor,
                AutoSize = true,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(15, 0, 0, 0)
            };
            msg_bubble.Controls.Add(msg);
            msg_panel.Controls.AddRange(new Control[] { ip_txt, msg_bubble, time_txt });

            chatroom.Controls.Add(msg_panel);
        }

        public void RepeatCount(string repeat_title)
        {
            RepeatMsg repeat = _repeat_count.FirstOrDefault(p => p.Key == repeat_title);
            if (repeat != null) 
            {
                repeat.Value++;
                repeat.Label.Text = repeat.Value.ToString();
            }
            else
            {
                Panel panel = new Panel()
                {
                    BorderStyle = BorderStyle.Fixed3D,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                };
                Label title = new Label()
                {
                    Text = repeat_title,
                    Font = replyTitleDemo.Font,
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Padding = new Padding(3)
                };
                Label count = new Label()
                {
                    Text = "1",
                    Font = replyCountDemo.Font,
                    ForeColor = replyCountDemo.ForeColor,
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    Padding = new Padding(3)
                };

                panel.Controls.AddRange(new Control[] { title, count });

                flowLayoutPanel2.Controls.Add(panel);

                _repeat_count.Add(new RepeatMsg(repeat_title, 1, count));
            }
            
        }

        private class RepeatMsg
        {
            public string Key { get; set; }
            public int Value { get; set; }
            public Label Label { get; set; }
            public RepeatMsg(string key, int value, Label label)
            {
                Key = key; 
                Value = value; 
                Label = label;
            }
        }

        public class UdpLog
        {
            public string IP { get; set; }
            public string Message { get; set; }
            public string Time { get; set; }
            public UdpLog(string ip, string message)
            {
                IP = ip;
                Message = message;
                Time = DateTime.Now.ToString(@"g");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //UdpLog udp_log = new UdpLog("127.0.0.1", messageBox.Text);
            //RemoteMessage(udp_log);
        }
    }
}
