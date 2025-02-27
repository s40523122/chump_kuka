namespace Chump_kuka.Forms
{
    partial class f01_ManualApi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f01_ManualApi));
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel2 = new iCAPS.ScaleLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel3 = new iCAPS.ScaleLabel();
            this.myPanel1 = new iCAPS.myPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.selected_2 = new iCAPS.ScaleLabel();
            this.selected_1 = new iCAPS.ScaleLabel();
            this.go_direction = new iCAPS.DoubleImg();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.kuka_area3 = new Chump_kuka.Controls.kuka_area();
            this.kuka_area2 = new Chump_kuka.Controls.kuka_area();
            this.kuka_area1 = new Chump_kuka.Controls.kuka_area();
            this.kukaRobotStatus1 = new Chump_kuka.Controls.KukaRobotStatus();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.go_direction)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel3.SetColumnSpan(this.button1, 3);
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(40, 70);
            this.button1.Margin = new System.Windows.Forms.Padding(40, 14, 40, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 42);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1091, 579);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.scaleLabel2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(35, 21);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(735, 536);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.kuka_area3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.kuka_area2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.kuka_area1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 45);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(729, 488);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // scaleLabel2
            // 
            this.scaleLabel2.AutoSize = true;
            this.scaleLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel2.Factor = 0.4F;
            this.scaleLabel2.Font = new System.Drawing.Font("微軟正黑體", 16.8F, System.Drawing.FontStyle.Bold);
            this.scaleLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel2.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel2.Name = "scaleLabel2";
            this.scaleLabel2.Size = new System.Drawing.Size(729, 42);
            this.scaleLabel2.TabIndex = 2;
            this.scaleLabel2.Text = "區域總覽";
            this.scaleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scaleLabel2.Click += new System.EventHandler(this.scaleLabel2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(776, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 536);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(277, 536);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.scaleLabel3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.myPanel1, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(277, 187);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // scaleLabel3
            // 
            this.scaleLabel3.AutoSize = true;
            this.scaleLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel3.Factor = 0.4F;
            this.scaleLabel3.Font = new System.Drawing.Font("微軟正黑體", 16.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel3.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel3.Name = "scaleLabel3";
            this.scaleLabel3.Size = new System.Drawing.Size(271, 41);
            this.scaleLabel3.TabIndex = 3;
            this.scaleLabel3.Text = "選擇派車節點";
            this.scaleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.myPanel1.Controls.Add(this.tableLayoutPanel3);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 51);
            this.myPanel1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Radius = 10;
            this.myPanel1.Size = new System.Drawing.Size(277, 126);
            this.myPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.selected_2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.selected_1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.go_direction, 1, 0);
            this.tableLayoutPanel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(277, 126);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // selected_2
            // 
            this.selected_2.AutoSize = true;
            this.selected_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_2.Factor = 0.21F;
            this.selected_2.Font = new System.Drawing.Font("微軟正黑體", 11.76F);
            this.selected_2.Location = new System.Drawing.Point(168, 0);
            this.selected_2.Name = "selected_2";
            this.selected_2.Size = new System.Drawing.Size(106, 56);
            this.selected_2.TabIndex = 2;
            this.selected_2.Text = "null";
            this.selected_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selected_1
            // 
            this.selected_1.AutoSize = true;
            this.selected_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_1.Factor = 0.21F;
            this.selected_1.Font = new System.Drawing.Font("微軟正黑體", 11.76F);
            this.selected_1.Location = new System.Drawing.Point(3, 0);
            this.selected_1.Name = "selected_1";
            this.selected_1.Size = new System.Drawing.Size(104, 56);
            this.selected_1.TabIndex = 2;
            this.selected_1.Text = "null";
            this.selected_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // go_direction
            // 
            this.go_direction.Change = false;
            this.go_direction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.go_direction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.go_direction.EnableCilck = true;
            this.go_direction.Image = ((System.Drawing.Image)(resources.GetObject("go_direction.Image")));
            this.go_direction.Location = new System.Drawing.Point(113, 3);
            this.go_direction.Name = "go_direction";
            this.go_direction.SetSquare = false;
            this.go_direction.Size = new System.Drawing.Size(49, 50);
            this.go_direction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.go_direction.SubImg = ((System.Drawing.Image)(resources.GetObject("go_direction.SubImg")));
            this.go_direction.TabIndex = 3;
            this.go_direction.TabStop = false;
            this.go_direction.Tag = ((object)(resources.GetObject("go_direction.Tag")));
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.kukaRobotStatus1, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.scaleLabel1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 190);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(271, 343);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 16.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel1.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(265, 41);
            this.scaleLabel1.TabIndex = 4;
            this.scaleLabel1.Text = "機器人狀態";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kuka_area3
            // 
            this.kuka_area3.AreaName = "成品區";
            this.kuka_area3.AreaNode = new string[] {
        "42",
        "43",
        "44",
        "45",
        "46",
        "47"};
            this.kuka_area3.Checked = false;
            this.kuka_area3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area3.Location = new System.Drawing.Point(10, 254);
            this.kuka_area3.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area3.Name = "kuka_area3";
            this.kuka_area3.NodeStatus = new int[0];
            this.kuka_area3.Size = new System.Drawing.Size(344, 224);
            this.kuka_area3.TabIndex = 2;
            // 
            // kuka_area2
            // 
            this.kuka_area2.AreaName = "組裝區";
            this.kuka_area2.AreaNode = new string[] {
        "36",
        "37",
        "38",
        "39"};
            this.kuka_area2.Checked = false;
            this.kuka_area2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area2.Location = new System.Drawing.Point(374, 10);
            this.kuka_area2.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area2.Name = "kuka_area2";
            this.kuka_area2.NodeStatus = new int[0];
            this.kuka_area2.Size = new System.Drawing.Size(345, 224);
            this.kuka_area2.TabIndex = 1;
            // 
            // kuka_area1
            // 
            this.kuka_area1.AreaName = "加工區";
            this.kuka_area1.AreaNode = new string[] {
        "25",
        "26"};
            this.kuka_area1.Checked = false;
            this.kuka_area1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area1.Location = new System.Drawing.Point(10, 10);
            this.kuka_area1.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area1.Name = "kuka_area1";
            this.kuka_area1.NodeStatus = new int[0];
            this.kuka_area1.Size = new System.Drawing.Size(344, 224);
            this.kuka_area1.TabIndex = 0;
            // 
            // kukaRobotStatus1
            // 
            this.kukaRobotStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kukaRobotStatus1.Location = new System.Drawing.Point(3, 44);
            this.kukaRobotStatus1.Name = "kukaRobotStatus1";
            this.kukaRobotStatus1.Size = new System.Drawing.Size(265, 296);
            this.kukaRobotStatus1.TabIndex = 4;
            // 
            // f01_ManualApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 579);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "f01_ManualApi";
            this.Text = "手動派車";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.go_direction)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.kuka_area kuka_area3;
        private Controls.kuka_area kuka_area2;
        private Controls.kuka_area kuka_area1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private iCAPS.ScaleLabel selected_2;
        private System.Windows.Forms.Panel panel1;
        private iCAPS.DoubleImg go_direction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private iCAPS.ScaleLabel selected_1;
        private Controls.KukaRobotStatus kukaRobotStatus1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private iCAPS.ScaleLabel scaleLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private iCAPS.ScaleLabel scaleLabel3;
        private iCAPS.myPanel myPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private iCAPS.ScaleLabel scaleLabel1;
    }
}