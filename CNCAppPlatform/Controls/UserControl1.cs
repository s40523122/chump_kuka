using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Chump_kuka.Controls
{
    public partial class UserControl1 : UserControl
    {

        private static Timer timer1 = new Timer();

        // 靜態事件，用於通知所有實例
        public static event EventHandler TimerTick;

        static UserControl1()
        {
            timer1 = new Timer();
            timer1.Interval = 3000; // 設定時間間隔（毫秒）
            timer1.Tick += Timer1_Tick;
            
        }
        private static string msg = string.Empty;
        private static string msg2 = string.Empty;
        private static void Timer1_Tick(object sender, EventArgs e)
        {

            msg = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            TimerTick?.Invoke(sender, e);
        }

        public UserControl1()
        {
            InitializeComponent();

            TimerTick += OnTimerTick;

            Timer timer2 = new Timer();
            timer2.Interval = 3000;
            timer2.Tick += Timer2_Tick;
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            msg2 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            label2.Text = msg2;
        }

        private void OnTimerTick(object Sender, EventArgs e)
        {
            // Set the caption to the current time.  
            label1.Text = msg;
        }

        private bool aa = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!aa)
            {
                timer1.Start();
                aa = true;
            }
            else timer1.Stop();

            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
