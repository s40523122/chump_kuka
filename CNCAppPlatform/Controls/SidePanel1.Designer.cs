namespace Chump_kuka.Controls
{
    partial class SidePanel1
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
            this.components = new System.ComponentModel.Container();
            this.SidebarTimer = new System.Windows.Forms.Timer(this.components);
            this.scalePadding1 = new iCAPS.ScalePadding();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.kukaRobotStatus1 = new Chump_kuka.Controls.KukaRobotStatus();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.scalePadding1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidebarTimer
            // 
            this.SidebarTimer.Interval = 5;
            this.SidebarTimer.Tick += new System.EventHandler(this.SidebarTimer_Tick);
            // 
            // scalePadding1
            // 
            this.scalePadding1.ColumnCount = 3;
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.scalePadding1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scalePadding1.Init = true;
            this.scalePadding1.Location = new System.Drawing.Point(0, 0);
            this.scalePadding1.Margin = new System.Windows.Forms.Padding(2);
            this.scalePadding1.Name = "scalePadding1";
            this.scalePadding1.RowCount = 3;
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.SetColumnRatio = 3F;
            this.scalePadding1.SetRowRatio = 2F;
            this.scalePadding1.Size = new System.Drawing.Size(230, 473);
            this.scalePadding1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 9);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(216, 454);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.kukaRobotStatus1, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.scaleLabel1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 184);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(210, 267);
            this.tableLayoutPanel7.TabIndex = 6;
            // 
            // kukaRobotStatus1
            // 
            this.kukaRobotStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kukaRobotStatus1.Location = new System.Drawing.Point(4, 36);
            this.kukaRobotStatus1.Margin = new System.Windows.Forms.Padding(4);
            this.kukaRobotStatus1.Name = "kukaRobotStatus1";
            this.kukaRobotStatus1.Size = new System.Drawing.Size(202, 227);
            this.kukaRobotStatus1.TabIndex = 4;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 12.8F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel1.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(204, 32);
            this.scaleLabel1.TabIndex = 4;
            this.scaleLabel1.Text = "機器人狀態";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 175);
            this.panel1.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(236, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "搬運任務派發狀態(閃爍表示等待新任務)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SidePanel1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.scalePadding1);
            this.Name = "SidePanel1";
            this.Size = new System.Drawing.Size(230, 473);
            this.scalePadding1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer SidebarTimer;
        private iCAPS.ScalePadding scalePadding1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private KukaRobotStatus kukaRobotStatus1;
        private iCAPS.ScaleLabel scaleLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
