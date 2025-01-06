﻿using Chump_kuka.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chump_kuka
{
    public partial class Container : UserControl
    {
        [Description("容器名稱。"), Category("自訂值")]
        public string ContainerName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Description("表示元件是否為已核取狀態。"), Category("自訂值")]
        public bool Checked
        {
            get { return _checked; }
            set 
            { 
                _checked = value;
                switch (_checked)
                {
                    case true:
                        doubleImg1.BackColor = Color.MediumSpringGreen;
                        panel2.BackColor = Color.OrangeRed;
                        label2.Text = "已選定";
                        f01_ManualApi.AddSeleted = this;
                        break;
                    case false:
                        doubleImg1.BackColor = Color.CadetBlue;
                        panel2.BackColor = Color.DarkOrange;
                        label2.Text = "";
                        f01_ManualApi.RemoveSeleted = this;
                        break;
                }
            }
        }
        private bool _checked = false;

        public Container()
        {
            InitializeComponent();

            SizeChanged += Container_SizeChanged;
            
        }

        private void Container_SizeChanged(object sender, EventArgs e)
        {
            panel2.Width = panel2.Height;
        }

        private void doubleImg1_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}
