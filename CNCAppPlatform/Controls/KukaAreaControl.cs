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
        private KukaAreaModel _model = new KukaAreaModel();
        private KukaNodeModel[] _nodes = new KukaNodeModel[] { };
        private bool _checked = false;
        private int[] _node_status = null;
        private bool _allow_click = true;
        private Color[] _container_colors = new Color[3] { Color.CadetBlue, Color.White, Color.Orange};

        // 定義事件，使用自定義參數
        public event EventHandler<ControlClickEventArgs> ContainerClick;
        public event EventHandler<ControlClickEventArgs> AreaClick;

        public Image[] ContainerImgs;
        public string Type { get { return "NODE_AREA"; } }
        public string AreaCode = "";

        public KukaAreaModel Model 
        { 
            get => _model;
            set
            {
                if (value != null)
                {
                    _model.PropertyChanged -= _model_PropertyChanged;       // 解除既有綁定事件
                    _model = value;     // 重新指定模型

                    AreaName = _model.AreaName;
                    AreaNode = _model.NodeList;
                    
                    _model.PropertyChanged += _model_PropertyChanged;       // 綁定新事件
                }
            } 
        }

        [Description("區域名稱。"), Category("自訂值")]
        public string AreaName
        {
            get => label1.Text; 
            set 
            { 
                if (Model.AreaName != value) Model.AreaName = value; 
                label1.Text = value;
            }
        }

        [Description("區域中是否可點擊。"), Category("自訂值")]
        public bool AllowClick 
        { 
            get => _allow_click;
            set
            {
                _allow_click = value;
                if (_allow_click)
                {
                    tableLayoutPanel1.Cursor = containerPanel.Cursor = label1.Cursor = Cursors.Hand;
                    tableLayoutPanel1.Click += flowLayoutPanel1_Click;
                    containerPanel.Click += flowLayoutPanel1_Click;
                    label1.Click += flowLayoutPanel1_Click;
                }
                else
                {
                    tableLayoutPanel1.Cursor = containerPanel.Cursor = label1.Cursor = Cursors.Default;
                    tableLayoutPanel1.Click -= flowLayoutPanel1_Click;
                    containerPanel.Click -= flowLayoutPanel1_Click;
                    label1.Click -= flowLayoutPanel1_Click;
                }
            } 
        }

        [Description("區域中的節點是否可點擊。"), Category("自訂值")]
        public bool AllowContainerClick{ get; set; } = true;

        [Description("區域中的節點是否顯示鎖圖示。"), Category("自訂值")]
        public bool AllowContainerLock { get; set; } = false;

        [Description("區域中的節點。"), Category("自訂值")]
        public KukaNodeModel[] AreaNode
        {
            get => _nodes;
            set
            {
                if (_nodes.SequenceEqual(value) || value == null) return;        // 如果資訊未更新，不處理

                containerPanel.Controls.Clear();
                _nodes = value;
                foreach (KukaNodeModel node in _nodes)
                {
                    Container container = new Container()
                    {
                        ContainerName = node.NodeName,
                        Size = container1.Size,
                        Enabled = AllowContainerClick
                    };

                    // 更新貨架狀態圖片
                    UpdateSingleContainerImage(container, node.RackStatus);        
                    container.ImgColor = _container_colors[Math.Max(node.NodeStatus, 0)];

                    node.PropertyChanged += (sender, e) =>
                    {
                        KukaNodeModel model = (sender as KukaNodeModel);
                        switch (e.PropertyName)
                        {
                            // 貨架狀態
                            case nameof(KukaNodeModel.RackStatus):
                                UpdateSingleContainerImage(container, model.RackStatus);        // 更新貨架狀態圖片
                                break;
                            // 節點狀態
                            case nameof(KukaNodeModel.NodeStatus):
                                container.ImgColor = _container_colors[model.NodeStatus];
                                break;
                        }
                    };

                    // container.ImageIndex = -1;
                    //UpdateSingleContainerImage(container, _nodes[i].RackStatus);
                    container.ContainerClick += Container_ContainerClick;
                    containerPanel.Controls.Add(container);

                    if (AllowContainerLock)
                    {
                        container.ShowLock = true;
                    }
                }
                // _node_status = new int[_nodes.Length];
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

        [Description("設定節點狀態。\n0: 無貨架\n1: 空載\n2: 滿載"), Category("自訂值")]
        public int[] NodeStatus
        {
            get => _node_status; 
            set
            {
                if (value == _node_status || value == null) return;
                _node_status = value;
                //if (value.Length < _nodes.Length)
                //{
                //    MessageBox.Show("節點狀態與節點數量不吻合");
                //    return;
                //}
                UpdateContainerImage(value);
            }
        }
        public KukaAreaControl()
        {
            InitializeComponent();
            ContainerImgs = new Image[3] { null, doubleImg1.Image, doubleImg1.SubImg };

            Controls.Remove(samplePanel);
            custom_border.Dock = DockStyle.Fill;
            SizeChanged += Kuka_area_SizeChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            KukaAreaModel model = (sender as KukaAreaModel);
            switch (e.PropertyName)
            {
                // 區域名稱
                case nameof(KukaAreaModel.AreaName):
                    AreaName = model.AreaName;
                    break;
                // 節點內容
                case nameof(KukaAreaModel.NodeList):
                    AreaNode = model.NodeList;
                    break;
            }
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

        public void UpdateSingleContainerImage(Container container, int container_status)
        {
            int i = 0;

            try
            {
                switch (container_status)
                {
                    case 0:
                        // 無交換站
                        container.ContainerImage = null;
                        break;
                    case 1:
                        // 有交換站 & 無料
                        container.ContainerImage = doubleImg1.Image;
                        break;
                    case 2:
                        // 有交換站 & 有料
                        container.ContainerImage = doubleImg1.SubImg;
                        break;
                }
                
            }
            catch
            {
                // 輸入 container_status 數量少於區域內的容器
            }
            
        }

        /// <summary>
        /// 更新容器圖片
        /// </summary>
        /// <param name="container_texts"></param>
        public void UpdateContainerText(string[] container_texts)
        {
            int i = 0;
            foreach (Container _container in containerPanel.Controls)
            {
                _container.Text = container_texts[i++];
            }
        }

        private void Container_ContainerClick(object sender, ControlClickEventArgs e)
        {
            ContainerClick?.Invoke(this, e);
        }

        public void ResetContainer()
        {
            foreach(Container rack in containerPanel.Controls)
            {
                rack.Checked = false;
            }
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
            if (!AllowClick) return;
            Checked = !Checked;
            // 觸發事件，並傳遞按鈕資訊
            AreaClick?.Invoke(this, new ControlClickEventArgs(Name, this));

        }

    }
}
