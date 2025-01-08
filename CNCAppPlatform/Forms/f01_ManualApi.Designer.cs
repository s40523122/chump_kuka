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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.kuka_area3 = new Chump_kuka.Controls.kuka_area();
            this.kuka_area2 = new Chump_kuka.Controls.kuka_area();
            this.kuka_area1 = new Chump_kuka.Controls.kuka_area();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.selected_2 = new iCAPS.ScaleLabel();
            this.selected_1 = new iCAPS.ScaleLabel();
            this.go_direction = new iCAPS.DoubleImg();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kukaRobotStatus1 = new Chump_kuka.Controls.KukaRobotStatus();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.go_direction)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel3.SetColumnSpan(this.button1, 3);
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(40, 105);
            this.button1.Margin = new System.Windows.Forms.Padding(40, 15, 40, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 45);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1163, 712);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(26, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(842, 634);
            this.tableLayoutPanel2.TabIndex = 1;
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
            this.kuka_area3.Location = new System.Drawing.Point(10, 327);
            this.kuka_area3.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area3.Name = "kuka_area3";
            this.kuka_area3.Size = new System.Drawing.Size(401, 297);
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
            this.kuka_area2.Location = new System.Drawing.Point(431, 10);
            this.kuka_area2.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area2.Name = "kuka_area2";
            this.kuka_area2.Size = new System.Drawing.Size(401, 297);
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
            this.kuka_area1.Size = new System.Drawing.Size(401, 297);
            this.kuka_area1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(874, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 634);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(261, 634);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 184);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.selected_2, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.selected_1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.go_direction, 1, 1);
            this.tableLayoutPanel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(249, 165);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.scaleLabel1, 2);
            this.scaleLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 13.2F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(142, 33);
            this.scaleLabel1.TabIndex = 1;
            this.scaleLabel1.Text = "選擇派車節點";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // selected_2
            // 
            this.selected_2.AutoSize = true;
            this.selected_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_2.Factor = 0.21F;
            this.selected_2.Font = new System.Drawing.Font("微軟正黑體", 11.97F);
            this.selected_2.Location = new System.Drawing.Point(151, 33);
            this.selected_2.Name = "selected_2";
            this.selected_2.Size = new System.Drawing.Size(95, 57);
            this.selected_2.TabIndex = 2;
            this.selected_2.Text = "null";
            this.selected_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selected_1
            // 
            this.selected_1.AutoSize = true;
            this.selected_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_1.Factor = 0.21F;
            this.selected_1.Font = new System.Drawing.Font("微軟正黑體", 11.97F);
            this.selected_1.Location = new System.Drawing.Point(3, 33);
            this.selected_1.Name = "selected_1";
            this.selected_1.Size = new System.Drawing.Size(93, 57);
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
            this.go_direction.Location = new System.Drawing.Point(102, 36);
            this.go_direction.Name = "go_direction";
            this.go_direction.SetSquare = false;
            this.go_direction.Size = new System.Drawing.Size(43, 51);
            this.go_direction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.go_direction.SubImg = ((System.Drawing.Image)(resources.GetObject("go_direction.SubImg")));
            this.go_direction.TabIndex = 3;
            this.go_direction.TabStop = false;
            this.go_direction.Tag = ((object)(resources.GetObject("go_direction.Tag")));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kukaRobotStatus1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(255, 438);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // kukaRobotStatus1
            // 
            this.kukaRobotStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kukaRobotStatus1.Location = new System.Drawing.Point(10, 23);
            this.kukaRobotStatus1.Name = "kukaRobotStatus1";
            this.kukaRobotStatus1.Size = new System.Drawing.Size(235, 405);
            this.kukaRobotStatus1.TabIndex = 4;
            // 
            // f01_ManualApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 732);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "f01_ManualApi";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "手動派車";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.go_direction)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private iCAPS.ScaleLabel scaleLabel1;
        private iCAPS.ScaleLabel selected_2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private iCAPS.DoubleImg go_direction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private iCAPS.ScaleLabel selected_1;
        private Controls.KukaRobotStatus kukaRobotStatus1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}