namespace PricesCollector
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fetchDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuRefreshView = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateDbTiki = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateDbOtherWebsite = new System.Windows.Forms.Timer(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBarFetchingOtherWebsite = new System.Windows.Forms.ProgressBar();
            this.btnStopOtherWebsite = new System.Windows.Forms.Button();
            this.progressBarUpdateDbOtherWebsite = new System.Windows.Forms.ProgressBar();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.progressBarFetchingTiki = new System.Windows.Forms.ProgressBar();
            this.btnStopTiki = new System.Windows.Forms.Button();
            this.progressBarUpdateDbTiki = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearchTiki = new System.Windows.Forms.RichTextBox();
            this.btnSearchTiki = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearchOtherWebsite = new System.Windows.Forms.Button();
            this.txtSearchOtherWebsite = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fetchDataToolStripMenuItem,
            this.toolStripMenuRefreshView,
            this.settingToolStripMenuItem,
            this.importDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1836, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fetchDataToolStripMenuItem
            // 
            this.fetchDataToolStripMenuItem.Name = "fetchDataToolStripMenuItem";
            this.fetchDataToolStripMenuItem.Size = new System.Drawing.Size(144, 36);
            this.fetchDataToolStripMenuItem.Text = "Fetch price";
            this.fetchDataToolStripMenuItem.Click += new System.EventHandler(this.fetchDataToolStripMenuItem_Click);
            // 
            // toolStripMenuRefreshView
            // 
            this.toolStripMenuRefreshView.Name = "toolStripMenuRefreshView";
            this.toolStripMenuRefreshView.Size = new System.Drawing.Size(161, 36);
            this.toolStripMenuRefreshView.Text = "Refresh view";
            this.toolStripMenuRefreshView.Click += new System.EventHandler(this.toolStripMenuRefreshView_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(103, 36);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // importDataToolStripMenuItem
            // 
            this.importDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importCSVToolStripMenuItem,
            this.exportExcelToolStripMenuItem});
            this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
            this.importDataToolStripMenuItem.Size = new System.Drawing.Size(76, 36);
            this.importDataToolStripMenuItem.Text = "Data";
            // 
            // importCSVToolStripMenuItem
            // 
            this.importCSVToolStripMenuItem.Name = "importCSVToolStripMenuItem";
            this.importCSVToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.importCSVToolStripMenuItem.Text = "Import data";
            this.importCSVToolStripMenuItem.Click += new System.EventHandler(this.importCSVToolStripMenuItem_Click);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.exportExcelToolStripMenuItem.Text = "Export Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // timerUpdateDbTiki
            // 
            this.timerUpdateDbTiki.Interval = 10000;
            this.timerUpdateDbTiki.Tick += new System.EventHandler(this.timerUpdateDBTiki_Tick);
            // 
            // timerUpdateDbOtherWebsite
            // 
            this.timerUpdateDbOtherWebsite.Interval = 10000;
            this.timerUpdateDbOtherWebsite.Tick += new System.EventHandler(this.timerUpdateDbOtherWebsite_Tick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1795, 714);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Website";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBarFetchingOtherWebsite
            // 
            this.progressBarFetchingOtherWebsite.Location = new System.Drawing.Point(1241, 15);
            this.progressBarFetchingOtherWebsite.Name = "progressBarFetchingOtherWebsite";
            this.progressBarFetchingOtherWebsite.Size = new System.Drawing.Size(218, 28);
            this.progressBarFetchingOtherWebsite.TabIndex = 7;
            // 
            // btnStopOtherWebsite
            // 
            this.btnStopOtherWebsite.Location = new System.Drawing.Point(1489, 7);
            this.btnStopOtherWebsite.Name = "btnStopOtherWebsite";
            this.btnStopOtherWebsite.Size = new System.Drawing.Size(103, 45);
            this.btnStopOtherWebsite.TabIndex = 6;
            this.btnStopOtherWebsite.Text = "Stop";
            this.btnStopOtherWebsite.UseVisualStyleBackColor = true;
            this.btnStopOtherWebsite.Click += new System.EventHandler(this.btnStopOtherWebsite_Click);
            // 
            // progressBarUpdateDbOtherWebsite
            // 
            this.progressBarUpdateDbOtherWebsite.Location = new System.Drawing.Point(815, 15);
            this.progressBarUpdateDbOtherWebsite.Name = "progressBarUpdateDbOtherWebsite";
            this.progressBarUpdateDbOtherWebsite.Size = new System.Drawing.Size(392, 28);
            this.progressBarUpdateDbOtherWebsite.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(4, 76);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 33;
            this.dataGridView2.Size = new System.Drawing.Size(1785, 632);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1795, 714);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tiki";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // progressBarFetchingTiki
            // 
            this.progressBarFetchingTiki.Location = new System.Drawing.Point(1236, 17);
            this.progressBarFetchingTiki.Name = "progressBarFetchingTiki";
            this.progressBarFetchingTiki.Size = new System.Drawing.Size(218, 28);
            this.progressBarFetchingTiki.TabIndex = 4;
            // 
            // btnStopTiki
            // 
            this.btnStopTiki.Location = new System.Drawing.Point(1470, 9);
            this.btnStopTiki.Name = "btnStopTiki";
            this.btnStopTiki.Size = new System.Drawing.Size(103, 45);
            this.btnStopTiki.TabIndex = 3;
            this.btnStopTiki.Text = "Stop";
            this.btnStopTiki.UseVisualStyleBackColor = true;
            this.btnStopTiki.Click += new System.EventHandler(this.btnStopTiki_Click);
            // 
            // progressBarUpdateDbTiki
            // 
            this.progressBarUpdateDbTiki.Location = new System.Drawing.Point(824, 17);
            this.progressBarUpdateDbTiki.Name = "progressBarUpdateDbTiki";
            this.progressBarUpdateDbTiki.Size = new System.Drawing.Size(392, 28);
            this.progressBarUpdateDbTiki.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 77);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1784, 630);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1811, 761);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearchTiki);
            this.panel1.Controls.Add(this.txtSearchTiki);
            this.panel1.Controls.Add(this.btnStopTiki);
            this.panel1.Controls.Add(this.progressBarFetchingTiki);
            this.panel1.Controls.Add(this.progressBarUpdateDbTiki);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1782, 64);
            this.panel1.TabIndex = 6;
            // 
            // txtSearchTiki
            // 
            this.txtSearchTiki.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchTiki.Location = new System.Drawing.Point(3, 9);
            this.txtSearchTiki.Name = "txtSearchTiki";
            this.txtSearchTiki.Size = new System.Drawing.Size(575, 45);
            this.txtSearchTiki.TabIndex = 5;
            this.txtSearchTiki.Text = "";
            this.txtSearchTiki.TextChanged += new System.EventHandler(this.txtSearchTiki_TextChanged);
            // 
            // btnSearchTiki
            // 
            this.btnSearchTiki.Location = new System.Drawing.Point(594, 9);
            this.btnSearchTiki.Name = "btnSearchTiki";
            this.btnSearchTiki.Size = new System.Drawing.Size(111, 45);
            this.btnSearchTiki.TabIndex = 6;
            this.btnSearchTiki.Text = "Search";
            this.btnSearchTiki.UseVisualStyleBackColor = true;
            this.btnSearchTiki.Click += new System.EventHandler(this.btnSearchTiki_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearchOtherWebsite);
            this.panel2.Controls.Add(this.txtSearchOtherWebsite);
            this.panel2.Controls.Add(this.progressBarFetchingOtherWebsite);
            this.panel2.Controls.Add(this.progressBarUpdateDbOtherWebsite);
            this.panel2.Controls.Add(this.btnStopOtherWebsite);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1783, 64);
            this.panel2.TabIndex = 8;
            // 
            // btnSearchOtherWebsite
            // 
            this.btnSearchOtherWebsite.Location = new System.Drawing.Point(594, 7);
            this.btnSearchOtherWebsite.Name = "btnSearchOtherWebsite";
            this.btnSearchOtherWebsite.Size = new System.Drawing.Size(111, 45);
            this.btnSearchOtherWebsite.TabIndex = 9;
            this.btnSearchOtherWebsite.Text = "Search";
            this.btnSearchOtherWebsite.UseVisualStyleBackColor = true;
            this.btnSearchOtherWebsite.Click += new System.EventHandler(this.btnSearchOtherWebsite_Click);
            // 
            // txtSearchOtherWebsite
            // 
            this.txtSearchOtherWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchOtherWebsite.Location = new System.Drawing.Point(3, 7);
            this.txtSearchOtherWebsite.Name = "txtSearchOtherWebsite";
            this.txtSearchOtherWebsite.Size = new System.Drawing.Size(575, 45);
            this.txtSearchOtherWebsite.TabIndex = 8;
            this.txtSearchOtherWebsite.Text = "";
            this.txtSearchOtherWebsite.TextChanged += new System.EventHandler(this.txtSearchOtherWebsite_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 831);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price collector";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fetchDataToolStripMenuItem;
        private System.Windows.Forms.Timer timerUpdateDbTiki;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRefreshView;
        private System.Windows.Forms.Timer timerUpdateDbOtherWebsite;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar progressBarFetchingOtherWebsite;
        private System.Windows.Forms.Button btnStopOtherWebsite;
        private System.Windows.Forms.ProgressBar progressBarUpdateDbOtherWebsite;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ProgressBar progressBarFetchingTiki;
        private System.Windows.Forms.Button btnStopTiki;
        private System.Windows.Forms.ProgressBar progressBarUpdateDbTiki;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txtSearchTiki;
        private System.Windows.Forms.Button btnSearchTiki;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearchOtherWebsite;
        private System.Windows.Forms.RichTextBox txtSearchOtherWebsite;
    }
}