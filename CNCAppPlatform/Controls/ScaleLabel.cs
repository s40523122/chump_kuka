using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCAPS
{
    public class ScaleLabel : Label
    {
        [Description("容器名稱。"), Category("自訂值")]
        public float Factor
        {
            get { return _factor; }
            set 
            {
                _factor = value;
                Font = new Font(Font.FontFamily, Height * _factor, Font.Style);
            }
        }
        private float _factor = 0.5f;

        public ScaleLabel()
        {
            SizeChanged += ScaleLabel_SizeChanged;
        }

        private void ScaleLabel_SizeChanged(object sender, EventArgs e)
        {
            Factor = _factor;
        }
    }
}
