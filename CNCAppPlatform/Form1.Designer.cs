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
            this.components = new System.ComponentModel.Container();
            this.enable_api_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.open_log_button = new System.Windows.Forms.CheckBox();
            this.logWindow1 = new Chump_kuka.Controls.LogWindow();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logWindow1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btStop);
            this.panel1.Controls.Add(this.btStart);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Size = new System.Drawing.Size(959, 689);
            this.panel1.Controls.SetChildIndex(this.pictureBox1, 0);
            this.panel1.Controls.SetChildIndex(this.pictureBox2, 0);
            this.panel1.Controls.SetChildIndex(this.pictureBox3, 0);
            this.panel1.Controls.SetChildIndex(this.pictureBox4, 0);
            this.panel1.Controls.SetChildIndex(this.btStart, 0);
            this.panel1.Controls.SetChildIndex(this.btStop, 0);
            this.panel1.Controls.SetChildIndex(this.button2, 0);
            this.panel1.Controls.SetChildIndex(this.logWindow1, 0);
            this.panel1.Controls.SetChildIndex(this.enable_side, 0);
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox1.Location = new System.Drawing.Point(86, 194);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 56);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox2.Location = new System.Drawing.Point(165, 194);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(56, 56);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox3.Location = new System.Drawing.Point(242, 194);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(56, 56);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox4.Location = new System.Drawing.Point(320, 194);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(56, 56);
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(166, 272);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(91, 45);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Visible = false;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(285, 272);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(91, 45);
            this.btStop.TabIndex = 2;
            this.btStop.Text = "stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Visible = false;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(708, 411);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 42);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.Controls.Add(this.open_log_button);
            this.Controls.Add(this.enable_api_btn);
            this.Name = "Form1";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.enable_api_btn, 0);
            this.Controls.SetChildIndex(this.open_log_button, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enable_api_btn;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private Controls.LogWindow logWindow1;
        private System.Windows.Forms.CheckBox open_log_button;
    }
}

