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
                        string productId = row.rowData["id"];

                        cmd.CommandText = "insert into `product` (id,product_sync_code,product_group,product_code,sku,msku,active,other_seller,link) values (";
                        cmd.CommandText += "'" + productId + "', ";
                        cmd.CommandText += "'" + row.rowData["product_sync_code"] + "', ";
                        cmd.CommandText += "'" + row.rowData["product_group"] + "', ";
                        cmd.CommandText += "'" + row.rowData["product_code"] + "', ";
                        cmd.CommandText += "'" + row.rowData["sku"] + "', ";
                        cmd.CommandText += "'" + row.rowData["msku"] + "', ";
                        cmd.CommandText += "'" + row.rowData["active"] + "', ";
                        cmd.CommandText += "'" + "empty" + "', ";
                        cmd.CommandText += "'" + row.rowData["link"] + "'";
                        cmd.CommandText += ");";
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
                            cmd.CommandText = "insert into `product` (id,product_sync_code,product_group,product_code,sku,msku,active,other_seller,link) values (";
                            cmd.CommandText += "'" + productId + "', ";
                            cmd.CommandText += "'" + row.rowData["product_sync_code"] + "', ";
                            cmd.CommandText += "'" + row.rowData["product_group"] + "', ";
                            cmd.CommandText += "'" + row.rowData["product_code"] + "', ";
                            cmd.CommandText += "'" + row.rowData["sku"] + "', ";
                            cmd.CommandText += "'" + row.rowData["msku"] + "', ";
                            cmd.CommandText += "'" + row.rowData["active"] + "', ";
                            cmd.CommandText += "'" + "empty" + "', ";
                            cmd.CommandText += "'" + row.rowData["link"] + "'";
                            cmd.CommandText += ");";
                        }
                        else
                        {
                            Console.WriteLine("NOT overwrite HasRows --> update");
                            cmd.CommandText = "update `product` set ";
                            cmd.CommandText += "product_sync_code='" + row.rowData["product_sync_code"] + "', ";
                            cmd.CommandText += "product_group='" + row.rowData["product_group"] + "', ";
                            cmd.CommandText += "product_code='" + row.rowData["product_code"] + "', ";
                            cmd.CommandText += "sku='" + row.rowData["sku"] + "', ";
                            cmd.CommandText += "msku='" + row.rowData["msku"] + "', ";
                            cmd.CommandText += "active='" + row.rowData["active"] + "', ";
                            cmd.CommandText += "link='" + row.rowData["link"] + "' ";
                            cmd.CommandText += "where id='"+ productId + "';";
                            
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
    }
}
