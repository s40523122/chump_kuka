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
                    Container container = new Container() { ContainerName = node[i], Size = container1.Size };
                    container.ContainerClick += Container_ContainerClick;
                    flowLayoutPanel1.Controls.Add(container);
                }
            }
        }

        private string[] node = new string[] { };

        // 定義事件，使用自定義參數
        public event EventHandler<ContainerClickEventArgs> ContainerClick;

        public kuka_area()
        {
            InitializeComponent();
            SizeChanged += Kuka_area_SizeChanged;
        }
        private void Container_ContainerClick(object sender, ContainerClickEventArgs e)
        {
            ContainerClick?.Invoke(this, e);
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

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {


        }
    }
}
