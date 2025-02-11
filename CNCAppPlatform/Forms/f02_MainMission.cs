using Chump_kuka.Controls;
using HZH_Controls.Controls;
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


            Load += F02_MainMission_Load;

            dataGridView1.Resize += DataGridView1_Resize;
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {
            int totalHeight = dataGridView1.ClientSize.Height; // 可用高度
            int rowCount = dataGridView1.RowCount;
            int rowHeight = totalHeight / 10; // 等比例分配

            //dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = rowHeight;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", rowHeight/3, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", rowHeight / 3, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (rowCount > 0)
            {
                
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = rowHeight;
                }
            }
        }

        private void F02_MainMission_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView1.ColumnCount = 5; // 設定 3 欄
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "起點";
            dataGridView1.Columns[2].Name = "終點";
            dataGridView1.Columns[3].Name = "建立時間";
            dataGridView1.Columns[4].Name = "完成時間";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 假設有三個欄位，分別設定 FillWeight
            dataGridView1.Columns[0].FillWeight = 10; // 30% 
            dataGridView1.Columns[1].FillWeight = 15; // 50%
            dataGridView1.Columns[2].FillWeight = 15; // 20%
            dataGridView1.Columns[3].FillWeight = 30; // 50%
            dataGridView1.Columns[4].FillWeight = 30; // 20%

            //dataGridView1.RowTemplate.Height = 30;

            // 設定是否允許編輯
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;


            for (int i = 0;  i < 30; i++)
            {
                dataGridView1.Rows.Add(i, $"25", $"加工區", DateTime.Now.AddHours(i*0.6).ToString(@"yyyy/MM/dd HH:mm"), "");
            }

        }

        private void scaleLabel7_Click(object sender, EventArgs e)
        {
            
            sidePanel.Start = true;
        }

        private void kuka_area1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
            dataGridView1.Rows.Add(dataGridView1.Rows.Count, $"25", $"加工區", DateTime.Now.ToString(@"yyyy/MM/dd HH:mm"), "");
        }
    }
}
