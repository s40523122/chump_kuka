using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iCAPS;

namespace Chump_kuka.Forms
{
    public partial class f04_Web : Form
    {
        public f04_Web()
        {
            InitializeComponent();
            Text = "API 設定";

            Load += Test_Load;
        }
        private void Test_Load(object sender, EventArgs e)
        {
            WebView webView = new WebView("http://192.168.68.64:6888/");
            webView.Embed(panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
