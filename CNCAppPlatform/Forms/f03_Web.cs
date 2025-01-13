using CefSharp;
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
    public partial class f03_Web : Form
    {
        public f03_Web()
        {
            InitializeComponent();
            Text = "地圖監控";

            Load += F03_Web_Load;
        }

        private void F03_Web_Load(object sender, EventArgs e)
        {
            WebView webView = new WebView("http://192.168.32.162:5000/#/monitor/monitor");
            webView.Embed(myPanel1);
        }
    }
}
