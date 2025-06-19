namespace Chump_kuka
{
    partial class Form1
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
            this.enable_api_btn = new System.Windows.Forms.Button();
            this.open_log_button = new System.Windows.Forms.CheckBox();
            this.btnUdpLog = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.myPanel1 = new iCAPS.myPanel();
            this.treeGridView1 = new Chump_kuka.Controls.TreeGridView();
            this.logWindow1 = new Chump_kuka.Controls.LogWindow();
            this.panel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.myPanel1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.logWindow1);
            this.panel1.Size = new System.Drawing.Size(959, 689);
            this.panel1.Controls.SetChildIndex(this.logWindow1, 0);
            this.panel1.Controls.SetChildIndex(this.button1, 0);
            this.panel1.Controls.SetChildIndex(this.enable_side, 0);
            this.panel1.Controls.SetChildIndex(this.myPanel1, 0);
            // 
            // enable_api_btn
            // 
            this.enable_api_btn.Location = new System.Drawing.Point(586, 66);
            this.enable_api_btn.Name = "enable_api_btn";
            this.enable_api_btn.Size = new System.Drawing.Size(109, 43);
            this.enable_api_btn.TabIndex = 45;
            this.enable_api_btn.Text = "enable api";
            this.enable_api_btn.UseVisualStyleBackColor = true;
            this.enable_api_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // open_log_button
            // 
            this.open_log_button.Appearance = System.Windows.Forms.Appearance.Button;
            this.open_log_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.open_log_button.Location = new System.Drawing.Point(717, 65);
            this.open_log_button.Name = "open_log_button";
            this.open_log_button.Size = new System.Drawing.Size(91, 43);
            this.open_log_button.TabIndex = 46;
            this.open_log_button.Text = "log";
            this.open_log_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.open_log_button.UseVisualStyleBackColor = true;
            this.open_log_button.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnUdpLog
            // 
            this.btnUdpLog.Location = new System.Drawing.Point(827, 68);
            this.btnUdpLog.Name = "btnUdpLog";
            this.btnUdpLog.Size = new System.Drawing.Size(94, 39);
            this.btnUdpLog.TabIndex = 47;
            this.btnUdpLog.Text = "UDP Log";
            this.btnUdpLog.UseVisualStyleBackColor = true;
            this.btnUdpLog.Visible = false;
            this.btnUdpLog.Click += new System.EventHandler(this.btnUdpLog_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 70);
            this.button1.TabIndex = 9;
            this.button1.Text = "刪除任務";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.treeGridView1);
            this.myPanel1.Location = new System.Drawing.Point(84, 231);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Radius = 10;
            this.myPanel1.Size = new System.Drawing.Size(458, 331);
            this.myPanel1.TabIndex = 11;
            // 
            // treeGridView1
            // 
            this.treeGridView1.Columns = new string[] {
        "起點",
        "終點",
        "建立日期",
        "完成日期",
        "🔔"};
            this.treeGridView1.DataSource = null;
            this.treeGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGridView1.Location = new System.Drawing.Point(0, 0);
            this.treeGridView1.Name = "treeGridView1";
            this.treeGridView1.Size = new System.Drawing.Size(458, 331);
            this.treeGridView1.TabIndex = 10;
            // 
            // logWindow1
            // 
            this.logWindow1.BackColor = System.Drawing.Color.Gainsboro;
            this.logWindow1.Location = new System.Drawing.Point(407, 33);
            this.logWindow1.Name = "logWindow1";
            this.logWindow1.Size = new System.Drawing.Size(681, 359);
            this.logWindow1.TabIndex = 7;
            this.logWindow1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1231, 820);
            this.Controls.Add(this.btnUdpLog);
            this.Controls.Add(this.open_log_button);
            this.Controls.Add(this.enable_api_btn);
            this.Name = "Form1";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.enable_api_btn, 0);
            this.Controls.SetChildIndex(this.open_log_button, 0);
            this.Controls.SetChildIndex(this.btnUdpLog, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enable_api_btn;
        private Controls.LogWindow logWindow1;
        private System.Windows.Forms.CheckBox open_log_button;
        private System.Windows.Forms.Button btnUdpLog;
        private System.Windows.Forms.Button button1;
        private Controls.TreeGridView treeGridView1;
        private iCAPS.myPanel myPanel1;
    }
}

