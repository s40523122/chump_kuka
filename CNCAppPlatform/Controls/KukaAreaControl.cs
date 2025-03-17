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
    public partial class KukaAreaControl : UserControl
    {
        private string[] _nodes = new string[] { };
        private bool _checked = false;
        private int[] _node_status = new int[] { };

        // 定義事件，使用自定義參數
        public event EventHandler<ControlClickEventArgs> ContainerClick;
        public event EventHandler<ControlClickEventArgs> AreaClick;

        public string Type { get { return "NODE_AREA"; } }
        public string AreaCode = "";

        [Description("區域名稱。"), Category("自訂值")]
        public string AreaName
        {
            get => label1.Text; 
            set { label1.Text = value; }
        }

        [Description("區域中的節點。"), Category("自訂值")]
        public string[] AreaNode
        {
            get => _nodes; 
            set 
            {
                if (_nodes == value) return;        // 如果資訊未更新，不處理

                containerPanel.Controls.Clear();
                if (value == null) return;
                _nodes = value; 
                for (int i = 0; i < _nodes.Length; i++)
                {
                    Container container = new Container() { ContainerName = _nodes[i], Size = container1.Size };
                    container.ContainerClick += Container_ContainerClick;
                    containerPanel.Controls.Add(container);
                }
                _node_status = new int[_nodes.Length];
            }
        }

        [Description("表示元件是否為已核取狀態。"), Category("自訂值")]
        public bool Checked
        {
            get => _checked; 
            set
            {
                // 當控制項被點擊後，點亮外框
                _checked = value;
                switch (_checked)
                {
                    case true:
                        custom_border.BackColor = Color.MediumSpringGreen;
                        break;
                    case false:
                        custom_border.BackColor = SystemColors.ControlLight;
                        break;
                }
            }
        }

        [Description("設定節點狀態。"), Category("自訂值")]
        public int[] NodeStatus
        {
            get => _node_status; 
            set
            {
                if (value == _node_status) return;
                if (value.Length < _nodes.Length)
                {
                    MessageBox.Show("節點狀態與節點數量不吻合");
                    return;
                }
                UpdateContainerImage(value);
            }
        }
        public KukaAreaControl()
        {
            InitializeComponent();
            Controls.Remove(samplePanel);
            custom_border.Dock = DockStyle.Fill;
            SizeChanged += Kuka_area_SizeChanged;
        }

        /// <summary>
        /// 更新容器圖片
        /// </summary>
        /// <param name="container_status"></param>
        public void UpdateContainerImage(int[] container_status)
        {
            int i = 0;
            foreach (Container _container in containerPanel.Controls)
            {
                try
                {
                    switch (container_status[i++])
                    {
                        case 0:
                            // 無交換站
                            _container.ContainerImage = null;
                            break;
                        case 1:
                            // 有交換站 & 無料
                            _container.ContainerImage = doubleImg1.Image;
                            break;
                        case 2:
                            // 有交換站 & 有料
                            _container.ContainerImage = doubleImg1.SubImg;
                            break;
                    }
                }
                catch 
                {
                    // 輸入 container_status 數量少於區域內的容器
                }
            }
        }
        private void Container_ContainerClick(object sender, ControlClickEventArgs e)
        {
            ContainerClick?.Invoke(this, e);
        }

        private void Kuka_area_SizeChanged(object sender, EventArgs e)
        {
            container1.Height = (int)(containerPanel.Height * 0.47);
            container1.Width = (int)(container1.Height * 0.6);

            foreach (Container container in containerPanel.Controls)
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
