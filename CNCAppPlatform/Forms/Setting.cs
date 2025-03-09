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
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private async void scaleButton1_Click(object sender, EventArgs e)
        {
            KukaApiHandle.Enable = true;
            progressBar1.Value = 20;
            kuka_api_check.Change = await KukaApiHandle.CheckConnect();
            progressBar1.Value = 30;
            IOHandle.Enable = true;
            progressBar1.Value = 50;
            await Task.Delay(1500);
            sensor_check.Change = IOHandle.Enable;
            progressBar1.Value = 60;
        }
    }
}
