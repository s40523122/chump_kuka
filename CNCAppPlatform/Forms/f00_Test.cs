using Chump_kuka.Services;
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
    public partial class f00_Test : Form
    {
        public f00_Test()
        {
            InitializeComponent();
            Load += F00_Test_Load;
            
            
        }

        private void F00_Test_Load(object sender, EventArgs e)
        {
            KukaApiHandle.Enable = false;
        }
    }
}
