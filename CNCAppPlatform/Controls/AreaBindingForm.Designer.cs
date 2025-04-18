using System.Windows.Forms;

namespace Chump_kuka.Controls
{
    partial class AreaBindingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "區域綁定設定";
            this.Size = new System.Drawing.Size(800, 600);

            flowLayoutPanelAreas = new FlowLayoutPanel();
            flowLayoutPanelAreas.Location = new System.Drawing.Point(10, 10);
            flowLayoutPanelAreas.Size = new System.Drawing.Size(200, 500);
            flowLayoutPanelAreas.AutoScroll = true;

            lblSelected = new Label();
            lblSelected.Location = new System.Drawing.Point(220, 10);
            lblSelected.Size = new System.Drawing.Size(500, 30);
            lblSelected.Text = "目前選擇：- → -";

            btnAddLogic = new Button();
            btnAddLogic.Text = "新增邏輯";
            btnAddLogic.Location = new System.Drawing.Point(220, 50);
            btnAddLogic.Size = new System.Drawing.Size(100, 30);
            btnAddLogic.Click += btnAddLogic_Click;

            listViewBindings = new ListView();
            listViewBindings.Location = new System.Drawing.Point(220, 100);
            listViewBindings.Size = new System.Drawing.Size(550, 350);
            listViewBindings.View = View.Details;
            listViewBindings.CheckBoxes = true;
            listViewBindings.FullRowSelect = true;
            listViewBindings.GridLines = true;
            listViewBindings.ItemChecked += listViewBindings_ItemChecked;

            listViewBindings.Columns.Add("來源", 120);
            listViewBindings.Columns.Add("目標", 120);
            listViewBindings.Columns.Add("等待呼叫", 100);

            button2 = new Button();
            button2.Text = "確定";
            button2.Location = new System.Drawing.Point(600, 470);
            button2.Size = new System.Drawing.Size(80, 30);
            button2.Click += btnOk_Click;

            button3 = new Button();
            button3.Text = "取消";
            button3.Location = new System.Drawing.Point(690, 470);
            button3.Size = new System.Drawing.Size(80, 30);
            button3.Click += btnCancel_Click;

            this.Controls.Add(flowLayoutPanelAreas);
            this.Controls.Add(lblSelected);
            this.Controls.Add(btnAddLogic);
            this.Controls.Add(listViewBindings);
            this.Controls.Add(button2);
            this.Controls.Add(button3);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAreas;
        private System.Windows.Forms.ListView listViewBindings;
        private System.Windows.Forms.Button btnAddLogic;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblSelected;
    }
}