using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Chump_kuka.Controls
{
    

    public partial class LogWindow : UserControl
    {
        private List<string> _log_labels = new List<string>() {"INFO", "WARN", "ERROR", "NOTICE", "ALERT" };

        // 拖曳用
        bool dragging = false;
        int dragStartY;

        public LogWindow()
        {
            InitializeComponent();
            Log.LogChanged += LogChanged;
        }

        private void LogChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            List<string> log_info = Log.LogInfo;
            log_info.Insert(0, (logView.RowCount+1).ToString());
            logView.AddRow(log_info);

            CreateLabel(log_info[2]);
        }

        private void CreateLabel(string log_label)
        {
            // 將第一次出現的 Log 標籤加入列表中
            if (!_log_labels.Contains(log_label))
            {
                _log_labels.Add(log_label);

                RadioButton new_label = new RadioButton()
                {
                    Appearance = Appearance.Button,
                    Size = checkBox1.Size,
                    Text = log_label,
                    Font = checkBox1.Font,
                    TextAlign = checkBox1.TextAlign,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(3, 15, 5, 3)
                };
                new_label.Click += checkBox_Click;
                flowLayoutPanel1.Controls.Add(new_label);
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            // MessageBox.Show((sender as RadioButton).Text);


        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStartY = Cursor.Position.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                int delta = Cursor.Position.Y - dragStartY;
                this.Height += delta;
                logView.Height += delta;
                dragStartY = Cursor.Position.Y;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
