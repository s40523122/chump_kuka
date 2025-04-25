using Chump_kuka.Controller;
using Chump_kuka.Controls;
using iCAPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Chump_kuka.Forms
{
    public partial class f02_MainMission : Form
    {
        SidePanel1 sidePanel = new SidePanel1();
        private string _bind_area = "";
        private Timer _idle_timer;

        public f02_MainMission()
        {
            InitializeComponent();

            Controls.Add(sidePanel);

            //VisibleChanged += F02_MainMission_VisibleChanged;

            dataGridView1.Resize += DataGridView1_Resize;

            //KukaApiController.CarryTaskPub += KukaApiController_CarryTaskPub;
            ChatController.CarryTaskUpdated += ChatController_CarryTaskUpdated;
            LocalAreaController.StepChanged += LocalAreaController_StepChanged;

            // 訂閱 ModbusTCP 以更新貨架狀態圖片
            //KukaParm.AreaStatusChanged += (_sender, _e) => bind_area_control.UpdateContainerImage(BindAreaController.BindArea.NodeStatus.ToArray());

            Load += F02_MainMission_Load;
        }

        private void F02_MainMission_Load(object sender, EventArgs e)
        {
            // SetupDataGridView();
            // BindAreaController.BindArea?.UserControls.Add(bind_area_control);
            // BindAreaController.UpdateControl(bind_area_control);

            // SetupDataGridView();

            // 當綁定區域更新時，同步更新控制項 UI
            KukaParm.BindChanged += KukaParm_BindChanged;
            LocalAreaController.BindControl = bind_area_control;
            LocalAreaController.UpdateControl();
            LocalAreaController.ButtonPush += (_s, _e) => scaleButton1_Click(_s, _e);


            InitIdleTimer();
        }

        private void KukaParm_BindChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                LocalAreaController.UpdateControl();
            }));
        }

        private void ChatController_CarryTaskUpdated(object sender, SimpleCarryTask[] e)
        {
            dataGridView1.Invoke(new Action(() => {
                dataGridView1.DataSource = e;
                InitDataGridView();
            }));
        }

        private void InitIdleTimer()
        {
            _idle_timer = new Timer();
            _idle_timer.Interval = 10000; // 設定間隔為 10000 毫秒 (10 秒)
            _idle_timer.Tick += (s, e) =>
            {
                Light(0);
                _idle_timer.Enabled = false;
            };
            _idle_timer.Enabled = true;
        }

        private void LocalAreaController_StepChanged(object sender, Dispatchers.HttpListenerDispatcher.HeardEventArgs e)
        {
            this.Invoke(new Action(()=>
            {
                try
                {
                    switch (e.Step)
                    {
                        case 0:
                            LocalAreaController.TryCreateCarryTask();      // 更新區域狀態
                            break;
                        case 1:
                            Light(2);
                            break;
                        case 2:
                            LocalAreaController.PubReady();     // 回報搬運車進站
                            Light(3);
                            break;
                        case 4:
                            Light(4);
                            break;
                        case 5:
                            Light(5);
                            CarryTaskController.FeedbackFinish();
                            _idle_timer.Enabled = true;
                            break;
                        case 7:
                            Light(0);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }));
        }

        private void F02_MainMission_VisibleChanged(object sender, EventArgs e)
        {
            // 切換視窗時，更新區域控制項內容
            LocalAreaController.UpdateControl();
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {
            if (ParentForm.WindowState == FormWindowState.Minimized) return;
            int totalHeight = dataGridView1.ClientSize.Height; // 可用高度
            int rowCount = dataGridView1.RowCount;
            int rowHeight = totalHeight / 10; // 等比例分配

            //dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = rowHeight;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", rowHeight/3, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微軟正黑體", rowHeight / 3, FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
            };
            dataGridView1.RowTemplate.Height = rowHeight;
        }
        

        private void InitDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns["Called"].FillWeight = 14;
            dataGridView1.Columns["Called"].HeaderText = "已呼叫";
            dataGridView1.Columns["ID"].FillWeight = 6;
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["StartNode"].FillWeight = 15;
            dataGridView1.Columns["StartNode"].HeaderText = "起點";
            dataGridView1.Columns["GoalNode"].FillWeight = 15;
            dataGridView1.Columns["GoalNode"].HeaderText = "終點";
            dataGridView1.Columns["CreateTime"].FillWeight = 25;
            dataGridView1.Columns["CreateTime"].HeaderText = "建立時間";
            dataGridView1.Columns["FinishTime"].FillWeight = 25;
            dataGridView1.Columns["FinishTime"].HeaderText = "完成時間";
            

            // 設定是否允許編輯
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
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

            //for (int i = 0;  i < 30; i++)
            //{
            //    dataGridView1.Rows.Add(i, $"25", $"加工區", DateTime.Now.AddHours(i*0.6).ToString(@"yyyy/MM/dd HH:mm"), "");
            //}

        }
        private void KukaApiController_CarryTaskPub(object sender, PropertyChangedEventArgs e)
        {
            dataGridView1.Rows.Insert(0, new object[] { dataGridView1.Rows.Count, KukaParm.StartNode.Name, KukaParm.GoalNode.Name, DateTime.Now.ToString(@"yyyy/MM/dd HH:mm") });
        }

        private async void scaleButton1_Click(object sender, EventArgs e)
        {
            if (Visible == true)        // 防止設定綁定區域時，不斷跳出錯誤訊息
            {
                await MsgBox.ShowFlash("準備按鈕已按下", "", 1000);

                // 取得可搬運貨架位置
                bool can_carry = LocalAreaController.TryCreateCarryTask();

                if (!can_carry) return;     // 不可搬運狀態，跳過

                // 透過點擊區域控制項，切換手動/自動模式
                if (bind_area_control.Checked == false)
                {
                    // 手動模式，詢問是否派發任務
                    DialogResult dialogResult = MessageBox.Show($"{KukaParm.StartNode?.Name} => {KukaParm.GoalNode?.Name}", "搬運任務", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                        return;
                }

                Light(1);       // 表示物料已進站
                ChatController.AppendCarryTask();
                // KukaApiController.SendCarryTask();
            }
        }

        /// <summary>
        /// 流程燈號
        /// </summary>
        /// <param name="index"></param>
        public void Light(int index)
        {
            DoubleImg[] list = new DoubleImg[] { led_idle, led_turtle_in, led_bot_move, led_bot_in, led_bot_out, led_task_over};
            tableLayoutPanel3.Invoke(new Action(() =>
            {
                for (int i = 0; i < list.Length; i++)
                {
                    if (i == index)
                    {
                        list[i].Change = true;
                        continue;
                    }
                    list[i].Change = false;
                }
            }));
        }

        private void led_bot_in_Click(object sender, EventArgs e)
        {
            LocalAreaController.PubReady();
        }

        private async void led_bot_out_Click(object sender, EventArgs e)
        {
            LocalAreaController.PubRobotFunc();
            await Task.Delay(1000);
            LocalAreaController.PubRobotOut();
        }

        private void led_task_over_Click(object sender, EventArgs e)
        {
            LocalAreaController.PubCarryOver();
        }

        private void scaleLabel7_Click(object sender, EventArgs e)
        {
            sidePanel.Start = true;
        }

        private void led_idle_Click(object sender, EventArgs e)
        {
            // ChatController.Send("Hi");
            LocalAreaController.TurnOffLight();
        }

        private void led_turtle_in_Click(object sender, EventArgs e)
        {
            MessageBox.Show("確定發送 UDP?");
            ChatController.SayHi();
        }
    }
}
