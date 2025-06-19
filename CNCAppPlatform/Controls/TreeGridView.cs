using iCAPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka.Controls
{
    public partial class TreeGridView : UserControl
    {

        private string[] _columns = new string[0];
        private float[] _column_ratios = new float[0];

        [Description("加入資料。"), Category("自訂值")]
        public object[] DataSource 
        { 
            get => null;
            set
            {
                if (value == null) return;

                int mission_index = 1;      // 任務編號
                foreach (object obj in value)
                {
                    if (obj == null) return;

                    Type type = obj.GetType();
                    var properties = type.GetProperties();

                    List<string> prop_values = new List<string>();

                    foreach (var prop in properties)
                    {
                        //string name = prop.Name;
                        object prop_value = prop.GetValue(obj);
                        //Console.WriteLine($"{name} = {prop_value}");

                        prop_values.Add(prop_value.ToString() );
                    };
                    TreeGridItem item = new TreeGridItem()
                    {
                        ID = mission_index++,
                        Columns = prop_values.ToArray(),
                        ColumnRatios = _column_ratios,
                        Width = flowLayoutPanel1.Width - 6,
                        Height = panel1.Height,
                        BackColor = Color.FromArgb(224, 224, 224),
                        Margin = new Padding(0, 3, 6, 3)
                    };
                    flowLayoutPanel1.Controls.Add(item);

                    item.RemoveItem += Item_RemoveItem;
                }
            }
        }

        [Description("加入資料欄。"), Category("自訂值")]
        public string[] Columns
        {
            get => _columns;
            set
            {
                _columns = (string[])value.Clone();
                if (value.Length == 0) return;
                Array.Reverse(value);
                Array.Reverse(_column_ratios);

                panel1.Controls.Clear();

                int title_length = panel1.Width - 6;
                scaleLabel1.Width = (int)(title_length * 0.08);

                int index = 0;
                foreach (string col in value)
                {
                    string content = col;
                    float ratio = 1.0f / value.Length;
                    if (_column_ratios.Length > index)
                    {
                        ratio = _column_ratios[index++];
                    }

                    ScaleLabel sLabel = new ScaleLabel()
                    {
                        Text = content,
                        Font = scaleLabel1.Font,
                        ForeColor = scaleLabel1.ForeColor,
                        TextAlign = scaleLabel1.TextAlign,
                        Factor = scaleLabel1.Factor,
                        Dock = DockStyle.Left,
                        Width = (int)((title_length - scaleLabel1.Width) * ratio),
                    };
                    panel1.Controls.Add(sLabel);
                }

                panel1.Controls.Add(scaleLabel1);
            }
        }

        [Description("資料欄比例。"), Category("自訂值")]
        public float[] ColumnRatios
        {
            get => _column_ratios;
            set
            {
                _column_ratios = value;
                Columns = _columns;     // 更新欄位顯示
            }
        }

        public TreeGridView()
        {
            InitializeComponent();
            Resize += TreeGridItem_Resize;
        }

        private void TreeGridItem_Resize(object sender, EventArgs e)
        {
            Columns = _columns;
        }
        private void Item_RemoveItem(object sender, EventArgs e)
        {
            TreeGridItem item = sender as TreeGridItem;
            DialogResult check = MessageBox.Show($"確認移除任務[{item.ID}] =>\n 從 [{item.Columns[0]}] 到 [{item.Columns[1]}]", "移除任務確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes)
            {
                item.Dispose();
            }
        }
    }
}
