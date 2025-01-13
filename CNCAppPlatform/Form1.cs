using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using iCAPS;

namespace Chump_kuka
{
    public partial class Form1 : iCAPS.Form1
    {
        public Form1()
        {
            InitializeComponent();
            
            if (!Debugger.IsAttached) { button1.Visible = false; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Env.enble_kuka_api = true;
        }
    }
}
