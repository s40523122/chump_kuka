using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using iCAPS;
using System.Windows.Documents;
using ModbusTCP_Master;
using Modbus.Device;
using System.Net.Sockets;
using System.Net.Http;

namespace Chump_kuka
{
    public partial class Form1 : iCAPS.Form1
    {
        public Form1()
        {
            InitializeComponent();
            
            if (!Debugger.IsAttached) { button1.Visible = false; }


            modbusService = new ModbusService();
            Load += Form1_Load;

            KukaParm.PropertyChanged += KukaParm_PropertyChanged;
            UpdateUI();
        }

        private void KukaParm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            label2.Text = $"音量: {KukaParm.Volume}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Env.enble_kuka_api = true;
        }


        private ModbusService modbusService;
        private bool NetworkIsOk = false;
        private List<PictureBox> listDI;
        private List<PictureBox> listDO;

        private void Form1_Load(object sender, EventArgs e)
        {
            listDI = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };
            //listDO = new List<PictureBox> { DO0, DO1, DO2, DO3 };
        }

        private async void btStart_Click(object sender, EventArgs e)
        {
            // NetworkIsOk = modbusService.Connect(txtIP.Text);
            NetworkIsOk = await modbusService.Connect("192.168.255.1");
            if (NetworkIsOk)
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
                btStart.Enabled = false;
                btStop.Enabled = true;
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            modbusService.Disconnect();
            btStart.Enabled = true;
            btStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (NetworkIsOk)
            {
                UpdateDI();
                //UpdateDO();
            }
        }

        private void UpdateDI()
        {
            bool[] status = modbusService.ReadDI(listDI.Count);
            for (int i = 0; i < listDI.Count; i++)
            {
                listDI[i].BackColor = status[i] ? Color.Lime : Color.Green;
            }
        }

        private void UpdateDO()
        {
            bool[] coils = modbusService.ReadDO(listDO.Count);
            for (int i = 0; i < listDO.Count; i++)
            {
                listDO[i].BackColor = coils[i] ? Color.Red : Color.Maroon;
            }
        }

        private void DO_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureBox)sender).Tag.ToString());
            bool currentState = listDO[index].BackColor == Color.Red;
            modbusService.WriteDO(index, !currentState);
            listDO[index].BackColor = !currentState ? Color.Red : Color.Maroon;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            KukaParm.Volume = textBox1.Text;
        }
    }
}
