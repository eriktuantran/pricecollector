using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            txtLinkTiki.DetectUrls = false;
            txtLinkLazada.DetectUrls = false;
            txtLinkShopee.DetectUrls = false;
            txtLinkSendo.DetectUrls = false;

            this.Height = 600;
            this.Width = 900;
            btnOK.Location = new Point(450-50, 520);
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
                        MessageBox.Show("Cannot connect to database. Contact administrator", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void clearTheFields(bool isEmptyId = false)
        {
            if(isEmptyId)
            {
                txtId.Text = "";
            }
            txtSyncCode.Text = cmbGroup.Text = txtCode.Text = txtSku.Text = txtMsku.Text = txtMinimumPrice.Text = txtLinkTiki.Text = txtLinkLazada.Text = "";
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
                string output = "insert into `product` (id,product_sync_code,product_group,product_code,sku,msku,active,minimum_price,link_tiki,link_lazada,link_shopee,link_sendo,other_seller_tiki, other_seller_lazada, other_seller_shopee, other_seller_sendo) values (";
                output += "'" + this.rowData["id"] + "', ";
                output += "'" + this.rowData["product_sync_code"] + "', ";
                output += "'" + this.rowData["product_group"] + "', ";
                output += "'" + this.rowData["product_code"] + "', ";
                output += "'" + this.rowData["sku"] + "', ";
                output += "'" + this.rowData["msku"] + "', ";
                output += "'" + this.rowData["active"] + "', ";
                output += "'" + this.rowData["minimum_price"] + "', ";
                output += "'" + this.rowData["link_tiki"] + "', ";
                output += "'" + this.rowData["link_lazada"] + "', ";
                output += "'" + this.rowData["link_shopee"] + "', ";
                output += "'" + this.rowData["link_sendo"] + "', ";
                output += "'" + "Other Tiki" + "', ";
                output += "'" + "Other Lazada" + "', ";
                output += "'" + "Other Shopee" + "', ";
                output += "'" + "Other Sendo" + "' ";
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
                output += "minimum_price='" + this.rowData["minimum_price"] + "', ";
                output += "link_tiki='" + this.rowData["link_tiki"] + "', ";
                output += "link_lazada='" + this.rowData["link_lazada"] + "', ";
                output += "link_shopee='" + this.rowData["link_shopee"] + "', ";
                output += "link_sendo='" + this.rowData["link_sendo"] + "' ";
                output += "where id='" + this.rowData["id"] + "';";

                return output;
            }
        }

        List<RowDataToImport> listRowToImport = new List<RowDataToImport>();

        void ReadCsv(string filePath)
        {
            // Empty the list to be get new value;
            listRowToImport.Clear();

            try
            {
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
                        MessageBox.Show("The format of CSV file is not correct!\nSeems the column name is not presence!", "File has wrong format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string[] headers = csv.GetFieldHeaders();
                    while (csv.ReadNextRecord())
                    {
                        RowDataToImport row = new RowDataToImport();

                        for (int i = 0; i < fieldCount; i++)
                        {
                            //Console.Write(string.Format("{0} = {1};", headers[i], csv[i] == null ? "MISSING" : csv[i]));

                            string cellValue = "";
                            if(csv[i] != null)
                            {
                                cellValue = csv[i].ToString();
                            }

                            // Parse the price to integer, it could be: 100,000,000
                            if(headers[i] == "minimum_price")
                            {
                                Regex digitsOnly = new Regex(@"[^\d]");
                                cellValue = digitsOnly.Replace(cellValue, "");
                                Console.WriteLine("Cell {0}", cellValue);
                                try
                                {
                                    Int32.Parse(cellValue);
                                }
                                catch
                                {
                                    cellValue = "0";
                                }
                            }
                            
                            row.rowData[headers[i]] = cellValue;
                            
                        }

                        listRowToImport.Add(row);
                    }
                }
            }
            catch
            {
                MessageBox.Show("CSV file is being opened by another appication!\nPlease close it and import again!", "CSV file busy...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                MessageBox.Show("Import Done!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            row.rowData["minimum_price"] = txtMinimumPrice.Text.Trim();
            row.rowData["link_tiki"] = txtLinkTiki.Text.Trim();
            row.rowData["link_lazada"] = txtLinkLazada.Text.Trim();
            row.rowData["link_shopee"] = txtLinkShopee.Text.Trim();
            row.rowData["link_sendo"] = txtLinkSendo.Text.Trim();

            foreach (var item in row.rowData)
            {
                if(item.Key == "sku" || item.Key == "link_lazada" || item.Key == "link_shopee" || item.Key == "link_sendo")
                {
                    continue; //Allow empty
                }

                if (item.Value == "")
                {
                    MessageBox.Show("Please fill in the: "+item.Key, "Some fields are missing...", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Finished!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnDelete.Enabled = true;
                this.CloseConnection();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (this.OpenConnection())
            {
                MySqlDataReader reader = null;
                string selectCmd = "select product_sync_code,product_group,product_code,sku,msku,active,minimum_price," +
                    "link_tiki,link_lazada,link_shopee,link_sendo " +
                    "from product where id='" + txtId.Text.Trim()+"';";
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
                            txtMinimumPrice.Text = reader.GetString(6).ToString();
                            txtLinkTiki.Text = reader.GetString(7).ToString();
                            txtLinkLazada.Text = reader.GetString(8).ToString();
                            txtLinkShopee.Text = reader.GetString(9).ToString();
                            txtLinkSendo.Text = reader.GetString(10).ToString();
                            btnAddProduct.Text = "Update";
                            btnDelete.Enabled = true;
                        }
                        catch
                        {
                            clearTheFields();
                        }
                    }
                }
                else
                {
                    clearTheFields();
                    chkActive.Checked = true;
                    btnAddProduct.Text = "Add";
                    btnDelete.Enabled = false;
                }

                this.CloseConnection();
            }
            else
            {
                clearTheFields();
                Console.WriteLine("error");
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
                    MessageBox.Show("Product id not exist in database: "+ productId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd.CommandText = "delete from product where id='" + productId + "';";
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();

                    clearTheFields(true);
                    chkActive.Checked = true;
                    MessageBox.Show("Deleted!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
