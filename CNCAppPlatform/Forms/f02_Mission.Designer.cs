namespace Chump_kuka.Forms
{
    partial class f02_Mission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f02_Mission));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kukaRobotStatus1 = new Chump_kuka.Controls.KukaRobotStatus();
            this.kuka_area1 = new Chump_kuka.Controls.kuka_area();
            this.kukaRobotStatus2 = new Chump_kuka.Controls.KukaRobotStatus();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1154, 705);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.kukaRobotStatus1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.kuka_area1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.kukaRobotStatus2, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(37, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1078, 628);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // kukaRobotStatus1
            // 
            this.kukaRobotStatus1.AutoSize = true;
            this.kukaRobotStatus1.Location = new System.Drawing.Point(3, 3);
            this.kukaRobotStatus1.Name = "kukaRobotStatus1";
            this.kukaRobotStatus1.Size = new System.Drawing.Size(0, 0);
            this.kukaRobotStatus1.TabIndex = 0;
            // 
            // kuka_area1
            // 
            this.kuka_area1.AreaName = "scaleLabel1";
            this.kuka_area1.AreaNode = new string[] {
        "1",
        "2",
        "3"};
            this.kuka_area1.Checked = false;
            this.kuka_area1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area1.Location = new System.Drawing.Point(0, 188);
            this.kuka_area1.Margin = new System.Windows.Forms.Padding(0);
            this.kuka_area1.Name = "kuka_area1";
            this.kuka_area1.Size = new System.Drawing.Size(539, 440);
            this.kuka_area1.TabIndex = 0;
            // 
            // kukaRobotStatus2
            // 
            this.kukaRobotStatus2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kukaRobotStatus2.Location = new System.Drawing.Point(811, 191);
            this.kukaRobotStatus2.Name = "kukaRobotStatus2";
            this.kukaRobotStatus2.Size = new System.Drawing.Size(264, 434);
            this.kukaRobotStatus2.TabIndex = 1;
            // 
            // f02_Mission
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1194, 745);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f02_Mission";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "交換站任務";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.kuka_area kuka_area1;
        private Controls.KukaRobotStatus kukaRobotStatus1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.KukaRobotStatus kukaRobotStatus2;
    }
}