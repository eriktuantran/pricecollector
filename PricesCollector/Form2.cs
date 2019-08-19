using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace PricesCollector
{
    using MyDictionary = System.Collections.Generic.Dictionary<string, ProductData>;
    using ConfigDictionary = System.Collections.Generic.Dictionary<string, string>;

    public partial class Form2 : Form
    {
        private string connectionString = "";//"server=127.0.0.1;user id=root;password=3V5wn0Kv9RRc8gQA;persistsecurityinfo=True;database=pricecollector";
        private int timeoutUpdateDB = 0;
        private bool globalRunningState = false;

        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;

        private MyDictionary myDict = new MyDictionary();

        private bool isFetching
        {
            get
            {
                return backgroundWorkerForFetchingList.Count != 0;
            }
        }

        public Form2()
        {
            InitializeComponent();

            //Datagridview Width/Height not overflow
            dataGridView1.Width = this.Width - dataGridView1.Location.X - 30;
            dataGridView1.Height = this.Height - dataGridView1.Location.Y - 50;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form2_Load");

            // Read config from file
            Configuration config = new Configuration();
            ConfigDictionary dictRead = config.loadDictionaryFromFile();

            // Update camera URL, SQL connection string,...
            populateFormIntialValue(dictRead);

            // Config file not exist
            if (connectionString == "" || timeoutUpdateDB == 0)
            {
                getValueFromSettingForm();
            }

            connection = new MySqlConnection(connectionString);

            refreshDatagridviewValue();
            timerUpdateDb.Enabled = true;
            timerUpdateDb.Interval = 10 * 1000;
            progressBarUpdateDb.Value = 0;
            startProgressBarUpdateDb(10);

            //Enable the global flag
            globalRunningState = true;
        }

        void populateFormIntialValue(ConfigDictionary dict)
        {
            if (dict.ContainsKey("timeout") && dict["timeout"] != "")
            {
                timeoutUpdateDB = Int32.Parse(dict["timeout"]);
            }

            if (dict.ContainsKey("connectstring") && dict["connectstring"] != "")
            {
                connectionString = dict["connectstring"];
            }
        }
        void getValueFromSettingForm()
        {
            var appSetting = new AppSetting();
            appSetting.ShowDialog();

            //Collect Data
            if (appSetting.isOKButtonClicked)
            {
                this.connectionString = appSetting.connectionString;
                this.timeoutUpdateDB = appSetting.timeout;
                connection = new MySqlConnection(connectionString);
                Console.WriteLine("Setting done");
            }
            else
            {
                Console.WriteLine("Setting Terminated");
            }
        }

        private int columnNameToIndex(string columnName)
        {
            var dataGridViewColumn = dataGridView1.Columns[columnName];
            if (dataGridViewColumn != null)
            {
                return dataGridView1.Columns.IndexOf(dataGridViewColumn);
            }
            else
            {
                Console.WriteLine("### Column: {0} does not exist", columnName);
                return -1;
            }
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
                        MessageBox.Show("Cannot connect to database. Contact administrator", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void updateDB(string id, string fieldName, string value)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "update product set "+ fieldName + "='"+ value + "' where id='"+id+"';";
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                //Console.WriteLine("Header Line, ignore");
                return;
            }

            var productIdCell = dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("id")];

            if (productIdCell.Value == null)
            {
                Console.WriteLine("New line");
                return;
            }

            string productId = productIdCell.Value.ToString();

            if (e.ColumnIndex == columnNameToIndex("link"))
            {
                Console.WriteLine("ID={0} Row={1} Col={2}", productId, e.RowIndex, e.ColumnIndex);
                updateDB(productId, "link", dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("link")].Value.ToString());
            }
            else if(e.ColumnIndex == columnNameToIndex("active"))
            {
                DataGridViewCheckBoxCell chkchecking = dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("active")] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chkchecking.Value) == true)
                {
                    updateDB(productId, "active", "1");
                }
                else
                {
                    updateDB(productId, "active", "0");
                }
            }
            else if(e.ColumnIndex == columnNameToIndex("minimum_price"))
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("minimum_price")];

                if (cell.Value == null)
                {
                    cell.Value = "0";
                    return;
                }

                string newMinimumPrice = dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("minimum_price")].Value.ToString();
                Console.WriteLine("minimum_price changed: {0} _ {1}", lastMinimumPrice, newMinimumPrice);

                int newMinimumPriceInt = Int32.Parse(newMinimumPrice);
                int lastMinimumPriceInt = Int32.Parse(lastMinimumPrice);

                int limited = lastMinimumPriceInt - lastMinimumPriceInt * 20 / 100;

                if (newMinimumPriceInt < limited)
                {
                    DialogResult res = MessageBox.Show("Are you sure\n\nLast: "+ lastMinimumPrice+"\nNew:  "+ newMinimumPrice + "\nDifference > 20 %", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        updateDB(productId, "minimum_price", newMinimumPrice);
                    }
                    if (res == DialogResult.Cancel)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("minimum_price")].Value = lastMinimumPrice;
                    }
                }
                else
                {
                    updateDB(productId, "minimum_price", newMinimumPrice);
                }
            }
        }

        private string lastMinimumPrice = "";
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == columnNameToIndex("minimum_price"))
            {
                var minimumPriceCell = dataGridView1.Rows[e.RowIndex].Cells[columnNameToIndex("minimum_price")];

                if (minimumPriceCell.Value == null)
                {
                    Console.WriteLine("New line");
                    return;
                }

                lastMinimumPrice = minimumPriceCell.Value.ToString();
                Console.WriteLine("Begin {0}", lastMinimumPrice);
            }
            else
            {
                lastMinimumPrice = "";
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("End");
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            syncDatagridviewToDataSource(sender, e);
        }

        private void syncDatagridviewToDataSource(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");


            //DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();

            //if (changes != null)
            //{
            //    MySqlCommandBuilder mcb = new MySqlCommandBuilder(mySqlDataAdapter);
            //    mySqlDataAdapter.UpdateCommand = mcb.GetUpdateCommand();
            //    mySqlDataAdapter.Update(changes);
            //    ((DataTable)dataGridView1.DataSource).AcceptChanges();
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == columnNameToIndex("link"))
            {
                try
                {
                    string url = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (url.Trim() != "" && Form.ModifierKeys == Keys.Control)
                    {
                        Process.Start(url);
                    }
                }
                catch { }
            }
        }



        private void fetchDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isFetching)
            {
                Console.WriteLine("Fetching threads are already running");
                MessageBox.Show("It is busy, please wait!", "Busy...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Stop the timer update DB
            timerUpdateDb.Enabled = false;
            stopProgressBarUpdateDb();

            polulateLinkToDictionary();
            refreshDBPricesMultiThreadAsync();
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }

        private void timerUpdateDB_Tick(object sender, EventArgs e)
        {
            //Stop the timer update DB
            timerUpdateDb.Enabled = false;
            stopProgressBarUpdateDb();

            polulateLinkToDictionary();
            refreshDBPricesMultiThreadAsync();
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }

        private void polulateLinkToDictionary()
        {
            myDict.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlDataReader reader = null;
                string selectCmd = "select id,link,active from product;";

                MySqlCommand command = new MySqlCommand(selectCmd, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string id = reader.GetString(0);
                            string link = reader.GetString(1);
                            string active = reader.GetString(2).ToLower();
                            ProductData dat = new ProductData();
                            dat.link = link;
                            dat.productId = Int32.Parse(id);
                            dat.isActive = active=="true"? true:false;
                            myDict[id] = dat;
                        }
                        catch { }
                    }
                    CloseConnection();
                }
                else
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Fetching Tiki multithread
        /// </summary>
        /// 

        List<BackgroundWorker> backgroundWorkerForFetchingList = new List<BackgroundWorker>();

        private void refreshDBPricesMultiThreadAsync()
        {
            foreach (var item in myDict)
            {
                ProductData data = item.Value;
                if(data.isActive == false)
                {
                    //This item is not active
                    Console.WriteLine("Skip this product, it is not active");
                    continue;
                }

                startProgressFetchingByMultiThread(data);
            }
            progressBarFetching.Maximum = backgroundWorkerForFetchingList.Count;
        }

        private void startProgressFetchingByMultiThread(ProductData data)
        {
            try
            {
                BackgroundWorker bgw = CreateBackgroundWorkerForFetching();
                backgroundWorkerForFetchingList.Add(bgw);
                bgw.RunWorkerAsync(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private BackgroundWorker CreateBackgroundWorkerForFetching()
        {
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerForFetching_DoWork;
            bw.RunWorkerCompleted += bgWorkerForFetching_RunWorkerCompleted;
            return bw;
        }

        private void bgWorkerForFetching_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;
            ProductData data = (ProductData)e.Argument;

            data.populateDataFromTikiLinkVersion2();

            //Do your work
            e.Result = data;
        }

        private void bgWorkerForFetching_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            backgroundWorkerForFetchingList.Remove(bgw);
            bgw.Dispose();

            if (globalRunningState == false)
            {
                return; //button stop clicked
            }

            if (progressBarFetching.Value < progressBarFetching.Maximum)
            {
                progressBarFetching.Value++;
            }

            if (backgroundWorkerForFetchingList.Count == 0) //All threads have finished the job
            {
                updateDbWhenFetchingDone(); //Store to DB
                refreshDatagridviewValue(); //Fetch from DB
                progressBarFetching.Value = 0;

                //Restart the timer update DB
                timerUpdateDb.Interval = timeoutUpdateDB * 1000;
                timerUpdateDb.Enabled = true;
                startProgressBarUpdateDb(timeoutUpdateDB);
            }

            if (e.Error != null)
            {
                Console.WriteLine("ERROR: " + e.Error.ToString());
            }
        }

        private void updateDbWhenFetchingDone()
        {
            if (this.OpenConnection() == true)
            {
                foreach (var item in myDict)
                {
                    ProductData data = item.Value;
                    List<Product> otherSellerList = data.SortedList();
                    string otherSellerStringToDB = "";
                    foreach (var product in otherSellerList)
                    {
                        otherSellerStringToDB += product.price + "_" + product.name + "\n";
                    }
                    otherSellerStringToDB = otherSellerStringToDB.Trim();

                    Console.WriteLine("{0} _ {1} _ {2} _ {3} _ {4}", item.Key, data.link, data.sellerName, data.currentPrice.ToString(), data.lowestPrice.ToString());

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "update product set ";
                    cmd.CommandText += "seller_name ='" + data.sellerName + "',";
                    cmd.CommandText += "product_name ='" + data.productName + "',";
                    cmd.CommandText += "sku ='" + data.sku + "',";
                    cmd.CommandText += "current_price ='" + data.currentPrice.ToString() + "',";
                    cmd.CommandText += "discount_price ='" + data.discountPrice.ToString() + "',";
                    cmd.CommandText += "lowest_price ='" + data.lowestPrice.ToString() + "',";
                    cmd.CommandText += "other_seller ='" + otherSellerStringToDB + "' ";
                    cmd.CommandText += "where id='" + item.Key + "';";
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }

        private void refreshDatagridviewValue()
        {
            dataGridView1.Rows.Clear();
            if (this.OpenConnection() == true)
            {
                string columnsToDisplay = "id,seller_name,product_group,product_name,sku,msku,current_price,minimum_price,lowest_price,discount_price,other_seller,active,link";
                mySqlDataAdapter = new MySqlDataAdapter("select "+ columnsToDisplay + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();
                
                DataTable tableFromDb = DS.Tables[0];
                List<MyRow> rowList = new List<MyRow>();

                foreach (DataRow _row in tableFromDb.Rows)
                {
                    int currentPrice = Int32.Parse(_row.ItemArray[columnNameToIndex("current_price")].ToString());
                    int lowestPrice = Int32.Parse(_row.ItemArray[columnNameToIndex("lowest_price")].ToString());
                    //Console.WriteLine("{0}==={1}",currentPrice, lowestPrice);

                    MyRow myRow = new MyRow();
                    myRow.row = _row;
                    if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0)
                    {
                        myRow.isLowest = false;
                    }
                    else
                    {
                        myRow.isLowest = true;
                    }
                    rowList.Add(myRow);
                }

                List<MyRow> sortedRowList = rowList.OrderBy(o => o.isLowest).ToList();


                int rowIndex = 0;
                foreach(MyRow row in sortedRowList)
                //foreach (DataRow _row in tableFromDb.Rows)
                {
                    var _row = row.row;

                    dataGridView1.Rows.Add(_row.ItemArray);

                    //DataGridViewComboBoxCell cellCombo = new DataGridViewComboBoxCell();
                    //string str = _row["other_seller"].ToString();
                    //List<string> s = str.Split('\n').ToList();
                    //cellCombo.DataSource = s;
                    //dataGridView1.Rows[rowIndex].Cells["other_seller"].Value = str;

                    //Color
                    int currentPrice = Int32.Parse( dataGridView1.Rows[rowIndex].Cells["current_price"].Value.ToString());
                    int lowestPrice = Int32.Parse(dataGridView1.Rows[rowIndex].Cells["lowest_price"].Value.ToString());

                    if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0 )
                    {
                        dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Green;
                    }

                    //Link color
                    var obj = (DataGridViewTextBoxCell)dataGridView1.Rows[rowIndex].Cells["link"];
                    obj.Style.ForeColor = Color.Blue;

                    rowIndex++;
                }
                

            }
        }

        /// <summary>
        /// Timeout value changed
        /// </summary>
        /// 
        private void timeoutUpdateDBChangedFromSettingFormEvent()
        {
            if(isFetching)
            {
                Console.WriteLine("Fetching threads are already running");
                return;
            }

            timerUpdateDb.Enabled = false;
            timerUpdateDb.Interval = timeoutUpdateDB * 1000;
            timerUpdateDb.Enabled = true;

            startProgressBarUpdateDb(timeoutUpdateDB);
        }


        /// <summary>
        /// Progress bar update DB
        /// </summary>
        
        BackgroundWorker _backgroundWorkerForProgressBar;

        private BackgroundWorker CreateBackgroundWorker()
        {
            Console.WriteLine("### CreateBackgroundWorker");
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerProgressBarUpdateDb_DoWork;
            bw.ProgressChanged += bgWorkerProgressBarUpdateDb_ProgressChanged;
            bw.RunWorkerCompleted += bgWorkerProgressBarUpdateDb_RunWorkerCompleted;
            return bw;
        }

        private void startProgressBarUpdateDb(int timerInSecond)
        {
            try
            {
                //Cancel whatever it is you're doing!
                if (_backgroundWorkerForProgressBar != null)
                {
                    _backgroundWorkerForProgressBar.CancelAsync();
                }

                _backgroundWorkerForProgressBar = CreateBackgroundWorker();

                //And start doing this immediately!
                _backgroundWorkerForProgressBar.RunWorkerAsync(timerInSecond);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void stopProgressBarUpdateDb()
        {
            try
            {
                if (_backgroundWorkerForProgressBar != null)
                {
                    _backgroundWorkerForProgressBar.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bgWorkerProgressBarUpdateDb_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int value = (int)e.Argument;

            for (int i = 1; i <= 100; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep((value-1)*10);
                    worker.ReportProgress(i);
                }
            }
        }

        private void bgWorkerProgressBarUpdateDb_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(globalRunningState == false)
            {
                return;// btn stop clicked
            }

            progressBarUpdateDb.Value = Int32.Parse(e.ProgressPercentage.ToString());
        }

        private void bgWorkerProgressBarUpdateDb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            bgw.Dispose();
        }



        /// <summary>
        /// Export EXCEL
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        /// 

        private DataTable getDataToExport(string columns)
        {
            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select " + columns + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();

                return DS.Tables[0];
            }
            MessageBox.Show("Pleasy try to export again!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return new DataTable();
        }

        void bgWorkerExportExcelFile_DoWork(string excelFileName)
        {
            string columnsToExport = "id, seller_name, product_group, product_name, sku, msku, active, current_price, minimum_price, lowest_price, discount_price, other_seller, link";
            DataTable data;

            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select " + columnsToExport + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();

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
                worksheet.Cells[1, i] = Utilities.getColName(columnId); // dataGridView1.Columns[i - 1].HeaderText;  
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < data.Rows.Count; i++) // dataGridView1.Rows.Count - 1
            {
                for (int j = 0; j < data.Columns.Count; j++) //dataGridView1.Columns.Count
                {
                    try
                    {
                        worksheet.Cells[i + 2, j + 1] = data.Rows[i].ItemArray[j].ToString(); //dataGridView1.Rows[i].Cells[j].Value.ToString();  
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

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("Pleasy try to export again!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Pleasy try to export again!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bgWorkerExportExcelFile_DoWork(excelFileName);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prevtimeoutUpdateDB = this.timeoutUpdateDB;
            getValueFromSettingForm();
            if (this.timeoutUpdateDB != prevtimeoutUpdateDB)
            {
                timeoutUpdateDBChangedFromSettingFormEvent();
            }
        }


        /// <summary>
        /// Button STOP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnStop_Click(object sender, EventArgs e)
        {
            if(isFetching)
            {
                MessageBox.Show("It is fetching, please wait!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (globalRunningState == true)
            {
                globalRunningState = false;
                btnStop.Text = "Start";
                timerUpdateDb.Stop();
                stopProgressBarUpdateDb();
                progressBarFetching.Value = 0;
                progressBarUpdateDb.Value = 0;
            }
            else
            {
                globalRunningState = true;
                btnStop.Text = "Stop";
                timerUpdateDb.Interval = timeoutUpdateDB * 1000;
                timerUpdateDb.Start();
                startProgressBarUpdateDb(timeoutUpdateDB);
            }

        }

        private void importCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appImportData = new ImportData(this.connection);
            appImportData.ShowDialog();
        }


    }
}
