﻿namespace Chump_kuka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.enable_api_btn = new System.Windows.Forms.Button();
            this.open_log_button = new System.Windows.Forms.CheckBox();
            this.btnUdpLog = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(959, 689);
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
            this.open_log_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.open_log_button.Appearance = System.Windows.Forms.Appearance.Button;
            this.open_log_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("open_log_button.BackgroundImage")));
            this.open_log_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.open_log_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.open_log_button.Location = new System.Drawing.Point(1169, 66);
            this.open_log_button.Name = "open_log_button";
            this.open_log_button.Size = new System.Drawing.Size(50, 50);
            this.open_log_button.TabIndex = 46;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enable_api_btn;
        private System.Windows.Forms.CheckBox open_log_button;
        private System.Windows.Forms.Button btnUdpLog;
    }
}

