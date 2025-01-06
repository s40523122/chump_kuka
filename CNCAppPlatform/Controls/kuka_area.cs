using iCAPS;
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

        [Description("表示元件是否為已核取狀態。"), Category("自訂值")]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                switch (_checked)
                {
                    case true:
                        myPanel2.BackColor = Color.MediumSpringGreen;
                        break;
                    case false:
                        myPanel2.BackColor = SystemColors.ControlLight;
                        break;
                }
            }
        }
        private bool _checked = false;

        private string[] node = new string[] { };

        // 定義事件，使用自定義參數
        public event EventHandler<ControlClickEventArgs> ContainerClick;
        public event EventHandler<ControlClickEventArgs> AreaClick;

        public kuka_area()
        {
            InitializeComponent();
            SizeChanged += Kuka_area_SizeChanged;
        }
        private void Container_ContainerClick(object sender, ControlClickEventArgs e)
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
            Checked = !Checked;
            // 觸發事件，並傳遞按鈕資訊
            AreaClick?.Invoke(this, new ControlClickEventArgs(Name, this));

        }
    }
}
