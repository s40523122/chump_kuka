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
    public partial class TreeGridRow : UserControl
    {
        bool _isExpanded = false;
        private string[] _items = new string[0];
        private float[] _col_ratios = new float[0];
        private string _log_msg = string.Empty;
        private Panel _panel;       // 展開空間，當展開時，建立新 Panel，若折疊時銷毀，減低系統負擔

        public event EventHandler RemoveItem;       // 移除事件

        [Description("是否顯示自動ID。"), Category("自訂值")]
        public bool AutoIDVisible
        {
            get => scaleLabel1.Visible;
            set
            {
                scaleLabel1.Visible = value;
            }
        }

        [Description("項目編號。"), Category("自訂值")]
        public int ID
        {
            get => int.Parse(scaleLabel1.Text);
            set
            {
                scaleLabel1.Text = value.ToString();
            }
        }

        [Description("加入資料項目。"), Category("自訂值")]
        public string[] Items 
        { 
            get => _items;
            set
            {
                _items = (string[])value.Clone();
                if (value.Length == 0) return;
                Array.Reverse(value);
                Array.Reverse(_col_ratios);

                panel1.Controls.Clear();

                // 設定首行自動 ID 寬度
                int auto_id_width = scaleLabel1.Width = scaleLabel1.Visible ? (int)(panel1.Width * 0.08) : 0;

                int index = 0;
                foreach (string col in value)
                {
                    string content = col;
                    float ratio = 1.0f / value.Length;
                    if (_col_ratios.Length > index)
                    {
                        ratio = _col_ratios[index++];
                    }

                    ScaleLabel sLabel = new ScaleLabel() 
                    { 
                        Text = content,
                        Font = scaleLabel1.Font, 
                        TextAlign = scaleLabel1.TextAlign, 
                        Factor = scaleLabel1.Factor, 
                        Dock = DockStyle.Left,
                        Width = (int)((panel1.Width- auto_id_width) * ratio),
                        Cursor = Cursors.Hand
                    };
                    sLabel.Click += (s, e) => TreeGridItem_Click(s, e);
                    panel1.Controls.Add(sLabel);
                }

                Array.Reverse(_col_ratios);      // 反轉回來，避免資料錯誤

                panel1.Controls.Add(scaleLabel1);
            } 
        }

        [Description("資料欄比例。"), Category("自訂值")]
        public float[] ColumnRatios 
        {
            get => _col_ratios;
            set
            {
                _col_ratios = value;
                Items = _items;     // 更新欄位顯示
            }
        }

        [Description("Log 訊息。"), Category("自訂值")]
        public string LogMsg
        {
            get => _log_msg;
            set
            {
                _log_msg = value;
            }
        }

        public TreeGridRow()
        {
            InitializeComponent();

            panel1.Size = this.Size;
            Resize += TreeGridItem_Resize;
            Click += TreeGridItem_Click;
        }

        private void TreeGridItem_Click(object sender, EventArgs e)
        {
            _isExpanded = !_isExpanded;

            if (_isExpanded)
            {
                this.Height = (int)(panel1.Height * 3.5);
                _panel = new Panel()
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Padding = new Padding(5),
                };
                this.Controls.Add(_panel);
                _panel.BringToFront();

                Button btn_rm = new Button()
                {
                    Text = "刪除任務",
                    Font = new Font(scaleLabel1.Font.FontFamily, (float)(panel1.Height * 0.22), FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.Salmon,
                    Size = new Size(panel1.Width/6, (int)(panel1.Height * 0.8)),
                    FlatStyle = FlatStyle.Flat,
                    Padding = new Padding(0),
                    Cursor = Cursors.Hand,
                };
                btn_rm.FlatAppearance.BorderSize = 0;
                btn_rm.Location = new Point(_panel.Width-btn_rm.Width-15, _panel.Height-btn_rm.Height-5);
                btn_rm.Click += (_s, _e) => RemoveItem?.Invoke(this, _e);       // 觸發刪除事件
                _panel.Controls.Add(btn_rm);

                Button btn_resend = new Button()
                {
                    Text = "重送任務",
                    Font = btn_rm.Font,
                    ForeColor = Color.Black,
                    BackColor = Color.LightSkyBlue,
                    Size = btn_rm.Size,
                    FlatStyle = FlatStyle.Flat,
                    Padding = new Padding(0),
                    Cursor = Cursors.Hand,

                };
                btn_resend.FlatAppearance.BorderSize = 0;
                btn_resend.Left = btn_rm.Left - btn_resend.Width - 5;
                btn_resend.Top = btn_rm.Top;
                _panel.Controls.Add(btn_resend);

                RichTextBox log_msg_box = new RichTextBox()
                {
                    Dock = DockStyle.Left,
                    Width = (int)(_panel.Width * 0.6),
                    Text = _log_msg,
                    ReadOnly = true,
                };
                log_msg_box.Location = new Point(5, 5);
                _panel.Controls.Add(log_msg_box);

            }
            else
            {
                this.Height = panel1.Height;
                _panel.Dispose();   // 釋放資源
            }

        }

        private void TreeGridItem_Resize(object sender, EventArgs e)
        {
            panel1.Height = _isExpanded ? (int)(this.Height/3.5) : this.Height;
            Items = _items;
        }
    }
}
