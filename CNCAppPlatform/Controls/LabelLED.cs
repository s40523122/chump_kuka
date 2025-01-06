using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    public partial class LabelLED : UserControl
    {
        private bool _isLight = true;
        [Description("是否正在加工。"), Category("自訂值")]
        public bool IsLight
        {
            get { return _isLight; }
            set
            {
                _isLight = value;
                if (_isLight)
                {
                    BackColor = Color.Lime;
                    label1.Text = "已連線";
                }
                else
                {
                    BackColor = Color.DarkGray;
                    label1.Text = "中斷連線";
                }

            }
        }

        public LabelLED()
        {
            InitializeComponent();
            SizeChanged += LabelLED_SizeChanged;
        }

        private void LabelLED_SizeChanged(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.FontFamily, Height * 36 / 123, label1.Font.Style); ;
        }
    }
}
