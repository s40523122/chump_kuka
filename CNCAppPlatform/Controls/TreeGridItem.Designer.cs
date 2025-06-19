namespace Chump_kuka.Controls
{
    partial class TreeGridItem
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.scaleLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 136);
            this.panel1.TabIndex = 0;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoEllipsis = true;
            this.scaleLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.scaleLabel1.Factor = 0.25F;
            this.scaleLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 34F);
            this.scaleLabel1.Location = new System.Drawing.Point(0, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(93, 136);
            this.scaleLabel1.TabIndex = 0;
            this.scaleLabel1.Text = "0";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.scaleLabel1.Click += new System.EventHandler(this.TreeGridItem_Click);
            // 
            // TreeGridItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TreeGridItem";
            this.Size = new System.Drawing.Size(637, 244);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private iCAPS.ScaleLabel scaleLabel1;
    }
}
