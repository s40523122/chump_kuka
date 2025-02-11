using Chump_kuka.Controls;
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
    public partial class f02_MainMission : Form
    {
        SidePanel sidePanel = new SidePanel();
        public f02_MainMission()
        {
            InitializeComponent();


            Controls.Add(sidePanel);
        }

        private void scaleLabel7_Click(object sender, EventArgs e)
        {
            
            sidePanel.Start = true;
        }
    }
}
