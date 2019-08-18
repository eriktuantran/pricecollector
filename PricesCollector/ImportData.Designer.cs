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
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenCsv = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLink = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtSyncCode = new System.Windows.Forms.TextBox();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtMsku = new System.Windows.Forms.TextBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnNewId = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCleanDb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOpenCsv);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import from CSV file:";
            // 
            // chkCleanDb
            // 
            this.chkCleanDb.AutoSize = true;
            this.chkCleanDb.Location = new System.Drawing.Point(30, 85);
            this.chkCleanDb.Name = "chkCleanDb";
            this.chkCleanDb.Size = new System.Drawing.Size(225, 29);
            this.chkCleanDb.TabIndex = 2;
            this.chkCleanDb.Text = "Clean up database";
            this.chkCleanDb.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(870, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Column syntax:   id,product_sync_code,product_group,product_code,sku,msku,active," +
    "link";
            // 
            // btnOpenCsv
            // 
            this.btnOpenCsv.Location = new System.Drawing.Point(30, 138);
            this.btnOpenCsv.Name = "btnOpenCsv";
            this.btnOpenCsv.Size = new System.Drawing.Size(116, 50);
            this.btnOpenCsv.TabIndex = 0;
            this.btnOpenCsv.Text = "Import...";
            this.btnOpenCsv.UseVisualStyleBackColor = true;
            this.btnOpenCsv.Click += new System.EventHandler(this.btnOpenCsv_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(390, 602);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(162, 53);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
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
            this.groupBox2.Controls.Add(this.txtLink);
            this.groupBox2.Location = new System.Drawing.Point(12, 242);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 344);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add new product:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(106, 188);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(776, 67);
            this.txtLink.TabIndex = 0;
            this.txtLink.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Link";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(429, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Group";
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(548, 36);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(334, 33);
            this.cmbGroup.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(429, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Sync code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Code";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(28, 273);
            this.chkActive.Name = "chkActive";
            this.chkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkActive.Size = new System.Drawing.Size(103, 29);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(429, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "MSKU";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "SKU";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(106, 87);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(305, 31);
            this.txtCode.TabIndex = 5;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(106, 37);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(112, 31);
            this.txtId.TabIndex = 6;
            this.txtId.TextChanged += new System.EventHandler(this.txtId_TextChanged);
            // 
            // txtSyncCode
            // 
            this.txtSyncCode.Location = new System.Drawing.Point(548, 87);
            this.txtSyncCode.Name = "txtSyncCode";
            this.txtSyncCode.Size = new System.Drawing.Size(334, 31);
            this.txtSyncCode.TabIndex = 7;
            // 
            // txtSku
            // 
            this.txtSku.Location = new System.Drawing.Point(106, 137);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(305, 31);
            this.txtSku.TabIndex = 8;
            // 
            // txtMsku
            // 
            this.txtMsku.Location = new System.Drawing.Point(548, 137);
            this.txtMsku.Name = "txtMsku";
            this.txtMsku.Size = new System.Drawing.Size(334, 31);
            this.txtMsku.TabIndex = 9;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(163, 272);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(125, 53);
            this.btnAddProduct.TabIndex = 10;
            this.btnAddProduct.Text = "Add";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnNewId
            // 
            this.btnNewId.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewId.Location = new System.Drawing.Point(235, 32);
            this.btnNewId.Name = "btnNewId";
            this.btnNewId.Size = new System.Drawing.Size(90, 41);
            this.btnNewId.TabIndex = 11;
            this.btnNewId.Text = "New";
            this.btnNewId.UseVisualStyleBackColor = true;
            this.btnNewId.Click += new System.EventHandler(this.btnNewId_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(314, 273);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(125, 53);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 673);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkCleanDb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtLink;
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
    }
}