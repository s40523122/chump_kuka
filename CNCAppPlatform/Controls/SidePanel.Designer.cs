namespace Chump_kuka.Controls
{
    partial class SidePanel
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
            this.scalePadding1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
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
            this.scalePadding1.Name = "scalePadding1";
            this.scalePadding1.RowCount = 3;
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.scalePadding1.SetColumnRatio = 3F;
            this.scalePadding1.SetRowRatio = 2F;
            this.scalePadding1.Size = new System.Drawing.Size(306, 631);
            this.scalePadding1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(287, 605);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.kukaRobotStatus1, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.scaleLabel1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 246);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(279, 355);
            this.tableLayoutPanel7.TabIndex = 6;
            // 
            // kukaRobotStatus1
            // 
            this.kukaRobotStatus1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kukaRobotStatus1.Location = new System.Drawing.Point(5, 47);
            this.kukaRobotStatus1.Margin = new System.Windows.Forms.Padding(5);
            this.kukaRobotStatus1.Name = "kukaRobotStatus1";
            this.kukaRobotStatus1.Size = new System.Drawing.Size(269, 303);
            this.kukaRobotStatus1.TabIndex = 4;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 16.8F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel1.Location = new System.Drawing.Point(4, 0);
            this.scaleLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(271, 42);
            this.scaleLabel1.TabIndex = 4;
            this.scaleLabel1.Text = "機器人狀態";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.Controls.Add(this.scalePadding1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SidePanel";
            this.Size = new System.Drawing.Size(306, 631);
            this.scalePadding1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer SidebarTimer;
        private iCAPS.ScalePadding scalePadding1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private KukaRobotStatus kukaRobotStatus1;
        private iCAPS.ScaleLabel scaleLabel1;
    }
}
