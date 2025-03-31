namespace Chump_kuka.Controls
{
    partial class KukaAreaControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KukaAreaControl));
            this.myPanel1 = new iCAPS.myPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.containerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new iCAPS.ScaleLabel();
            this.container1 = new Chump_kuka.Container();
            this.doubleImg1 = new iCAPS.DoubleImg();
            this.custom_border = new iCAPS.myPanel();
            this.samplePanel = new System.Windows.Forms.Panel();
            this.myPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).BeginInit();
            this.custom_border.SuspendLayout();
            this.samplePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.myPanel1.Controls.Add(this.tableLayoutPanel1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(5, 5);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Padding = new System.Windows.Forms.Padding(15);
            this.myPanel1.Radius = 10;
            this.myPanel1.Size = new System.Drawing.Size(541, 372);
            this.myPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.37484F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.62516F));
            this.tableLayoutPanel1.Controls.Add(this.containerPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(511, 342);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            // 
            // containerPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.containerPanel, 2);
            this.containerPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerPanel.Location = new System.Drawing.Point(2, 63);
            this.containerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Padding = new System.Windows.Forms.Padding(4);
            this.containerPanel.Size = new System.Drawing.Size(507, 277);
            this.containerPanel.TabIndex = 2;
            this.containerPanel.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 6);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Factor = 0.5F;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 25.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(505, 51);
            this.label1.TabIndex = 3;
            this.label1.Text = "scaleLabel1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            // 
            // container1
            // 
            this.container1.Checked = false;
            this.container1.ContainerImage = null;
            this.container1.ContainerName = "label1";
            this.container1.Location = new System.Drawing.Point(14, 15);
            this.container1.Name = "container1";
            this.container1.Size = new System.Drawing.Size(108, 146);
            this.container1.TabIndex = 1;
            this.container1.Visible = false;
            // 
            // doubleImg1
            // 
            this.doubleImg1.BackColor = System.Drawing.Color.CadetBlue;
            this.doubleImg1.Change = false;
            this.doubleImg1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doubleImg1.EnableCilck = false;
            this.doubleImg1.Image = ((System.Drawing.Image)(resources.GetObject("doubleImg1.Image")));
            this.doubleImg1.Location = new System.Drawing.Point(14, 198);
            this.doubleImg1.Name = "doubleImg1";
            this.doubleImg1.SetSquare = false;
            this.doubleImg1.Size = new System.Drawing.Size(118, 108);
            this.doubleImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doubleImg1.SubImg = ((System.Drawing.Image)(resources.GetObject("doubleImg1.SubImg")));
            this.doubleImg1.TabIndex = 3;
            this.doubleImg1.TabStop = false;
            this.doubleImg1.Tag = ((object)(resources.GetObject("doubleImg1.Tag")));
            // 
            // custom_border
            // 
            this.custom_border.BackColor = System.Drawing.SystemColors.ControlLight;
            this.custom_border.Controls.Add(this.myPanel1);
            this.custom_border.Location = new System.Drawing.Point(0, 0);
            this.custom_border.Name = "custom_border";
            this.custom_border.Padding = new System.Windows.Forms.Padding(5);
            this.custom_border.Radius = 10;
            this.custom_border.Size = new System.Drawing.Size(551, 382);
            this.custom_border.TabIndex = 4;
            // 
            // samplePanel
            // 
            this.samplePanel.Controls.Add(this.container1);
            this.samplePanel.Controls.Add(this.doubleImg1);
            this.samplePanel.Location = new System.Drawing.Point(557, 5);
            this.samplePanel.Name = "samplePanel";
            this.samplePanel.Size = new System.Drawing.Size(151, 374);
            this.samplePanel.TabIndex = 5;
            // 
            // KukaAreaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.samplePanel);
            this.Controls.Add(this.custom_border);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KukaAreaControl";
            this.Size = new System.Drawing.Size(718, 382);
            this.myPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).EndInit();
            this.custom_border.ResumeLayout(false);
            this.samplePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private iCAPS.myPanel myPanel1;
        private Container container1;
        private System.Windows.Forms.FlowLayoutPanel containerPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private iCAPS.ScaleLabel label1;
        private iCAPS.myPanel custom_border;
        private iCAPS.DoubleImg doubleImg1;
        private System.Windows.Forms.Panel samplePanel;
    }
}
