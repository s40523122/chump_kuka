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

namespace Chump_kuka.Forms
{
    public partial class f05_Web : Form
    {
        public f05_Web()
        {
            InitializeComponent();
            Text = "作業訊息";

            Load += Ttest_Load;
        }

        private void Ttest_Load(object sender, EventArgs e)
        {
            WebView webView = new WebView("http://192.168.32.162:5000/#/wcs/job_records");
            webView.Embed(panel1);
        }
    }
}
