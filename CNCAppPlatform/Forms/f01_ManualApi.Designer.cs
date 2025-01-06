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
            this.doubleImg1 = new iCAPS.DoubleImg();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.button1, 3);
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(30, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 100);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1183, 732);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.kuka_area3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.kuka_area2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.kuka_area1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(881, 726);
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
            this.kuka_area3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area3.Location = new System.Drawing.Point(10, 373);
            this.kuka_area3.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area3.Name = "kuka_area3";
            this.kuka_area3.Size = new System.Drawing.Size(420, 343);
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
            this.kuka_area2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area2.Location = new System.Drawing.Point(450, 10);
            this.kuka_area2.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area2.Name = "kuka_area2";
            this.kuka_area2.Size = new System.Drawing.Size(421, 343);
            this.kuka_area2.TabIndex = 1;
            // 
            // kuka_area1
            // 
            this.kuka_area1.AreaName = "加工區";
            this.kuka_area1.AreaNode = new string[] {
        "25",
        "26"};
            this.kuka_area1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kuka_area1.Location = new System.Drawing.Point(10, 10);
            this.kuka_area1.Margin = new System.Windows.Forms.Padding(10);
            this.kuka_area1.Name = "kuka_area1";
            this.kuka_area1.Size = new System.Drawing.Size(420, 343);
            this.kuka_area1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(890, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 726);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(290, 726);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 284);
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
            this.tableLayoutPanel3.Controls.Add(this.doubleImg1, 1, 1);
            this.tableLayoutPanel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(278, 265);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.scaleLabel1, 2);
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 21.2F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(160, 53);
            this.scaleLabel1.TabIndex = 1;
            this.scaleLabel1.Text = "已選擇";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // selected_2
            // 
            this.selected_2.AutoSize = true;
            this.selected_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_2.Factor = 0.21F;
            this.selected_2.Font = new System.Drawing.Font("微軟正黑體", 19.32F);
            this.selected_2.Location = new System.Drawing.Point(169, 53);
            this.selected_2.Name = "selected_2";
            this.selected_2.Size = new System.Drawing.Size(106, 92);
            this.selected_2.TabIndex = 2;
            this.selected_2.Text = "組裝區";
            this.selected_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selected_1
            // 
            this.selected_1.AutoSize = true;
            this.selected_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected_1.Factor = 0.21F;
            this.selected_1.Font = new System.Drawing.Font("微軟正黑體", 19.32F);
            this.selected_1.Location = new System.Drawing.Point(3, 53);
            this.selected_1.Name = "selected_1";
            this.selected_1.Size = new System.Drawing.Size(105, 92);
            this.selected_1.TabIndex = 2;
            this.selected_1.Text = "組裝區";
            this.selected_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // doubleImg1
            // 
            this.doubleImg1.Change = false;
            this.doubleImg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleImg1.Image = ((System.Drawing.Image)(resources.GetObject("doubleImg1.Image")));
            this.doubleImg1.Location = new System.Drawing.Point(114, 56);
            this.doubleImg1.Name = "doubleImg1";
            this.doubleImg1.SetSquare = false;
            this.doubleImg1.Size = new System.Drawing.Size(49, 86);
            this.doubleImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doubleImg1.SubImg = ((System.Drawing.Image)(resources.GetObject("doubleImg1.SubImg")));
            this.doubleImg1.TabIndex = 3;
            this.doubleImg1.TabStop = false;
            this.doubleImg1.Tag = ((object)(resources.GetObject("doubleImg1.Tag")));
            // 
            // f01_ManualApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 732);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "f01_ManualApi";
            this.Text = "手動派車";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).EndInit();
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
        private iCAPS.DoubleImg doubleImg1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private iCAPS.ScaleLabel selected_1;
    }
}