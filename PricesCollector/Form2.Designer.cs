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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fetchDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateDb = new System.Windows.Forms.Timer(this.components);
            this.progressBarUpdateDb = new System.Windows.Forms.ProgressBar();
            this.progressBarFetching = new System.Windows.Forms.ProgressBar();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.store = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.current_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lowest_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other_seller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.link = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.store,
            this.group,
            this.product_name,
            this.sku,
            this.active,
            this.current_price,
            this.lowest_price,
            this.discount_price,
            this.other_seller,
            this.link});
            this.dataGridView1.Location = new System.Drawing.Point(12, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1775, 657);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowValidated);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fetchDataToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1835, 40);
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
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(160, 36);
            this.exportToolStripMenuItem.Text = "Export Excell";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(103, 36);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // timerUpdateDb
            // 
            this.timerUpdateDb.Interval = 10000;
            this.timerUpdateDb.Tick += new System.EventHandler(this.timerUpdateDB_Tick);
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
            this.id.Frozen = true;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Width = 77;
            // 
            // store
            // 
            this.store.Frozen = true;
            this.store.HeaderText = "Seller name";
            this.store.Name = "store";
            this.store.Width = 171;
            // 
            // group
            // 
            this.group.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.group.Frozen = true;
            this.group.HeaderText = "Group";
            this.group.Name = "group";
            this.group.Width = 116;
            // 
            // product_name
            // 
            this.product_name.Frozen = true;
            this.product_name.HeaderText = "Product Name";
            this.product_name.Name = "product_name";
            // 
            // sku
            // 
            this.sku.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sku.Frozen = true;
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
            this.current_price.Width = 167;
            // 
            // lowest_price
            // 
            this.lowest_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lowest_price.HeaderText = "Lowest price";
            this.lowest_price.Name = "lowest_price";
            this.lowest_price.Width = 164;
            // 
            // discount_price
            // 
            this.discount_price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.discount_price.HeaderText = "Discount price";
            this.discount_price.Name = "discount_price";
            this.discount_price.Width = 179;
            // 
            // other_seller
            // 
            this.other_seller.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.other_seller.DefaultCellStyle = dataGridViewCellStyle1;
            this.other_seller.HeaderText = "Other seller";
            this.other_seller.Name = "other_seller";
            this.other_seller.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.other_seller.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.other_seller.Width = 116;
            // 
            // link
            // 
            this.link.HeaderText = "Link";
            this.link.Name = "link";
            this.link.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.link.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.link.Width = 465;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1835, 830);
            this.Controls.Add(this.progressBarFetching);
            this.Controls.Add(this.progressBarUpdateDb);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Price collector";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fetchDataToolStripMenuItem;
        private System.Windows.Forms.Timer timerUpdateDb;
        private System.Windows.Forms.ProgressBar progressBarUpdateDb;
        private System.Windows.Forms.ProgressBar progressBarFetching;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn store;
        private System.Windows.Forms.DataGridViewTextBoxColumn group;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn sku;
        private System.Windows.Forms.DataGridViewCheckBoxColumn active;
        private System.Windows.Forms.DataGridViewTextBoxColumn current_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn lowest_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn discount_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn other_seller;
        private System.Windows.Forms.DataGridViewLinkColumn link;
    }
}