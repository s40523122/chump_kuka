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
    

    public partial class LogWindow : UserControl
    {
        private List<string> _log_labels = new List<string>() {"INFO", "WARN", "ERROR", "NOTICE", "ALERT" };
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

            CreateLabel(log_info[1]);
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
                    Text = log_label
                };
                new_label.Click += checkBox_Click;
                flowLayoutPanel1.Controls.Add(new_label);
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            // MessageBox.Show((sender as RadioButton).Text);


        }
    }
}
