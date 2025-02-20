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
            this.scalePadding1 = new iCAPS.ScalePadding();
            this.button1 = new System.Windows.Forms.Button();
            this.scalePadding1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scalePadding1
            // 
            this.scalePadding1.ColumnCount = 3;
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.Controls.Add(this.button1, 1, 1);
            this.scalePadding1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scalePadding1.Init = true;
            this.scalePadding1.Location = new System.Drawing.Point(0, 28);
            this.scalePadding1.Name = "scalePadding1";
            this.scalePadding1.RowCount = 3;
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.scalePadding1.SetColumnRatio = 3F;
            this.scalePadding1.SetRowRatio = 1F;
            this.scalePadding1.Size = new System.Drawing.Size(314, 536);
            this.scalePadding1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 95);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.scalePadding1);
            this.Name = "SidePanel";
            this.Size = new System.Drawing.Size(314, 564);
            this.Controls.SetChildIndex(this.scalePadding1, 0);
            this.scalePadding1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private iCAPS.ScalePadding scalePadding1;
        private System.Windows.Forms.Button button1;
    }
}
