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
    public partial class ttest : Form
    {
        public ttest()
        {
            InitializeComponent();

            Load += Ttest_Load;
        }

        private void Ttest_Load(object sender, EventArgs e)
        {
            WebView webView = new WebView("https://www.youtube.com/");
            webView.Embed(panel1);
        }
    }
}
