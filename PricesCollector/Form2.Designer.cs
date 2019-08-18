namespace PricesCollector
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fetchDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateDb = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerValue = new System.Windows.Forms.NumericUpDown();
            this.progressBarUpdateDb = new System.Windows.Forms.ProgressBar();
            this.progressBarFetching = new System.Windows.Forms.ProgressBar();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.store = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.current_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lowest_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.common_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other_seller = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.link = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerValue)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.store,
            this.group,
            this.sku,
            this.active,
            this.current_price,
            this.lowest_price,
            this.common_price,
            this.other_seller,
            this.link});
            this.dataGridView1.Location = new System.Drawing.Point(12, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(2060, 657);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowValidated);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fetchDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2084, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fetchDataToolStripMenuItem
            // 
            this.fetchDataToolStripMenuItem.Name = "fetchDataToolStripMenuItem";
            this.fetchDataToolStripMenuItem.Size = new System.Drawing.Size(129, 36);
            this.fetchDataToolStripMenuItem.Text = "Fetch Tiki";
            this.fetchDataToolStripMenuItem.Click += new System.EventHandler(this.fetchDataToolStripMenuItem_Click);
            // 
            // timerUpdateDb
            // 
            this.timerUpdateDb.Interval = 10000;
            this.timerUpdateDb.Tick += new System.EventHandler(this.timerUpdateDB_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "second";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Timeout:";
            // 
            // timerValue
            // 
            this.timerValue.Location = new System.Drawing.Point(122, 79);
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
            this.timerValue.TabIndex = 7;
            this.timerValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timerValue.ValueChanged += new System.EventHandler(this.timerValue_ValueChanged);
            // 
            // progressBarUpdateDb
            // 
            this.progressBarUpdateDb.Location = new System.Drawing.Point(326, 73);
            this.progressBarUpdateDb.Name = "progressBarUpdateDb";
            this.progressBarUpdateDb.Size = new System.Drawing.Size(325, 43);
            this.progressBarUpdateDb.TabIndex = 10;
            // 
            // progressBarFetching
            // 
            this.progressBarFetching.Location = new System.Drawing.Point(679, 73);
            this.progressBarFetching.Name = "progressBarFetching";
            this.progressBarFetching.Size = new System.Drawing.Size(236, 43);
            this.progressBarFetching.TabIndex = 11;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Width = 77;
            // 
            // store
            // 
            this.store.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.store.HeaderText = "Seller name";
            this.store.Name = "store";
            this.store.Width = 171;
            // 
            // group
            // 
            this.group.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.group.HeaderText = "Group";
            this.group.Name = "group";
            this.group.Width = 116;
            // 
            // sku
            // 
            this.sku.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sku.HeaderText = "SKU";
            this.sku.Name = "sku";
            // 
            // active
            // 
            this.active.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.active.HeaderText = "Active";
            this.active.Name = "active";
            this.active.Width = 77;
            // 
            // current_price
            // 
            this.current_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.current_price.HeaderText = "Current price";
            this.current_price.Name = "current_price";
            this.current_price.Width = 181;
            // 
            // lowest_price
            // 
            this.lowest_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lowest_price.HeaderText = "Lowest price";
            this.lowest_price.Name = "lowest_price";
            this.lowest_price.Width = 178;
            // 
            // common_price
            // 
            this.common_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.common_price.HeaderText = "Market  price";
            this.common_price.Name = "common_price";
            this.common_price.Width = 182;
            // 
            // other_seller
            // 
            this.other_seller.HeaderText = "Other seller";
            this.other_seller.Name = "other_seller";
            this.other_seller.Width = 120;
            // 
            // link
            // 
            this.link.HeaderText = "Link";
            this.link.Name = "link";
            this.link.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.link.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2084, 830);
            this.Controls.Add(this.progressBarFetching);
            this.Controls.Add(this.progressBarUpdateDb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timerValue);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fetchDataToolStripMenuItem;
        private System.Windows.Forms.Timer timerUpdateDb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timerValue;
        private System.Windows.Forms.ProgressBar progressBarUpdateDb;
        private System.Windows.Forms.ProgressBar progressBarFetching;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn store;
        private System.Windows.Forms.DataGridViewTextBoxColumn group;
        private System.Windows.Forms.DataGridViewTextBoxColumn sku;
        private System.Windows.Forms.DataGridViewCheckBoxColumn active;
        private System.Windows.Forms.DataGridViewTextBoxColumn current_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn lowest_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn common_price;
        private System.Windows.Forms.DataGridViewComboBoxColumn other_seller;
        private System.Windows.Forms.DataGridViewLinkColumn link;
    }
}