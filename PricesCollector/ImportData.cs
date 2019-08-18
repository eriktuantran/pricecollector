﻿using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PricesCollector
{

    public partial class ImportData : Form
    {
        private MySqlConnection connection;

        public ImportData(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            btnDelete.Enabled = false;
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            ReadCsv(filePath);
        }


        class RowDataToImport
        {
            public Dictionary<string, string> rowData = new Dictionary<string, string>();

            public string buildInsertString()
            {
                string output = "insert into `product` (id,product_sync_code,product_group,product_code,sku,msku,active,other_seller,link) values (";
                output += "'" + this.rowData["id"] + "', ";
                output += "'" + this.rowData["product_sync_code"] + "', ";
                output += "'" + this.rowData["product_group"] + "', ";
                output += "'" + this.rowData["product_code"] + "', ";
                output += "'" + this.rowData["sku"] + "', ";
                output += "'" + this.rowData["msku"] + "', ";
                output += "'" + this.rowData["active"] + "', ";
                output += "'" + "empty" + "', ";
                output += "'" + this.rowData["link"] + "'";
                output += ");";

                return output;
            }

            public string buildUpdateString()
            {
                string output = "update `product` set ";
                output += "product_sync_code='" + this.rowData["product_sync_code"] + "', ";
                output += "product_group='" + this.rowData["product_group"] + "', ";
                output += "product_code='" + this.rowData["product_code"] + "', ";
                output += "sku='" + this.rowData["sku"] + "', ";
                output += "msku='" + this.rowData["msku"] + "', ";
                output += "active='" + this.rowData["active"] + "', ";
                output += "link='" + this.rowData["link"] + "' ";
                output += "where id='" + this.rowData["id"] + "';";

                return output;
            }
        }

        List<RowDataToImport> listRowToImport = new List<RowDataToImport>();

        void ReadCsv(string filePath)
        {
            // Empty the list to be get new value;
            listRowToImport.Clear();

            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(
                    new StreamReader(filePath), true))
            {
                // missing fields will not throw an exception,
                // but will instead be treated as if there was a null value
                //csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                // to replace by "" instead, then use the following action:
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                int fieldCount = 0;
                try
                {
                    fieldCount = csv.FieldCount;
                }
                catch
                {
                    MessageBox.Show("The format of CSV file is not correct!\nSeems the column name is not presence!");
                    return;
                }
                    
                string[] headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord())
                {
                    RowDataToImport row = new RowDataToImport();

                    for (int i = 0; i < fieldCount; i++)
                    {
                        //Console.Write(string.Format("{0} = {1};", headers[i], csv[i] == null ? "MISSING" : csv[i]));

                        row.rowData[headers[i]] = csv[i] == null ? "MISSING" : csv[i];
                    }

                    listRowToImport.Add(row);
                }
            }
            Console.WriteLine("CSV DONE");

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

                if (chkCleanDb.Checked)
                {
                    cmd.CommandText = "delete from product;";
                    cmd.ExecuteNonQuery();
                    foreach (var row in listRowToImport)
                    {
                        cmd.CommandText = row.buildInsertString();
                        Console.WriteLine(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    foreach (var row in listRowToImport)
                    {
                        string productId = row.rowData["id"];
                        cmd.CommandText = "select Count(*) from product where id='" + productId + "';";
                        Console.WriteLine(cmd.ExecuteScalar());

                        int count = Int32.Parse( cmd.ExecuteScalar().ToString() );
                        if (count == 0)
                        {
                            Console.WriteLine("NOT overwrite NOT HasRows --> insert");
                            cmd.CommandText = row.buildInsertString();
                        }
                        else
                        {
                            Console.WriteLine("NOT overwrite HasRows --> update");
                            cmd.CommandText = row.buildUpdateString();
                        }
                        Console.WriteLine(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }

                }
                this.CloseConnection();

                Console.WriteLine("SQL DONE");
                MessageBox.Show("Import Done!");
            }
            

            //using (CachedCsvReader csv = new CachedCsvReader(new StreamReader(filePath), true))
            //{
            //    // Field headers will automatically be used as column names

            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnNewId_Click(object sender, EventArgs e)
        {
            txtId.Text = generateNewKeyForDb();
        }

        private string generateNewKeyForDb()
        {
            if (this.OpenConnection())
            {
                MySqlDataReader reader = null;
                string selectCmd = "select id from product;";

                MySqlCommand command = new MySqlCommand(selectCmd, connection);
                reader = command.ExecuteReader();

                int max = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string id = reader.GetString(0);
                            int idValue = Int32.Parse(id);
                            if (max < idValue) max = idValue;
                        }
                        catch { }
                    }
                }
                this.CloseConnection();


                return (max + 1).ToString();
            }
            return "";
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            RowDataToImport row = new RowDataToImport();
            row.rowData["id"] = txtId.Text.Trim();
            row.rowData["product_group"] = cmbGroup.Text.Trim();
            row.rowData["product_sync_code"] = txtSyncCode.Text.Trim();
            row.rowData["product_code"] = txtCode.Text.Trim();
            row.rowData["sku"] = txtSku.Text.Trim();
            row.rowData["msku"] = txtMsku.Text.Trim();
            row.rowData["active"] = chkActive.Checked?"1":"0";
            row.rowData["link"] = txtLink.Text.Trim();

            foreach(var item in row.rowData)
            {
                if (item.Value == "")
                {
                    MessageBox.Show("Please fill in the: "+item.Key);
                    return;
                }
            }

            if (this.OpenConnection())
            {
                string productId = row.rowData["id"];

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select Count(*) from product where id='" + productId + "';";

                int count = Int32.Parse(cmd.ExecuteScalar().ToString());
                if (count == 0) // not exist
                {
                    cmd.CommandText = row.buildInsertString();
                }
                else
                {
                    cmd.CommandText = row.buildUpdateString();
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Finished!");
                btnDelete.Enabled = true;
                this.CloseConnection();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (this.OpenConnection())
            {
                MySqlDataReader reader = null;
                string selectCmd = "select product_sync_code,product_group,product_code,sku,msku,active,link from product where id='"+txtId.Text.Trim()+"';";

                MySqlCommand command = new MySqlCommand(selectCmd, connection);
                reader = command.ExecuteReader();
                

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            txtSyncCode.Text = reader.GetString(0).ToString();
                            cmbGroup.Text = reader.GetString(1).ToString();
                            txtCode.Text = reader.GetString(2).ToString();
                            txtSku.Text = reader.GetString(3).ToString();
                            txtMsku.Text = reader.GetString(4).ToString();
                            chkActive.Checked = reader.GetString(5).ToString().ToLower()=="true"?true:false;
                            txtLink.Text = reader.GetString(6).ToString();
                            btnAddProduct.Text = "Update";
                            btnDelete.Enabled = true;
                        }
                        catch { }
                    }
                }
                else
                {
                    txtSyncCode.Text = cmbGroup.Text = txtCode.Text = txtSku.Text = txtMsku.Text = txtLink.Text = "";
                    chkActive.Checked = true;
                    btnAddProduct.Text = "Add";
                    btnDelete.Enabled = false;
                }

                this.CloseConnection();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection())
            {
                string productId = txtId.Text.Trim();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select Count(*) from product where id='" + productId + "';";
                Console.WriteLine(cmd.ExecuteScalar());

                int count = Int32.Parse(cmd.ExecuteScalar().ToString());
                if (count == 0) // not exist
                {
                    this.CloseConnection();
                    MessageBox.Show("Product id not exist in database: "+ productId);
                }
                else
                {
                    cmd.CommandText = "delete from product where id='" + productId + "';";
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();

                    txtId.Text = txtSyncCode.Text = cmbGroup.Text = txtCode.Text = txtSku.Text = txtMsku.Text = txtLink.Text = "";
                    chkActive.Checked = true;
                    MessageBox.Show("Deleted!");
                }
            }
        }
    }
}
