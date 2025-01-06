using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    public partial class myPanel : Panel
    {
        [Description("圓角半徑"), Category("自訂值")]
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                SetRegion();
            }
        }
        private int radius = 10;

        public myPanel()
        {
            //Paint += MyPanel_Paint;
            SetRegion();
            SizeChanged += MyPanel_SizeChanged;
        }

        private void MyPanel_SizeChanged(object sender, EventArgs e)
        {
            SetRegion();
        }

        private void SetRegion()
        {
            // 建立 GraphicsPath 物件，定義帶圓角的方形
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(radius, 0, Width - radius * 2, 0); // 上邊緣
                path.AddArc(Width - radius * 2, 0, radius * 2, radius * 2, 270, 90); // 右上角
                path.AddLine(Width, radius, Width, Height - radius * 2); // 右邊緣
                path.AddArc(Width - radius * 2, Height - radius * 2, radius * 2, radius * 2, 0, 90); // 右下角
                path.AddLine(Width - radius * 2, Height, radius, Height); // 下邊緣
                path.AddArc(0, Height - radius * 2, radius * 2, radius * 2, 90, 90); // 左下角
                path.AddLine(0, Height - radius * 2, 0, 0 + radius); // 左邊緣
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // 左上角
                path.CloseFigure(); // 關閉圖形

                Region = new Region(path);
            }
        }

        private void MyPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
