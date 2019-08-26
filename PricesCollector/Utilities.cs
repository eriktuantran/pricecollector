﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PricesCollector
{
    class MyRow
    {
        public bool isLowest = false;
        public DataRow row;
    };

    class Utilities
    {
        static public int colNameToIndex(string columnName, DataGridView dataGridView)
        {
            var dataGridViewColumn = dataGridView.Columns[columnName];
            if (dataGridViewColumn != null)
            {
                return dataGridView.Columns.IndexOf(dataGridViewColumn);
            }
            else
            {
                Console.WriteLine("### Column: {0} does not exist", columnName);
                return -1;
            }
        }

        static public string getColName(string key)
        {
            switch(key)
            {
                case "id":
                    return "Mã số";
                case "product_sync_code":
                    return "Mã đồng bộ";
                case "product_group":
                    return "Nhóm";
                case "seller_name":
                    return "Tên Seller";
                case "product_name":
                    return "Tên sản phẩm";
                case "product_code":
                    return "Mã sản phẩm";
                case "sku":
                case "msku":
                    return key.ToUpper();
                case "active":
                    return "Trạng thái";
                case "current_price":
                    return "Giá bán";
                case "minimum_price":
                    return "Giá bán thấp nhất";
                case "lowest_price":
                    return "Giá bán thấp nhất (từ nhà cung cấp khác)";
                case "discount_price":
                    return "Giảm giá";
                case "other_seller":
                    return "Nhà cung cấp khác";
                case "link_tiki":
                    return "Link Tiki";
                case "link_lazada":
                    return "Link Lazada";

                default: return key;
            }
        }

        //open connection to database
        static private bool OpenConnection(MySqlConnection connection)
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
        static private bool CloseConnection(MySqlConnection connection)
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

        static private void updateDB(string id, string fieldName, string value, MySqlConnection connection)
        {
            if (OpenConnection(connection) == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "update product set " + fieldName + "='" + value + "' where id='" + id + "';";
                cmd.ExecuteNonQuery();
                CloseConnection(connection);
            }
        }

        static public bool verifyClickEventIsApproriate(DataGridView mydataGridView, int rowIndex, out string productId)
        {
            productId = "";
            if (rowIndex < 0)
            {
                return false;
            }

            var productIdCell = mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("id", mydataGridView)];

            if (productIdCell.Value == null)
            {
                return false;
            }

            productId = productIdCell.Value.ToString();
            return true;
        }

        static public void openLinkInBrowser(DataGridView mydataGridView, int rowIndex)
        {
            try
            {
                string url = (string)mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("link_tiki", mydataGridView)].Value;
                if (url.Trim() != "" && Form.ModifierKeys == Keys.Control)
                {
                    Process.Start(url);
                }
            }
            catch { }
        }

        static public void updateLinkTikiCellValue(DataGridView mydataGridView, int rowIndex, string productId, MySqlConnection connection)
        {
            Console.WriteLine("ID={0} Row={1}", productId, rowIndex);
            updateDB(productId, "link_tiki", mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("link_tiki", mydataGridView)].Value.ToString(), connection);
        }

        static public void updateActiveCheckboxCellValue(DataGridView mydataGridView, int rowIndex, string productId, MySqlConnection connection)
        {
            DataGridViewCheckBoxCell chkchecking = mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("active", mydataGridView)] as DataGridViewCheckBoxCell;

            if (Convert.ToBoolean(chkchecking.Value) == true)
            {
                updateDB(productId, "active", "1", connection);
            }
            else
            {
                updateDB(productId, "active", "0", connection);
            }
        }

        static public void updateMinimumPriceCellValue(DataGridView mydataGridView, int rowIndex, string productId ,string lastMinimumPrice, MySqlConnection connection)
        {
            var cell = mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("minimum_price", mydataGridView)];

            if (cell.Value == null)
            {
                cell.Value = "0";
                return;
            }

            string newMinimumPrice = mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("minimum_price", mydataGridView)].Value.ToString();
            Console.WriteLine("minimum_price changed: {0} _ {1}", lastMinimumPrice, newMinimumPrice);
            try
            {
                int newMinimumPriceInt = Int32.Parse(newMinimumPrice);
                int lastMinimumPriceInt = Int32.Parse(lastMinimumPrice);

                int limited = lastMinimumPriceInt - lastMinimumPriceInt * 20 / 100;
                int percent = 100 - (newMinimumPriceInt * 100 / lastMinimumPriceInt);

                if (newMinimumPriceInt < limited)
                {
                    DialogResult res = MessageBox.Show("Are you sure?\n\nLast: " + lastMinimumPrice + "\nNew:  " + newMinimumPrice + "\nDifference: " + percent.ToString(),
                        "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        updateDB(productId, "minimum_price", newMinimumPrice, connection);
                    }
                    if (res == DialogResult.Cancel)
                    {
                        mydataGridView.Rows[rowIndex].Cells[Utilities.colNameToIndex("minimum_price", mydataGridView)].Value = lastMinimumPrice;
                    }
                }
                else
                {
                    updateDB(productId, "minimum_price", newMinimumPrice, connection);
                }
            }
            catch
            {
                DialogResult res = MessageBox.Show("Failed to update value, please double check!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void bgWorkerExportExcelFile_DoWork(MySqlConnection connection)
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.ToString("yyyy-MM-dd-HH-mm-ss");
            string excelFileName = "Tiki-Export-" + timeStamp + ".xls";

            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "Save Excel File";
            //saveFileDialog1.CheckFileExists = true;
            saveFileDlg.FileName = excelFileName;
            saveFileDlg.OverwritePrompt = false;
            saveFileDlg.CheckPathExists = true;
            saveFileDlg.DefaultExt = "xls";
            saveFileDlg.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            saveFileDlg.FilterIndex = 2;
            saveFileDlg.RestoreDirectory = true;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                excelFileName = saveFileDlg.FileName;
            }
            else
            {
                return;
            }

            if (connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Pleasy try to export again!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            // Fetch data from database to export
            string columnsToExport = "id, seller_name, product_group, product_name, sku, msku, active, current_price, minimum_price, lowest_price, discount_price, other_seller, link_tiki";
            DataTable data;

            if (OpenConnection(connection) == true)
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select " + columnsToExport + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                CloseConnection(connection);

                data = DS.Tables[0];
            }
            else
            {
                MessageBox.Show("Connection to DB failed", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            //app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < data.Columns.Count + 1; i++)
            {
                string columnId = data.Columns[i - 1].ColumnName;
                worksheet.Cells[1, i] = Utilities.getColName(columnId);
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    try
                    {
                        worksheet.Cells[i + 2, j + 1] = data.Rows[i].ItemArray[j].ToString();
                    }
                    catch { }
                }
            }

            try
            {
                // save the application  
                workbook.SaveAs(excelFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                MessageBox.Show("Export successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Failed to save!", "Failed to export!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Exit from the application  
            app.Quit();
        }


    }
}
