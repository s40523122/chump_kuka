namespace Chump_kuka.Forms
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.scalePadding1 = new iCAPS.ScalePadding();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.scaleLabel1 = new iCAPS.ScaleLabel();
            this.kuka_api_check = new iCAPS.DoubleImg();
            this.lable1 = new iCAPS.ScaleLabel();
            this.scaleLabel3 = new iCAPS.ScaleLabel();
            this.scaleLabel4 = new iCAPS.ScaleLabel();
            this.scaleLabel5 = new iCAPS.ScaleLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.scaleLabel6 = new iCAPS.ScaleLabel();
            this.scaleButton1 = new iCAPS.ScaleButton();
            this.doubleImg2 = new iCAPS.DoubleImg();
            this.sensor_check = new iCAPS.DoubleImg();
            this.scalePadding1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kuka_api_check)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensor_check)).BeginInit();
            this.SuspendLayout();
            // 
            // scalePadding1
            // 
            this.scalePadding1.ColumnCount = 3;
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.scalePadding1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.scalePadding1.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.scalePadding1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scalePadding1.Init = true;
            this.scalePadding1.Location = new System.Drawing.Point(0, 0);
            this.scalePadding1.Name = "scalePadding1";
            this.scalePadding1.RowCount = 3;
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.scalePadding1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.scalePadding1.SetColumnRatio = 5F;
            this.scalePadding1.SetRowRatio = 3F;
            this.scalePadding1.Size = new System.Drawing.Size(981, 580);
            this.scalePadding1.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.scaleLabel1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.kuka_api_check, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lable1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.scaleLabel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.scaleLabel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.scaleLabel5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.scaleLabel6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.scaleButton1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.doubleImg2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.sensor_check, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(49, 17);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(882, 545);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 3);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 535);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(876, 7);
            this.progressBar1.TabIndex = 0;
            // 
            // scaleLabel1
            // 
            this.scaleLabel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.scaleLabel1, 3);
            this.scaleLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel1.Factor = 0.4F;
            this.scaleLabel1.Font = new System.Drawing.Font("微軟正黑體", 6.4F);
            this.scaleLabel1.Location = new System.Drawing.Point(3, 516);
            this.scaleLabel1.Name = "scaleLabel1";
            this.scaleLabel1.Size = new System.Drawing.Size(876, 16);
            this.scaleLabel1.TabIndex = 1;
            this.scaleLabel1.Text = "等待連線";
            this.scaleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kuka_api_check
            // 
            this.kuka_api_check.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kuka_api_check.Change = false;
            this.kuka_api_check.EnableCilck = true;
            this.kuka_api_check.Image = ((System.Drawing.Image)(resources.GetObject("kuka_api_check.Image")));
            this.kuka_api_check.Location = new System.Drawing.Point(710, 5);
            this.kuka_api_check.Margin = new System.Windows.Forms.Padding(5);
            this.kuka_api_check.Name = "kuka_api_check";
            this.kuka_api_check.SetSquare = true;
            this.kuka_api_check.Size = new System.Drawing.Size(28, 28);
            this.kuka_api_check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.kuka_api_check.SubImg = ((System.Drawing.Image)(resources.GetObject("kuka_api_check.SubImg")));
            this.kuka_api_check.TabIndex = 2;
            this.kuka_api_check.TabStop = false;
            this.kuka_api_check.Tag = ((object)(resources.GetObject("kuka_api_check.Tag")));
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lable1.Factor = 0.3F;
            this.lable1.Font = new System.Drawing.Font("微軟正黑體", 11.4F, System.Drawing.FontStyle.Bold);
            this.lable1.Location = new System.Drawing.Point(3, 0);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(258, 38);
            this.lable1.TabIndex = 3;
            this.lable1.Text = "KUKA API IP";
            this.lable1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scaleLabel3
            // 
            this.scaleLabel3.AutoSize = true;
            this.scaleLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel3.Factor = 0.3F;
            this.scaleLabel3.Font = new System.Drawing.Font("微軟正黑體", 11.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel3.Location = new System.Drawing.Point(3, 38);
            this.scaleLabel3.Name = "scaleLabel3";
            this.scaleLabel3.Size = new System.Drawing.Size(258, 38);
            this.scaleLabel3.TabIndex = 3;
            this.scaleLabel3.Text = "是否為伺服器";
            this.scaleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scaleLabel4
            // 
            this.scaleLabel4.AutoSize = true;
            this.scaleLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel4.Factor = 0.3F;
            this.scaleLabel4.Font = new System.Drawing.Font("微軟正黑體", 11.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel4.Location = new System.Drawing.Point(3, 76);
            this.scaleLabel4.Name = "scaleLabel4";
            this.scaleLabel4.Size = new System.Drawing.Size(258, 38);
            this.scaleLabel4.TabIndex = 3;
            this.scaleLabel4.Text = "伺服器IP";
            this.scaleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scaleLabel5
            // 
            this.scaleLabel5.AutoSize = true;
            this.scaleLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel5.Factor = 0.3F;
            this.scaleLabel5.Font = new System.Drawing.Font("微軟正黑體", 11.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel5.Location = new System.Drawing.Point(3, 114);
            this.scaleLabel5.Name = "scaleLabel5";
            this.scaleLabel5.Size = new System.Drawing.Size(258, 38);
            this.scaleLabel5.TabIndex = 3;
            this.scaleLabel5.Text = "感測模組IP";
            this.scaleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(267, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "http://192.168.68.64:10870/interfaces/api/amr/";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(267, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(435, 22);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "192.168.68.64";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(267, 122);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(435, 22);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "192.168.255.1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.checkBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(264, 38);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(441, 38);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox1.Location = new System.Drawing.Point(113, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 32);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "是";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox2.Location = new System.Drawing.Point(223, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(104, 32);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "否";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(267, 199);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(435, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // scaleLabel6
            // 
            this.scaleLabel6.AutoSize = true;
            this.scaleLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleLabel6.Factor = 0.3F;
            this.scaleLabel6.Font = new System.Drawing.Font("微軟正黑體", 11.4F, System.Drawing.FontStyle.Bold);
            this.scaleLabel6.Location = new System.Drawing.Point(3, 190);
            this.scaleLabel6.Name = "scaleLabel6";
            this.scaleLabel6.Size = new System.Drawing.Size(258, 38);
            this.scaleLabel6.TabIndex = 3;
            this.scaleLabel6.Text = "綁定區域";
            this.scaleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scaleButton1
            // 
            this.scaleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.scaleButton1.AutoSize = true;
            this.scaleButton1.Factor = 0.3F;
            this.scaleButton1.Font = new System.Drawing.Font("微軟正黑體", 9.6F);
            this.scaleButton1.Location = new System.Drawing.Point(422, 155);
            this.scaleButton1.Name = "scaleButton1";
            this.scaleButton1.Size = new System.Drawing.Size(124, 32);
            this.scaleButton1.TabIndex = 7;
            this.scaleButton1.Text = "通訊測試";
            this.scaleButton1.UseVisualStyleBackColor = true;
            this.scaleButton1.Click += new System.EventHandler(this.scaleButton1_Click);
            // 
            // doubleImg2
            // 
            this.doubleImg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doubleImg2.Change = false;
            this.doubleImg2.EnableCilck = true;
            this.doubleImg2.Image = ((System.Drawing.Image)(resources.GetObject("doubleImg2.Image")));
            this.doubleImg2.Location = new System.Drawing.Point(710, 81);
            this.doubleImg2.Margin = new System.Windows.Forms.Padding(5);
            this.doubleImg2.Name = "doubleImg2";
            this.doubleImg2.SetSquare = true;
            this.doubleImg2.Size = new System.Drawing.Size(28, 28);
            this.doubleImg2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.doubleImg2.SubImg = ((System.Drawing.Image)(resources.GetObject("doubleImg2.SubImg")));
            this.doubleImg2.TabIndex = 2;
            this.doubleImg2.TabStop = false;
            // 
            // sensor_check
            // 
            this.sensor_check.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sensor_check.Change = false;
            this.sensor_check.EnableCilck = true;
            this.sensor_check.Image = ((System.Drawing.Image)(resources.GetObject("sensor_check.Image")));
            this.sensor_check.Location = new System.Drawing.Point(710, 119);
            this.sensor_check.Margin = new System.Windows.Forms.Padding(5);
            this.sensor_check.Name = "sensor_check";
            this.sensor_check.SetSquare = true;
            this.sensor_check.Size = new System.Drawing.Size(28, 28);
            this.sensor_check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sensor_check.SubImg = ((System.Drawing.Image)(resources.GetObject("sensor_check.SubImg")));
            this.sensor_check.TabIndex = 2;
            this.sensor_check.TabStop = false;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 580);
            this.Controls.Add(this.scalePadding1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Setting";
            this.scalePadding1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kuka_api_check)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.doubleImg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensor_check)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private iCAPS.ScalePadding scalePadding1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private iCAPS.ScaleLabel scaleLabel1;
        private iCAPS.DoubleImg kuka_api_check;
        private iCAPS.ScaleLabel lable1;
        private iCAPS.ScaleLabel scaleLabel3;
        private iCAPS.ScaleLabel scaleLabel4;
        private iCAPS.ScaleLabel scaleLabel5;
        private iCAPS.ScaleLabel scaleLabel6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private iCAPS.ScaleButton scaleButton1;
        private iCAPS.DoubleImg doubleImg2;
        private iCAPS.DoubleImg sensor_check;
    }
}