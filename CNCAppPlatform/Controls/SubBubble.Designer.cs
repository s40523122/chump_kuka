namespace Chump_kuka.Controls
{
    partial class SubBubble
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubBubble));
            this.myPanel1 = new iCAPS.myPanel();
            this.SuspendLayout();
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.myPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("myPanel1.BackgroundImage")));
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.myPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(8, 8);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Radius = 40;
            this.myPanel1.Size = new System.Drawing.Size(487, 369);
            this.myPanel1.TabIndex = 0;
            // 
            // SubBubble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 385);
            this.Controls.Add(this.myPanel1);
            this.Name = "SubBubble";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "SubBubble";
            this.ResumeLayout(false);

        }

        #endregion

        private iCAPS.myPanel myPanel1;
    }
}