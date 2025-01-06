using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Controls
{
    public partial class kuka_area : UserControl
    {
        [Description("區域名稱。"), Category("自訂值")]
        public string AreaName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Description("區域中的節點。"), Category("自訂值")]
        public string[] AreaNode
        {
            get { return node; }
            set 
            {
                flowLayoutPanel1.Controls.Clear();
                if (value == null) return;
                node = value; 
                for (int i = 0; i < node.Length; i++)
                {
                    flowLayoutPanel1.Controls.Add(new Container() { ContainerName = node[i], Size = container1.Size});
                }
            }
        }


        private string[] node = new string[] { };

        public kuka_area()
        {
            InitializeComponent();
            SizeChanged += Kuka_area_SizeChanged;
        }

        private void Kuka_area_SizeChanged(object sender, EventArgs e)
        {
            container1.Height = flowLayoutPanel1.Height / 2 - 10;
            container1.Width = (int)(container1.Height * 0.6);

            foreach (Container container in flowLayoutPanel1.Controls)
            {
                container.Size = container1.Size;
            }
        }
    }
}
