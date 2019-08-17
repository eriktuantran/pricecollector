namespace PricesCollector
{
    partial class AppSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSetting));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblDbStatus = new System.Windows.Forms.Label();
            this.btnTestDb = new System.Windows.Forms.Button();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDbPasswd = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDbIP = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.progressbarWorker = new System.ComponentModel.BackgroundWorker();
            this.dbConnectionTester = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.timerValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(358, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(162, 43);
            this.progressBar1.TabIndex = 26;
            // 
            // lblDbStatus
            // 
            this.lblDbStatus.AutoSize = true;
            this.lblDbStatus.Location = new System.Drawing.Point(225, 345);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(73, 25);
            this.lblDbStatus.TabIndex = 24;
            this.lblDbStatus.Text = "Status";
            // 
            // btnTestDb
            // 
            this.btnTestDb.Location = new System.Drawing.Point(23, 336);
            this.btnTestDb.Name = "btnTestDb";
            this.btnTestDb.Size = new System.Drawing.Size(188, 43);
            this.btnTestDb.TabIndex = 15;
            this.btnTestDb.Text = "Test connection";
            this.btnTestDb.UseVisualStyleBackColor = true;
            this.btnTestDb.Click += new System.EventHandler(this.btnTestDb_Click);
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(136, 263);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(388, 31);
            this.txtDbName.TabIndex = 14;
            this.txtDbName.Text = "pricecollector";
            this.txtDbName.TextChanged += new System.EventHandler(this.txtDbIP_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 25);
            this.label12.TabIndex = 18;
            this.label12.Text = "DB name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 17;
            this.label13.Text = "Passwd:";
            // 
            // txtDbPasswd
            // 
            this.txtDbPasswd.Location = new System.Drawing.Point(136, 207);
            this.txtDbPasswd.Name = "txtDbPasswd";
            this.txtDbPasswd.Size = new System.Drawing.Size(388, 31);
            this.txtDbPasswd.TabIndex = 13;
            this.txtDbPasswd.Text = "3V5wn0Kv9RRc8gQA";
            this.txtDbPasswd.UseSystemPasswordChar = true;
            this.txtDbPasswd.TextChanged += new System.EventHandler(this.txtDbIP_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 25);
            this.label15.TabIndex = 15;
            this.label15.Text = "Port:";
            // 
            // txtDbPort
            // 
            this.txtDbPort.Location = new System.Drawing.Point(136, 99);
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Size = new System.Drawing.Size(388, 31);
            this.txtDbPort.TabIndex = 11;
            this.txtDbPort.Text = "3306";
            this.txtDbPort.TextChanged += new System.EventHandler(this.txtDbIP_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(23, 155);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 25);
            this.label16.TabIndex = 13;
            this.label16.Text = "User:";
            // 
            // txtDbUser
            // 
            this.txtDbUser.Location = new System.Drawing.Point(136, 152);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(388, 31);
            this.txtDbUser.TabIndex = 12;
            this.txtDbUser.Text = "root";
            this.txtDbUser.TextChanged += new System.EventHandler(this.txtDbIP_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(23, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 25);
            this.label17.TabIndex = 7;
            this.label17.Text = "Server:";
            // 
            // txtDbIP
            // 
            this.txtDbIP.Location = new System.Drawing.Point(136, 46);
            this.txtDbIP.Name = "txtDbIP";
            this.txtDbIP.Size = new System.Drawing.Size(388, 31);
            this.txtDbIP.TabIndex = 10;
            this.txtDbIP.Text = "127.0.0.1";
            this.txtDbIP.TextChanged += new System.EventHandler(this.txtDbIP_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(247, 604);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 53);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // progressbarWorker
            // 
            this.progressbarWorker.WorkerSupportsCancellation = true;
            this.progressbarWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.progressbarWorker_DoWork);
            this.progressbarWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.progressbarWorker_ProgressChanged);
            // 
            // dbConnectionTester
            // 
            this.dbConnectionTester.WorkerSupportsCancellation = true;
            this.dbConnectionTester.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dbConnectionTester_DoWork);
            this.dbConnectionTester.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dbConnectionTester_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "second";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "Timeout:";
            // 
            // timerValue
            // 
            this.timerValue.Location = new System.Drawing.Point(136, 42);
            this.timerValue.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.timerValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timerValue.Name = "timerValue";
            this.timerValue.Size = new System.Drawing.Size(86, 31);
            this.timerValue.TabIndex = 24;
            this.timerValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timerValue.ValueChanged += new System.EventHandler(this.timerValue_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.timerValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(27, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 100);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timeout setting:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.txtDbName);
            this.groupBox2.Controls.Add(this.lblDbStatus);
            this.groupBox2.Controls.Add(this.txtDbIP);
            this.groupBox2.Controls.Add(this.btnTestDb);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtDbUser);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtDbPort);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtDbPasswd);
            this.groupBox2.Location = new System.Drawing.Point(27, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 402);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database setting:";
            // 
            // AppSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 690);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)(this.timerValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDbPasswd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDbIP;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDbStatus;
        private System.Windows.Forms.Button btnTestDb;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker progressbarWorker;
        private System.ComponentModel.BackgroundWorker dbConnectionTester;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timerValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}