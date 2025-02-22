using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Chump_kuka.Controls
{
    

    public partial class LogWindow : UserControl
    {
        public LogWindow()
        {
            InitializeComponent();
            Log.LogChanged += LogChanged;
        }

        private void LogChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            List<string> log_info = Log.LogInfo;
            logView.AddRow(log_info);
        }


    }
}
