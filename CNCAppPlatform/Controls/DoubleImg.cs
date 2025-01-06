using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ART_plus
{
    class DoubleImg : PictureBox 
    {
        [Description("欲存放於 PictureBox 預備區的影像。"), Category("自訂值")]
        public Image SubImg { get; set; } = null;

        private bool change = false;

        [Description("是否切換影像。"), Category("自訂值")]
        public bool Change
        {
            get { return change; }
            set
            {
                change = value;
                Tag = Image;
                Image = SubImg;
                SubImg = (Image)Tag;
                Refresh();
            }
        }

        private bool setSquare = false;

        [Description("是否指定影像為方形。"), Category("自訂值")]
        public bool SetSquare
        {
            get { return setSquare; }
            set
            {
                setSquare = value;
                Squaring();
            }
        }
        
        public DoubleImg()
        {
            SizeChanged += DoubleImg_SizeChanged;
            Click += DoubleImg_Click;
        }

        private void DoubleImg_Click(object sender, EventArgs e)
        {
            Change = !Change;
        }

        private void Squaring()
        {
            if (setSquare)
            {
                int squareSize = Math.Min(Width, Height);
                Size = new Size(squareSize, squareSize);
            }
        }

        private void DoubleImg_SizeChanged(object sender, EventArgs e)
        {
            Squaring();
        }
    }
}
