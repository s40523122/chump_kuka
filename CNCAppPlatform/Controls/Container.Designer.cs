namespace Chump_kuka
{
    partial class Container
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Container));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new iCAPS.ScaleLabel();
            this.doubleImg1 = new iCAPS.DoubleImg();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new iCAPS.ScaleLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.doubleImg1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 615);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Factor = 0.6F;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 55.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 92);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // doubleImg1
            // 
            this.doubleImg1.BackColor = System.Drawing.Color.CadetBlue;
            this.doubleImg1.Change = false;
            this.doubleImg1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doubleImg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleImg1.EnableCilck = false;
            this.doubleImg1.Image = ((System.Drawing.Image)(resources.GetObject("doubleImg1.Image")));
            this.doubleImg1.Location = new System.Drawing.Point(3, 95);
            this.doubleImg1.Name = "doubleImg1";
            this.doubleImg1.SetSquare = false;
            this.doubleImg1.Size = new System.Drawing.Size(411, 424);
            this.doubleImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doubleImg1.SubImg = ((System.Drawing.Image)(resources.GetObject("doubleImg1.SubImg")));
            this.doubleImg1.TabIndex = 2;
            this.doubleImg1.TabStop = false;
            this.doubleImg1.Tag = ((object)(resources.GetObject("doubleImg1.Tag")));
            this.doubleImg1.Click += new System.EventHandler(this.doubleImg1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 522);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 93);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(49, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(362, 93);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightSlateGray;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Factor = 0.4F;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 37.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(359, 93);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(49, 93);
            this.panel2.TabIndex = 0;
            // 
            // Container
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Container";
            this.Size = new System.Drawing.Size(417, 615);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private iCAPS.ScaleLabel label1;
        private iCAPS.DoubleImg doubleImg1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private iCAPS.ScaleLabel label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
