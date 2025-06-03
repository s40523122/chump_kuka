using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Chump_kuka.Controls
{
    public partial class SubBubble : Form
    {
        private Form _main_form;    // 主視窗
        public SubBubble(Form form)
        {
            InitializeComponent();

            _main_form = form;

            Load += SubBubble_Load;

            // 設定滑鼠事件以支援拖曳
            myPanel1.Click += Circle_form_Click;
            this.MouseDown += Form_MouseDown;
        }

        private void SubBubble_init()
        {
            this.StartPosition = FormStartPosition.Manual; // 手動設定位置
            this.FormBorderStyle = FormBorderStyle.None; // 去除邊框
            this.BackColor = Color.DodgerBlue;
            this.Width = 100;
            this.Height = 100;
            this.TopMost = true;     // 置於螢幕最上層
            this.Cursor = Cursors.NoMove2D;
            
            // 啟用雙緩衝與透明背景
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            this.BackColor = Color.DodgerBlue; // 用來當透明色
            this.TransparencyKey = Color.DodgerBlue; // 這個顏色會變透明
        }

        private void SubBubble_Load(object sender, EventArgs e)
        {
            SubBubble_init();

            // 計算右下角位置
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);

            // 修剪成圓形
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(path);

            Console.WriteLine($"{this.Width}, {this.Height}");
            // 預先產生圓形圖並指定為背景
            this.BackgroundImage = GenerateCircleBitmap(this.Width, this.Height, Color.DeepSkyBlue);
            this.BackgroundImageLayout = ImageLayout.None;
        }

        private Bitmap GenerateCircleBitmap(int width, int height, Color color)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, 0, 0, width - 1, height - 1);
                }
            }
            return bmp;
        }

        private void Circle_form_Click(object sender, EventArgs e)
        {
            _main_form.WindowState = FormWindowState.Maximized;
            _main_form.Show();
            this.Hide();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            int WM_NCLBUTTONDOWN = 0xA1;
            int HTCAPTION = 0x2;
            if (e.Button == MouseButtons.Left)
            {
                WindowControl.ReleaseCapture();
                WindowControl.SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }

    public class WindowControl
    {
        // 引入 Windows API
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
