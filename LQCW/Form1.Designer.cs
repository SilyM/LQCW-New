namespace _LQCW
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbStop = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tsStopBits = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDataBits = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsBaudRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSpNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsParity = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tmSend = new System.Windows.Forms.Timer(this.components);
            this.label = new System.Windows.Forms.Label();
            this.BatteryPower = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gatawayID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.session2 = new System.Windows.Forms.Label();
            this.COM = new System.Windows.Forms.Label();
            this.COM2 = new System.Windows.Forms.Label();
            this.COM3 = new System.Windows.Forms.Label();
            this.COM4 = new System.Windows.Forms.Label();
            this.session3 = new System.Windows.Forms.Label();
            this.session1 = new System.Windows.Forms.Label();
            this.session4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 228);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(963, 328);
            this.dataGridView1.TabIndex = 20;
            // 
            // cbSerial
            // 
            this.cbSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(85, 29);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(121, 24);
            this.cbSerial.TabIndex = 38;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbParity);
            this.groupBox3.Controls.Add(this.cbStop);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbDataBits);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbBaudRate);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(26, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 100);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "串口设置";
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.cbParity.Location = new System.Drawing.Point(275, 51);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(97, 24);
            this.cbParity.TabIndex = 37;
            // 
            // cbStop
            // 
            this.cbStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStop.FormattingEnabled = true;
            this.cbStop.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbStop.Location = new System.Drawing.Point(97, 51);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(83, 24);
            this.cbStop.TabIndex = 36;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(211, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 14);
            this.label8.TabIndex = 34;
            this.label8.Text = "校验位：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(35, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 35;
            this.label7.Text = "停止位：";
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(275, 25);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(97, 24);
            this.cbDataBits.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(212, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 32;
            this.label6.Text = "数据位：";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(97, 25);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(83, 24);
            this.cbBaudRate.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 30;
            this.label5.Text = "波特率:";
            // 
            // tsStopBits
            // 
            this.tsStopBits.Name = "tsStopBits";
            this.tsStopBits.Size = new System.Drawing.Size(86, 17);
            this.tsStopBits.Text = "停止位:未指定|";
            // 
            // tsDataBits
            // 
            this.tsDataBits.Name = "tsDataBits";
            this.tsDataBits.Size = new System.Drawing.Size(86, 17);
            this.tsDataBits.Text = "数据位:未指定|";
            // 
            // tsBaudRate
            // 
            this.tsBaudRate.Name = "tsBaudRate";
            this.tsBaudRate.Size = new System.Drawing.Size(86, 17);
            this.tsBaudRate.Text = "波特率:未指定|";
            // 
            // tsSpNum
            // 
            this.tsSpNum.Name = "tsSpNum";
            this.tsSpNum.Size = new System.Drawing.Size(95, 17);
            this.tsSpNum.Text = "串口号：未指定|";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSpNum,
            this.tsBaudRate,
            this.tsDataBits,
            this.tsStopBits,
            this.tsParity});
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsParity
            // 
            this.tsParity.Name = "tsParity";
            this.tsParity.Size = new System.Drawing.Size(86, 17);
            this.tsParity.Text = "停止位:未指定|";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(803, 571);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(913, 571);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 23);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(19, 20);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(460, 134);
            this.txtReceive.TabIndex = 0;
            this.txtReceive.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(339, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "保存设置";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtReceive);
            this.groupBox2.Location = new System.Drawing.Point(522, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 173);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(234, 30);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(99, 23);
            this.btnSwitch.TabIndex = 2;
            this.btnSwitch.Text = "打开串口";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSerial);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnSwitch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 173);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口配置";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(865, 199);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(65, 12);
            this.label.TabIndex = 21;
            this.label.Text = "电池电量：";
            // 
            // BatteryPower
            // 
            this.BatteryPower.AutoSize = true;
            this.BatteryPower.Location = new System.Drawing.Point(936, 199);
            this.BatteryPower.Name = "BatteryPower";
            this.BatteryPower.Size = new System.Drawing.Size(0, 12);
            this.BatteryPower.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "网关ID：";
            // 
            // gatawayID
            // 
            this.gatawayID.AutoSize = true;
            this.gatawayID.Location = new System.Drawing.Point(96, 199);
            this.gatawayID.Name = "gatawayID";
            this.gatawayID.Size = new System.Drawing.Size(0, 12);
            this.gatawayID.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "通道：";
            // 
            // session2
            // 
            this.session2.AutoSize = true;
            this.session2.Location = new System.Drawing.Point(317, 213);
            this.session2.Name = "session2";
            this.session2.Size = new System.Drawing.Size(0, 12);
            this.session2.TabIndex = 29;
            // 
            // COM
            // 
            this.COM.AutoSize = true;
            this.COM.Location = new System.Drawing.Point(264, 187);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(0, 12);
            this.COM.TabIndex = 30;
            // 
            // COM2
            // 
            this.COM2.AutoSize = true;
            this.COM2.Location = new System.Drawing.Point(317, 187);
            this.COM2.Name = "COM2";
            this.COM2.Size = new System.Drawing.Size(0, 12);
            this.COM2.TabIndex = 31;
            // 
            // COM3
            // 
            this.COM3.AutoSize = true;
            this.COM3.Location = new System.Drawing.Point(379, 187);
            this.COM3.Name = "COM3";
            this.COM3.Size = new System.Drawing.Size(0, 12);
            this.COM3.TabIndex = 32;
            // 
            // COM4
            // 
            this.COM4.AutoSize = true;
            this.COM4.Location = new System.Drawing.Point(438, 187);
            this.COM4.Name = "COM4";
            this.COM4.Size = new System.Drawing.Size(0, 12);
            this.COM4.TabIndex = 33;
            // 
            // session3
            // 
            this.session3.AutoSize = true;
            this.session3.Location = new System.Drawing.Point(379, 213);
            this.session3.Name = "session3";
            this.session3.Size = new System.Drawing.Size(0, 12);
            this.session3.TabIndex = 34;
            // 
            // session1
            // 
            this.session1.AutoSize = true;
            this.session1.Location = new System.Drawing.Point(264, 213);
            this.session1.Name = "session1";
            this.session1.Size = new System.Drawing.Size(0, 12);
            this.session1.TabIndex = 35;
            // 
            // session4
            // 
            this.session4.AutoSize = true;
            this.session4.Location = new System.Drawing.Point(438, 213);
            this.session4.Name = "session4";
            this.session4.Size = new System.Drawing.Size(0, 12);
            this.session4.TabIndex = 36;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LQCW.Properties.Resources.logo_02;
            this.pictureBox1.Location = new System.Drawing.Point(181, 609);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(395, 57);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "传感器：";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(703, 574);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 39;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "开启滚动条";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 631);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.session4);
            this.Controls.Add(this.session1);
            this.Controls.Add(this.session3);
            this.Controls.Add(this.COM4);
            this.Controls.Add(this.COM3);
            this.Controls.Add(this.COM2);
            this.Controls.Add(this.COM);
            this.Controls.Add(this.session2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gatawayID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BatteryPower);
            this.Controls.Add(this.label);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "北京七芯中创科技有限公司";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbSerial;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbStop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripStatusLabel tsStopBits;
        private System.Windows.Forms.ToolStripStatusLabel tsDataBits;
        private System.Windows.Forms.ToolStripStatusLabel tsBaudRate;
        private System.Windows.Forms.ToolStripStatusLabel tsSpNum;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsParity;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RichTextBox txtReceive;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer tmSend;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label BatteryPower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label gatawayID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label session2;
        private System.Windows.Forms.Label COM;
        private System.Windows.Forms.Label COM2;
        private System.Windows.Forms.Label COM3;
        private System.Windows.Forms.Label COM4;
        private System.Windows.Forms.Label session3;
        private System.Windows.Forms.Label session1;
        private System.Windows.Forms.Label session4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

