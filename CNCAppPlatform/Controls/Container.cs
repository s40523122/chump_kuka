using Chump_kuka.Controls;
using Chump_kuka.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    public partial class Container : UserControl
    {
        private bool _lock = false;
        private Color _origin_back_color = Color.CadetBlue;     // 預設背景顏色
        private Color _replace_back_color;      //  外部修改背景顏色

        [Description("綁定模型資料"), Category("自訂值")]
        public dynamic BindingModel { get; set; } = null;

        [Description("容器名稱。"), Category("自訂值")]
        public string ContainerName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Description("容器圖片。"), Category("自訂值")]
        public Image ContainerImage
        {
            get { return doubleImg1.BackgroundImage; }
            set
            {
                try
                {
                    doubleImg1.BackgroundImage = value; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        [Description("容器狀態文字。"), Category("自訂值")]
        public override String Text
        {
            get => label2.Text; 
            set => label2.Text = value;
        }

        [Description("表示元件是否為已核取狀態。"), Category("自訂值")]
        public bool Checked
        {
            get { return _checked; }
            set 
            {
                if (_checked == value) return;
                _checked = value;
                if (_checked)
                {
                    doubleImg1.BackColor = Color.MediumSpringGreen;
                    panel2.BackColor = Color.OrangeRed;
                    if (Text == "")
                        Text = "已選定";
                }
                else 
                { 
                    doubleImg1.BackColor = (_replace_back_color == null) ? _origin_back_color : _replace_back_color;
                    ShowLock = _lock;       // 保持鎖定狀態
                    panel2.BackColor = Color.DarkOrange;
                    if (Text == "已選定")
                        Text = "";
                }
            }
        }
        private bool _checked = false;

        [Description("是否顯示鎖。"), Category("自訂值")]
        public bool ShowLock
        { 
            get => _lock;
            set 
            {
                _lock = value;
                
                if (_lock) doubleImg1.BackColor = Color.DeepSkyBlue;
                else doubleImg1.BackColor = (_replace_back_color == null) ? _origin_back_color : _replace_back_color;

                doubleImg1.Change = _lock;
            }
        }

        
        public Color ImgColor 
        { 
            get => doubleImg1.BackColor;
            set
            {
                _replace_back_color = value;
                doubleImg1.BackColor = _replace_back_color;
            }
        }

        public string Type { get { return "NODE_POINT"; } }

        // 定義事件，使用自定義參數
        public event EventHandler<ControlClickEventArgs> ContainerClick;

        public Container()
        {
            InitializeComponent();

            SizeChanged += Container_SizeChanged;
            
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            panel2.Width = panel2.Height;
        }

        private void doubleImg1_Click(object sender, EventArgs e)
        {
            Checked = !Checked;

            // 觸發事件，並傳遞按鈕資訊
            ContainerClick?.Invoke(this, new ControlClickEventArgs(ContainerName, this));
        }

    }
}
