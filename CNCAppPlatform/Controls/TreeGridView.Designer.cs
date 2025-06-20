namespace Chump_kuka.Controls
{
    partial class TreeGridView
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.treeGridItem1 = new Chump_kuka.Controls.TreeGridRow();
            this.treeGridItem2 = new Chump_kuka.Controls.TreeGridRow();
            this.treeGridItem3 = new Chump_kuka.Controls.TreeGridRow();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.treeGridItem1);
            this.flowLayoutPanel1.Controls.Add(this.treeGridItem2);
            this.flowLayoutPanel1.Controls.Add(this.treeGridItem3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 46);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(469, 296);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.scaleLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.panel1.Size = new System.Drawing.Size(469, 46);
            this.panel1.TabIndex = 1;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.scaleLabel1.Factor = 0.3F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.ForeColor = System.Drawing.Color.White;
            this.scaleLabel1.Location = new System.Drawing.Point(0, 0);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(46, 46);
            this.scaleLabel1.TabIndex = 0;
            this.scaleLabel1.Text = "ID";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeGridItem1
            // 
            this.treeGridItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeGridItem1.ColumnRatios = new float[] {
        0.2F,
        0.2F,
        0.24F,
        0.24F,
        0.12F};
            this.treeGridItem1.ID = 100;
            this.treeGridItem1.Items = new string[] {
        "20",
        "加工區",
        "06/19 10:20",
        "06/19 10:35",
        "🔕"};
            this.treeGridItem1.Location = new System.Drawing.Point(3, 3);
            this.treeGridItem1.LogMsg = "";
            this.treeGridItem1.Name = "treeGridItem1";
            this.treeGridItem1.Size = new System.Drawing.Size(462, 46);
            this.treeGridItem1.TabIndex = 9;
            this.treeGridItem1.Visible = false;
            // 
            // treeGridItem2
            // 
            this.treeGridItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeGridItem2.ColumnRatios = new float[] {
        0.2F,
        0.2F,
        0.24F,
        0.24F,
        0.12F};
            this.treeGridItem2.ID = 100;
            this.treeGridItem2.Items = new string[] {
        "23",
        "組裝區",
        "06/19 10:22",
        "06/19 10:48",
        "🔔"};
            this.treeGridItem2.Location = new System.Drawing.Point(3, 55);
            this.treeGridItem2.LogMsg = "";
            this.treeGridItem2.Name = "treeGridItem2";
            this.treeGridItem2.Size = new System.Drawing.Size(462, 46);
            this.treeGridItem2.TabIndex = 10;
            this.treeGridItem2.Visible = false;
            // 
            // treeGridItem3
            // 
            this.treeGridItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeGridItem3.ColumnRatios = new float[] {
        0.2F,
        0.2F,
        0.24F,
        0.24F,
        0.12F};
            this.treeGridItem3.ID = 100;
            this.treeGridItem3.Items = new string[] {
        "26",
        "成品區",
        "06/19 10:21",
        "06/19 10:55",
        "N"};
            this.treeGridItem3.Location = new System.Drawing.Point(3, 107);
            this.treeGridItem3.LogMsg = "";
            this.treeGridItem3.Name = "treeGridItem3";
            this.treeGridItem3.Size = new System.Drawing.Size(462, 46);
            this.treeGridItem3.TabIndex = 11;
            this.treeGridItem3.Visible = false;
            // 
            // TreeGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "TreeGridView";
            this.Size = new System.Drawing.Size(469, 342);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private TreeGridRow treeGridItem1;
        private TreeGridRow treeGridItem2;
        private TreeGridRow treeGridItem3;
        private System.Windows.Forms.Panel panel1;
        private iCAPS.ScaleLabel scaleLabel1;
    }
}
