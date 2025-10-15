using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chump_kuka
{
    public partial class SensorSim : Form
    {
        private List<KukaModel.Area> _areas;

        public SensorSim()
        {
            InitializeComponent();
            _areas = KukaParm.GetAreaArray().ToList();
            SetupGrid();
            
        }

        private void SetupGrid()
        {
            area1.Text = _areas[0].AreaName;
            area2.Text = _areas[1].AreaName;
            area3.Text = _areas[2].AreaName;

            for (int i=0; i < area1.Controls.Count; i++)
            {
                area1.Controls[i].Text = _areas[0].NodeList[i].NodeName;
                area1.Controls[i].Tag = _areas[0].NodeList[i];
                
                if (_areas[0].NodeList[i].RackStatus >= 0)
                {
                    (area1.Controls[i].Controls[_areas[0].NodeList[i].RackStatus] as RadioButton).Checked = true;
                }
            }
            for (int i = 0; i < area2.Controls.Count; i++)
            {
                area2.Controls[i].Text = _areas[1].NodeList[i].NodeName;
                area2.Controls[i].Tag = _areas[1].NodeList[i];

                if (_areas[1].NodeList[i].RackStatus >= 0)
                {
                    (area2.Controls[i].Controls[_areas[1].NodeList[i].RackStatus] as RadioButton).Checked = true;
                }
            }
            for (int i = 0; i < area3.Controls.Count; i++)
            {
                area3.Controls[i].Text = _areas[2].NodeList[i].NodeName;
                area3.Controls[i].Tag = _areas[2].NodeList[i];

                if (_areas[2].NodeList[i].RackStatus >= 0)
                {
                    (area3.Controls[i].Controls[_areas[2].NodeList[i].RackStatus] as RadioButton).Checked = true;
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
            {
                (button.Parent.Tag as KukaModel.Node).RackStatus = int.Parse((string)button.Tag);
            }
        }
    }
}

