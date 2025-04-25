namespace Chump_kuka.Controls
{
    partial class UdpChatRoom
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
            this.label1 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.multiCastGroupTxt = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.onlineUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chatroom = new System.Windows.Forms.FlowLayoutPanel();
            this.myPanel2 = new iCAPS.myPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.localIpDemo = new System.Windows.Forms.Label();
            this.myPanel6 = new iCAPS.myPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.localTimeDemo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.remoteIpDemo = new System.Windows.Forms.Label();
            this.myPanel1 = new iCAPS.myPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.remoteTimeDemo = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.replyTitleDemo = new System.Windows.Forms.Label();
            this.replyCountDemo = new System.Windows.Forms.Label();
            this.chatroom.SuspendLayout();
            this.myPanel2.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.myPanel6.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(24, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "線上使用者";
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.messageBox.Location = new System.Drawing.Point(243, 371);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(348, 27);
            this.messageBox.TabIndex = 3;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton1.Location = new System.Drawing.Point(437, -1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 47);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "單播";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton2.Location = new System.Drawing.Point(437, 34);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(112, 47);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "廣播";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioButton3.Location = new System.Drawing.Point(555, -1);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(112, 47);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "組播";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // multiCastGroupTxt
            // 
            this.multiCastGroupTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.multiCastGroupTxt.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.multiCastGroupTxt.Location = new System.Drawing.Point(555, 44);
            this.multiCastGroupTxt.Name = "multiCastGroupTxt";
            this.multiCastGroupTxt.Size = new System.Drawing.Size(160, 27);
            this.multiCastGroupTxt.TabIndex = 3;
            this.multiCastGroupTxt.Text = "239.0.0.1";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(605, 371);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(109, 26);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // onlineUsers
            // 
            this.onlineUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.onlineUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.onlineUsers.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.onlineUsers.HideSelection = false;
            this.onlineUsers.Location = new System.Drawing.Point(27, 61);
            this.onlineUsers.Name = "onlineUsers";
            this.onlineUsers.Size = new System.Drawing.Size(200, 336);
            this.onlineUsers.TabIndex = 6;
            this.onlineUsers.UseCompatibleStateImageBehavior = false;
            this.onlineUsers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "狀態";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 140;
            // 
            // chatroom
            // 
            this.chatroom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatroom.AutoScroll = true;
            this.chatroom.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chatroom.Controls.Add(this.myPanel2);
            this.chatroom.Controls.Add(this.flowLayoutPanel7);
            this.chatroom.Controls.Add(this.flowLayoutPanel1);
            this.chatroom.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatroom.Location = new System.Drawing.Point(243, 136);
            this.chatroom.Name = "chatroom";
            this.chatroom.Size = new System.Drawing.Size(473, 229);
            this.chatroom.TabIndex = 8;
            this.chatroom.WrapContents = false;
            // 
            // myPanel2
            // 
            this.myPanel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.myPanel2.Controls.Add(this.label7);
            this.myPanel2.Location = new System.Drawing.Point(3, 3);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Radius = 7;
            this.myPanel2.Size = new System.Drawing.Size(450, 15);
            this.myPanel2.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(450, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "加入聊天室";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel7.AutoSize = true;
            this.flowLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel7.Controls.Add(this.localIpDemo);
            this.flowLayoutPanel7.Controls.Add(this.myPanel6);
            this.flowLayoutPanel7.Controls.Add(this.localTimeDemo);
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(357, 24);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(96, 52);
            this.flowLayoutPanel7.TabIndex = 12;
            this.flowLayoutPanel7.Visible = false;
            // 
            // localIpDemo
            // 
            this.localIpDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.localIpDemo.AutoSize = true;
            this.localIpDemo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.localIpDemo.Location = new System.Drawing.Point(34, 0);
            this.localIpDemo.Name = "localIpDemo";
            this.localIpDemo.Size = new System.Drawing.Size(59, 12);
            this.localIpDemo.TabIndex = 9;
            this.localIpDemo.Text = "127.0.0.1";
            // 
            // myPanel6
            // 
            this.myPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myPanel6.AutoSize = true;
            this.myPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.myPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.myPanel6.Controls.Add(this.label18);
            this.myPanel6.Location = new System.Drawing.Point(0, 12);
            this.myPanel6.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.myPanel6.Name = "myPanel6";
            this.myPanel6.Radius = 5;
            this.myPanel6.Size = new System.Drawing.Size(81, 28);
            this.myPanel6.TabIndex = 8;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.MaximumSize = new System.Drawing.Size(110, 0);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(8);
            this.label18.Size = new System.Drawing.Size(81, 28);
            this.label18.TabIndex = 0;
            this.label18.Text = "How are you";
            // 
            // localTimeDemo
            // 
            this.localTimeDemo.AutoSize = true;
            this.localTimeDemo.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.localTimeDemo.Location = new System.Drawing.Point(0, 40);
            this.localTimeDemo.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.localTimeDemo.Name = "localTimeDemo";
            this.localTimeDemo.Size = new System.Drawing.Size(62, 12);
            this.localTimeDemo.TabIndex = 9;
            this.localTimeDemo.Text = "04/21 14:53";
            this.localTimeDemo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.remoteIpDemo);
            this.flowLayoutPanel1.Controls.Add(this.myPanel1);
            this.flowLayoutPanel1.Controls.Add(this.remoteTimeDemo);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 82);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(105, 52);
            this.flowLayoutPanel1.TabIndex = 11;
            this.flowLayoutPanel1.Visible = false;
            // 
            // remoteIpDemo
            // 
            this.remoteIpDemo.AutoSize = true;
            this.remoteIpDemo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.remoteIpDemo.Location = new System.Drawing.Point(3, 0);
            this.remoteIpDemo.Name = "remoteIpDemo";
            this.remoteIpDemo.Size = new System.Drawing.Size(59, 12);
            this.remoteIpDemo.TabIndex = 9;
            this.remoteIpDemo.Text = "127.0.0.1";
            // 
            // myPanel1
            // 
            this.myPanel1.AutoSize = true;
            this.myPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.myPanel1.Controls.Add(this.label5);
            this.myPanel1.Location = new System.Drawing.Point(15, 12);
            this.myPanel1.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Radius = 5;
            this.myPanel1.Size = new System.Drawing.Size(90, 28);
            this.myPanel1.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.MaximumSize = new System.Drawing.Size(110, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(8);
            this.label5.Size = new System.Drawing.Size(90, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "I\'m fine thanks";
            // 
            // remoteTimeDemo
            // 
            this.remoteTimeDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteTimeDemo.AutoSize = true;
            this.remoteTimeDemo.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.remoteTimeDemo.Location = new System.Drawing.Point(40, 40);
            this.remoteTimeDemo.Name = "remoteTimeDemo";
            this.remoteTimeDemo.Size = new System.Drawing.Size(62, 12);
            this.remoteTimeDemo.TabIndex = 9;
            this.remoteTimeDemo.Text = "04/21 14:53";
            this.remoteTimeDemo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.panel1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(243, 77);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(473, 38);
            this.flowLayoutPanel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.replyTitleDemo);
            this.panel1.Controls.Add(this.replyCountDemo);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 30);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // replyTitleDemo
            // 
            this.replyTitleDemo.AutoSize = true;
            this.replyTitleDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replyTitleDemo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.replyTitleDemo.Location = new System.Drawing.Point(0, 0);
            this.replyTitleDemo.Name = "replyTitleDemo";
            this.replyTitleDemo.Padding = new System.Windows.Forms.Padding(3);
            this.replyTitleDemo.Size = new System.Drawing.Size(109, 26);
            this.replyTitleDemo.TabIndex = 0;
            this.replyTitleDemo.Text = "robot_status";
            // 
            // replyCountDemo
            // 
            this.replyCountDemo.AutoSize = true;
            this.replyCountDemo.Dock = System.Windows.Forms.DockStyle.Right;
            this.replyCountDemo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.replyCountDemo.ForeColor = System.Drawing.Color.Red;
            this.replyCountDemo.Location = new System.Drawing.Point(109, 0);
            this.replyCountDemo.Name = "replyCountDemo";
            this.replyCountDemo.Padding = new System.Windows.Forms.Padding(3);
            this.replyCountDemo.Size = new System.Drawing.Size(26, 27);
            this.replyCountDemo.TabIndex = 0;
            this.replyCountDemo.Text = "1";
            // 
            // UdpChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 428);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.chatroom);
            this.Controls.Add(this.onlineUsers);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.multiCastGroupTxt);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(760, 467);
            this.Name = "UdpChatRoom";
            this.Text = "UdpChatRoom";
            this.chatroom.ResumeLayout(false);
            this.chatroom.PerformLayout();
            this.myPanel2.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.myPanel6.ResumeLayout(false);
            this.myPanel6.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox multiCastGroupTxt;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListView onlineUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.FlowLayoutPanel chatroom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label remoteIpDemo;
        private iCAPS.myPanel myPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label remoteTimeDemo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Label localIpDemo;
        private iCAPS.myPanel myPanel6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label localTimeDemo;
        private iCAPS.myPanel myPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label replyTitleDemo;
        private System.Windows.Forms.Label replyCountDemo;
        private System.Windows.Forms.Panel panel1;
    }
}