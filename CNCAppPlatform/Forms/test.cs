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
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();

            Text = "第一項";

            Load += Test_Load;
        }
        private void Test_Load(object sender, EventArgs e)
        {
            WebView webView = new WebView("https://stackoverflow.com/questions/1237829/datagridview-checkbox-column-value-and-functionality");
            webView.Embed(panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
