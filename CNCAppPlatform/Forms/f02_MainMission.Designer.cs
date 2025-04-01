namespace Chump_kuka.Forms
{
    partial class f02_MainMission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f02_MainMission));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel7 = new iCAPS.ScaleLabel();
            this.myPanel1 = new iCAPS.myPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.scaleLabel2 = new iCAPS.ScaleLabel();
            this.scaleLabel3 = new iCAPS.ScaleLabel();
            this.scaleLabel4 = new iCAPS.ScaleLabel();
            this.scaleLabel5 = new iCAPS.ScaleLabel();
            this.scaleLabel6 = new iCAPS.ScaleLabel();
            this.led_idle = new iCAPS.DoubleImg();
            this.led_turtle_in = new iCAPS.DoubleImg();
            this.led_bot_move = new iCAPS.DoubleImg();
            this.led_bot_in = new iCAPS.DoubleImg();
            this.led_bot_out = new iCAPS.DoubleImg();
            this.led_task_over = new iCAPS.DoubleImg();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel8 = new iCAPS.ScaleLabel();
            this.scaleButton1 = new iCAPS.ScaleButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.scaleLabel9 = new iCAPS.ScaleLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bind_area_control = new Chump_kuka.Controls.KukaAreaControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.led_idle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_turtle_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_move)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_task_over)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1159, 691);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.scaleLabel7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.myPanel1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(37, 23);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1082, 187);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // scaleLabel7
            // 
            this.scaleLabel7.AutoSize = true;
            this.scaleLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel7.Factor = 0.4F;
            this.scaleLabel7.Font = new System.Drawing.Font("微軟正黑體", 22.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel7.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel7.Name = "scaleLabel7";
            this.scaleLabel7.Size = new System.Drawing.Size(1076, 56);
            this.scaleLabel7.TabIndex = 0;
            this.scaleLabel7.Text = "任務流程";
            this.scaleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scaleLabel7.Click += new System.EventHandler(this.scaleLabel7_Click);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.myPanel1.Controls.Add(this.tableLayoutPanel3);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(3, 59);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Radius = 10;
            this.myPanel1.Size = new System.Drawing.Size(1076, 125);
            this.myPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel2, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel3, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel4, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel5, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.scaleLabel6, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.led_idle, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.led_turtle_in, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.led_bot_move, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.led_bot_in, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.led_bot_out, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.led_task_over, 5, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1076, 125);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.35F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel1.Location = new System.Drawing.Point(3, 65);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(173, 50);
            this.scaleLabel1.TabIndex = 0;
            this.scaleLabel1.Text = "區域閒置";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scaleLabel2
            // 
            this.scaleLabel2.AutoSize = true;
            this.scaleLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel2.Factor = 0.35F;
            this.scaleLabel2.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel2.Location = new System.Drawing.Point(182, 65);
            this.scaleLabel2.Name = "scaleLabel2";
            this.scaleLabel2.Size = new System.Drawing.Size(173, 50);
            this.scaleLabel2.TabIndex = 0;
            this.scaleLabel2.Text = "物料進站";
            this.scaleLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scaleLabel3
            // 
            this.scaleLabel3.AutoSize = true;
            this.scaleLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel3.Factor = 0.35F;
            this.scaleLabel3.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel3.Location = new System.Drawing.Point(361, 65);
            this.scaleLabel3.Name = "scaleLabel3";
            this.scaleLabel3.Size = new System.Drawing.Size(173, 50);
            this.scaleLabel3.TabIndex = 0;
            this.scaleLabel3.Text = "等待運輸";
            this.scaleLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scaleLabel4
            // 
            this.scaleLabel4.AutoSize = true;
            this.scaleLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel4.Factor = 0.35F;
            this.scaleLabel4.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel4.Location = new System.Drawing.Point(540, 65);
            this.scaleLabel4.Name = "scaleLabel4";
            this.scaleLabel4.Size = new System.Drawing.Size(173, 50);
            this.scaleLabel4.TabIndex = 0;
            this.scaleLabel4.Text = "機器人進站";
            this.scaleLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scaleLabel5
            // 
            this.scaleLabel5.AutoSize = true;
            this.scaleLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel5.Factor = 0.35F;
            this.scaleLabel5.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel5.Location = new System.Drawing.Point(719, 65);
            this.scaleLabel5.Name = "scaleLabel5";
            this.scaleLabel5.Size = new System.Drawing.Size(173, 50);
            this.scaleLabel5.TabIndex = 0;
            this.scaleLabel5.Text = "物料出站";
            this.scaleLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scaleLabel6
            // 
            this.scaleLabel6.AutoSize = true;
            this.scaleLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel6.Factor = 0.35F;
            this.scaleLabel6.Font = new System.Drawing.Font("微軟正黑體", 17.5F, System.Drawing.FontStyle.Bold);
            this.scaleLabel6.Location = new System.Drawing.Point(898, 65);
            this.scaleLabel6.Name = "scaleLabel6";
            this.scaleLabel6.Size = new System.Drawing.Size(175, 50);
            this.scaleLabel6.TabIndex = 0;
            this.scaleLabel6.Text = "物料送達";
            this.scaleLabel6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // led_idle
            // 
            this.led_idle.Change = false;
            this.led_idle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_idle.EnableCilck = true;
            this.led_idle.Image = ((System.Drawing.Image)(resources.GetObject("led_idle.Image")));
            this.led_idle.Location = new System.Drawing.Point(3, 25);
            this.led_idle.Name = "led_idle";
            this.led_idle.SetSquare = false;
            this.led_idle.Size = new System.Drawing.Size(173, 37);
            this.led_idle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_idle.SubImg = ((System.Drawing.Image)(resources.GetObject("led_idle.SubImg")));
            this.led_idle.TabIndex = 1;
            this.led_idle.TabStop = false;
            this.led_idle.Tag = ((object)(resources.GetObject("led_idle.Tag")));
            this.led_idle.Click += new System.EventHandler(this.led_idle_Click);
            // 
            // led_turtle_in
            // 
            this.led_turtle_in.Change = false;
            this.led_turtle_in.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_turtle_in.EnableCilck = true;
            this.led_turtle_in.Image = ((System.Drawing.Image)(resources.GetObject("led_turtle_in.Image")));
            this.led_turtle_in.Location = new System.Drawing.Point(182, 25);
            this.led_turtle_in.Name = "led_turtle_in";
            this.led_turtle_in.SetSquare = false;
            this.led_turtle_in.Size = new System.Drawing.Size(173, 37);
            this.led_turtle_in.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_turtle_in.SubImg = ((System.Drawing.Image)(resources.GetObject("led_turtle_in.SubImg")));
            this.led_turtle_in.TabIndex = 1;
            this.led_turtle_in.TabStop = false;
            this.led_turtle_in.Tag = ((object)(resources.GetObject("led_turtle_in.Tag")));
            this.led_turtle_in.Click += new System.EventHandler(this.led_turtle_in_Click);
            // 
            // led_bot_move
            // 
            this.led_bot_move.Change = false;
            this.led_bot_move.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_bot_move.EnableCilck = true;
            this.led_bot_move.Image = ((System.Drawing.Image)(resources.GetObject("led_bot_move.Image")));
            this.led_bot_move.Location = new System.Drawing.Point(361, 25);
            this.led_bot_move.Name = "led_bot_move";
            this.led_bot_move.SetSquare = false;
            this.led_bot_move.Size = new System.Drawing.Size(173, 37);
            this.led_bot_move.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_bot_move.SubImg = ((System.Drawing.Image)(resources.GetObject("led_bot_move.SubImg")));
            this.led_bot_move.TabIndex = 1;
            this.led_bot_move.TabStop = false;
            this.led_bot_move.Tag = ((object)(resources.GetObject("led_bot_move.Tag")));
            // 
            // led_bot_in
            // 
            this.led_bot_in.Change = false;
            this.led_bot_in.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_bot_in.EnableCilck = true;
            this.led_bot_in.Image = ((System.Drawing.Image)(resources.GetObject("led_bot_in.Image")));
            this.led_bot_in.Location = new System.Drawing.Point(540, 25);
            this.led_bot_in.Name = "led_bot_in";
            this.led_bot_in.SetSquare = false;
            this.led_bot_in.Size = new System.Drawing.Size(173, 37);
            this.led_bot_in.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_bot_in.SubImg = ((System.Drawing.Image)(resources.GetObject("led_bot_in.SubImg")));
            this.led_bot_in.TabIndex = 1;
            this.led_bot_in.TabStop = false;
            this.led_bot_in.Tag = ((object)(resources.GetObject("led_bot_in.Tag")));
            this.led_bot_in.Click += new System.EventHandler(this.led_bot_in_Click);
            // 
            // led_bot_out
            // 
            this.led_bot_out.Change = false;
            this.led_bot_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_bot_out.EnableCilck = true;
            this.led_bot_out.Image = ((System.Drawing.Image)(resources.GetObject("led_bot_out.Image")));
            this.led_bot_out.Location = new System.Drawing.Point(719, 25);
            this.led_bot_out.Name = "led_bot_out";
            this.led_bot_out.SetSquare = false;
            this.led_bot_out.Size = new System.Drawing.Size(173, 37);
            this.led_bot_out.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_bot_out.SubImg = ((System.Drawing.Image)(resources.GetObject("led_bot_out.SubImg")));
            this.led_bot_out.TabIndex = 1;
            this.led_bot_out.TabStop = false;
            this.led_bot_out.Tag = ((object)(resources.GetObject("led_bot_out.Tag")));
            this.led_bot_out.Click += new System.EventHandler(this.led_bot_out_Click);
            // 
            // led_task_over
            // 
            this.led_task_over.Change = false;
            this.led_task_over.Dock = System.Windows.Forms.DockStyle.Fill;
            this.led_task_over.EnableCilck = true;
            this.led_task_over.Image = ((System.Drawing.Image)(resources.GetObject("led_task_over.Image")));
            this.led_task_over.Location = new System.Drawing.Point(898, 25);
            this.led_task_over.Name = "led_task_over";
            this.led_task_over.SetSquare = false;
            this.led_task_over.Size = new System.Drawing.Size(175, 37);
            this.led_task_over.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.led_task_over.SubImg = ((System.Drawing.Image)(resources.GetObject("led_task_over.SubImg")));
            this.led_task_over.TabIndex = 1;
            this.led_task_over.TabStop = false;
            this.led_task_over.Tag = ((object)(resources.GetObject("led_task_over.Tag")));
            this.led_task_over.Click += new System.EventHandler(this.led_task_over_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.scaleLabel8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.bind_area_control, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.scaleButton1, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(37, 216);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(503, 436);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // scaleLabel8
            // 
            this.scaleLabel8.AutoSize = true;
            this.scaleLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel8.Factor = 0.4F;
            this.scaleLabel8.Font = new System.Drawing.Font("微軟正黑體", 21.6F, System.Drawing.FontStyle.Bold);
            this.scaleLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel8.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel8.Name = "scaleLabel8";
            this.scaleLabel8.Size = new System.Drawing.Size(497, 54);
            this.scaleLabel8.TabIndex = 0;
            this.scaleLabel8.Text = "區域狀態";
            this.scaleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scaleButton1
            // 
            this.scaleButton1.BackColor = System.Drawing.SystemColors.Control;
            this.scaleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleButton1.Factor = 0.35F;
            this.scaleButton1.Font = new System.Drawing.Font("微軟正黑體", 17.15F);
            this.scaleButton1.Location = new System.Drawing.Point(3, 384);
            this.scaleButton1.Name = "scaleButton1";
            this.scaleButton1.Size = new System.Drawing.Size(497, 49);
            this.scaleButton1.TabIndex = 1;
            this.scaleButton1.Text = "Ready";
            this.scaleButton1.UseVisualStyleBackColor = false;
            this.scaleButton1.Click += new System.EventHandler(this.scaleButton1_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.scaleLabel9, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(546, 216);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.5F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(573, 436);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // scaleLabel9
            // 
            this.scaleLabel9.AutoSize = true;
            this.scaleLabel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel9.Factor = 0.4F;
            this.scaleLabel9.Font = new System.Drawing.Font("微軟正黑體", 21.6F, System.Drawing.FontStyle.Bold);
            this.scaleLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scaleLabel9.Location = new System.Drawing.Point(3, 0);
            this.scaleLabel9.Name = "scaleLabel9";
            this.scaleLabel9.Size = new System.Drawing.Size(567, 54);
            this.scaleLabel9.TabIndex = 0;
            this.scaleLabel9.Text = "任務清單";
            this.scaleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(567, 376);
            this.dataGridView1.TabIndex = 1;
            // 
            // bind_area_control
            // 
            this.bind_area_control.AreaName = "No Area Bind";
            this.bind_area_control.AreaNode = new string[] {
        "1",
        "2",
        "3"};
            this.bind_area_control.Checked = false;
            this.bind_area_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bind_area_control.Location = new System.Drawing.Point(0, 54);
            this.bind_area_control.Margin = new System.Windows.Forms.Padding(0);
            this.bind_area_control.Name = "bind_area_control";
            this.bind_area_control.NodeStatus = new int[] {
        0,
        0,
        0};
            this.bind_area_control.Size = new System.Drawing.Size(503, 327);
            this.bind_area_control.TabIndex = 0;
            // 
            // f02_MainMission
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1159, 691);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f02_MainMission";
            this.Text = "交換站任務";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.led_idle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_turtle_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_move)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_bot_out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.led_task_over)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.KukaAreaControl bind_area_control;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private iCAPS.ScaleLabel scaleLabel1;
        private iCAPS.ScaleLabel scaleLabel2;
        private iCAPS.ScaleLabel scaleLabel3;
        private iCAPS.ScaleLabel scaleLabel4;
        private iCAPS.ScaleLabel scaleLabel5;
        private iCAPS.ScaleLabel scaleLabel6;
        private iCAPS.DoubleImg led_idle;
        private iCAPS.DoubleImg led_turtle_in;
        private iCAPS.DoubleImg led_bot_move;
        private iCAPS.DoubleImg led_bot_in;
        private iCAPS.DoubleImg led_bot_out;
        private iCAPS.DoubleImg led_task_over;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private iCAPS.ScaleLabel scaleLabel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private iCAPS.ScaleLabel scaleLabel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private iCAPS.ScaleLabel scaleLabel9;
        private iCAPS.myPanel myPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private iCAPS.ScaleButton scaleButton1;
    }
}