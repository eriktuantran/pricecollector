namespace PricesCollector
{
    partial class ImportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportData));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCleanDb = new System.Windows.Forms.CheckBox();
            this.btnOpenCsv = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLinkSendo = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLinkShopee = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLinkLazada = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMinimumPrice = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewId = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.txtMsku = new System.Windows.Forms.TextBox();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtSyncCode = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLinkTiki = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkCleanDb);
            this.groupBox1.Controls.Add(this.btnOpenCsv);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1734, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import from CSV file with column headers: id,product_sync_code,product_group,prod" +
    "uct_code,sku,msku,active,link";
            // 
            // chkCleanDb
            // 
            this.chkCleanDb.AutoSize = true;
            this.chkCleanDb.Location = new System.Drawing.Point(30, 40);
            this.chkCleanDb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCleanDb.Name = "chkCleanDb";
            this.chkCleanDb.Size = new System.Drawing.Size(225, 29);
            this.chkCleanDb.TabIndex = 2;
            this.chkCleanDb.Text = "Clean up database";
            this.chkCleanDb.UseVisualStyleBackColor = true;
            // 
            // btnOpenCsv
            // 
            this.btnOpenCsv.Location = new System.Drawing.Point(30, 81);
            this.btnOpenCsv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenCsv.Name = "btnOpenCsv";
            this.btnOpenCsv.Size = new System.Drawing.Size(116, 50);
            this.btnOpenCsv.TabIndex = 0;
            this.btnOpenCsv.Text = "Import...";
            this.btnOpenCsv.UseVisualStyleBackColor = true;
            this.btnOpenCsv.Click += new System.EventHandler(this.btnOpenCsv_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(648, 1302);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(162, 54);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtLinkSendo);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtLinkShopee);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtLinkLazada);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtMinimumPrice);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnNewId);
            this.groupBox2.Controls.Add(this.btnAddProduct);
            this.groupBox2.Controls.Add(this.txtMsku);
            this.groupBox2.Controls.Add(this.txtSku);
            this.groupBox2.Controls.Add(this.txtSyncCode);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Controls.Add(this.txtCode);
            this.groupBox2.Controls.Add(this.chkActive);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbGroup);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLinkTiki);
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1734, 783);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add / Update product:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(48, 146);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 25);
            this.label15.TabIndex = 24;
            this.label15.Text = "Tiki";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(36, 621);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 25);
            this.label13.TabIndex = 23;
            this.label13.Text = "links";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 588);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 25);
            this.label14.TabIndex = 22;
            this.label14.Text = "Sendo";
            // 
            // txtLinkSendo
            // 
            this.txtLinkSendo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkSendo.Location = new System.Drawing.Point(106, 548);
            this.txtLinkSendo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLinkSendo.Name = "txtLinkSendo";
            this.txtLinkSendo.Size = new System.Drawing.Size(1610, 144);
            this.txtLinkSendo.TabIndex = 21;
            this.txtLinkSendo.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 456);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 25);
            this.label11.TabIndex = 20;
            this.label11.Text = "links";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 425);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 25);
            this.label12.TabIndex = 19;
            this.label12.Text = "Shopee";
            // 
            // txtLinkShopee
            // 
            this.txtLinkShopee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkShopee.Location = new System.Drawing.Point(106, 383);
            this.txtLinkShopee.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLinkShopee.Name = "txtLinkShopee";
            this.txtLinkShopee.Size = new System.Drawing.Size(1610, 144);
            this.txtLinkShopee.TabIndex = 18;
            this.txtLinkShopee.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 292);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 25);
            this.label1.TabIndex = 17;
            this.label1.Text = "links";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 262);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 25);
            this.label10.TabIndex = 16;
            this.label10.Text = "Lazada";
            // 
            // txtLinkLazada
            // 
            this.txtLinkLazada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkLazada.Location = new System.Drawing.Point(106, 219);
            this.txtLinkLazada.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLinkLazada.Name = "txtLinkLazada";
            this.txtLinkLazada.Size = new System.Drawing.Size(1610, 144);
            this.txtLinkLazada.TabIndex = 15;
            this.txtLinkLazada.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1322, 88);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 25);
            this.label9.TabIndex = 13;
            this.label9.Text = "Minimum price";
            // 
            // txtMinimumPrice
            // 
            this.txtMinimumPrice.Location = new System.Drawing.Point(1474, 85);
            this.txtMinimumPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMinimumPrice.Name = "txtMinimumPrice";
            this.txtMinimumPrice.Size = new System.Drawing.Size(242, 31);
            this.txtMinimumPrice.TabIndex = 14;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(282, 715);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(124, 56);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewId
            // 
            this.btnNewId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewId.Location = new System.Drawing.Point(292, 27);
            this.btnNewId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewId.Name = "btnNewId";
            this.btnNewId.Size = new System.Drawing.Size(90, 50);
            this.btnNewId.TabIndex = 11;
            this.btnNewId.Text = "New";
            this.btnNewId.UseVisualStyleBackColor = true;
            this.btnNewId.Click += new System.EventHandler(this.btnNewId_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.Location = new System.Drawing.Point(104, 715);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(134, 56);
            this.btnAddProduct.TabIndex = 10;
            this.btnAddProduct.Text = "Add";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // txtMsku
            // 
            this.txtMsku.Location = new System.Drawing.Point(980, 87);
            this.txtMsku.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMsku.Name = "txtMsku";
            this.txtMsku.Size = new System.Drawing.Size(280, 31);
            this.txtMsku.TabIndex = 9;
            // 
            // txtSku
            // 
            this.txtSku.Location = new System.Drawing.Point(980, 38);
            this.txtSku.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(280, 31);
            this.txtSku.TabIndex = 8;
            // 
            // txtSyncCode
            // 
            this.txtSyncCode.Location = new System.Drawing.Point(560, 87);
            this.txtSyncCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSyncCode.Name = "txtSyncCode";
            this.txtSyncCode.Size = new System.Drawing.Size(284, 31);
            this.txtSyncCode.TabIndex = 7;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(106, 37);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(174, 31);
            this.txtId.TabIndex = 6;
            this.txtId.TextChanged += new System.EventHandler(this.txtId_TextChanged);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(106, 87);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(272, 31);
            this.txtCode.TabIndex = 5;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(1394, 35);
            this.chkActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkActive.Name = "chkActive";
            this.chkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkActive.Size = new System.Drawing.Size(103, 29);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(902, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "SKU";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Code";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(900, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "MSKU";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(438, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Sync code";
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(560, 37);
            this.cmbGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(284, 33);
            this.cmbGroup.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(482, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Group";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 177);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "link";
            // 
            // txtLinkTiki
            // 
            this.txtLinkTiki.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkTiki.Location = new System.Drawing.Point(106, 135);
            this.txtLinkTiki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLinkTiki.Name = "txtLinkTiki";
            this.txtLinkTiki.Size = new System.Drawing.Size(1610, 67);
            this.txtLinkTiki.TabIndex = 0;
            this.txtLinkTiki.Text = "";
            // 
            // ImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1768, 1079);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImportData";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenCsv;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkCleanDb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtLinkTiki;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMsku;
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.TextBox txtSyncCode;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnNewId;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMinimumPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtLinkLazada;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox txtLinkSendo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox txtLinkShopee;
        private System.Windows.Forms.Label label15;
    }
}