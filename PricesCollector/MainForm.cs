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
using System.Net;

namespace PricesCollector
{
    using MyDictionary = System.Collections.Generic.Dictionary<string, ProductData>;
    using ConfigDictionary = System.Collections.Generic.Dictionary<string, string>;

    enum Tab
    {
        Tiki = 0,
        OtherWebsite = 1
    }

    public partial class MainForm : Form
    {
        private string connectionString = "";//"server=127.0.0.1;user id=root;password=3V5wn0Kv9RRc8gQA;persistsecurityinfo=True;database=pricecollector";
        private int timeoutUpdateDB = 0;
        private bool globalRunningState = false;
        private bool isManualFetching = false;

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

        public MainForm()
        {
            InitializeComponent();
            createDatagridview1();
            createDatagridview2();
        }


        private void createDatagridview1()
        {
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.HeaderText = "ID";
            idColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(idColumn);

            dataGridView1.Columns.Add("seller_name", "Seller name");
            dataGridView1.Columns.Add("product_group", "Group");
            dataGridView1.Columns.Add("product_name", "Product name");
            dataGridView1.Columns.Add("sku", "SKU");
            dataGridView1.Columns.Add("msku", "MSKU");
            dataGridView1.Columns.Add("current_price", "Tiki price");
            dataGridView1.Columns.Add("minimum_price", "Minimum price");
            dataGridView1.Columns.Add("lowest_price", "Lowest price");
            dataGridView1.Columns.Add("discount_price", "Discount price");

            DataGridViewTextBoxColumn otherSellerColumn = new DataGridViewTextBoxColumn();
            otherSellerColumn.Name = "other_seller";
            otherSellerColumn.HeaderText = "Other Seller";
            otherSellerColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherSellerColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(otherSellerColumn);

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "active";
            checkColumn.HeaderText = "Active";
            checkColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(checkColumn);

            dataGridView1.Columns.Add("link_tiki", "Tiki link");


            //Datagridview Width/Height not overflow
            dataGridView1.Columns[Utilities.colNameToIndex("link_tiki", dataGridView1)].Width = 500;
            dataGridView1.Width = this.tabControl1.Width - 10;
            dataGridView1.Height = this.tabControl1.Height - 30;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void createDatagridview2()
        {
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.HeaderText = "ID";
            idColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns.Add(idColumn);

            dataGridView2.Columns.Add("seller_name", "Seller name");
            dataGridView2.Columns.Add("product_group", "Group");
            dataGridView2.Columns.Add("product_name", "Product name");
            dataGridView2.Columns.Add("sku", "SKU");
            dataGridView2.Columns.Add("msku", "MSKU");
            dataGridView2.Columns.Add("current_price", "Tiki price");
            dataGridView2.Columns.Add("minimum_price", "Minimum price");
            dataGridView2.Columns.Add("lowest_price", "Lowest price");
            dataGridView2.Columns.Add("discount_price", "Discount price");

            DataGridViewTextBoxColumn otherSellerColumn = new DataGridViewTextBoxColumn();
            otherSellerColumn.Name = "other_seller";
            otherSellerColumn.HeaderText = "Other seller";
            otherSellerColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherSellerColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns.Add(otherSellerColumn);

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "active";
            checkColumn.HeaderText = "Active";
            checkColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns.Add(checkColumn);

            dataGridView2.Columns.Add("link_tiki", "Tiki link");

            dataGridView2.Columns.Add("link_lazada", "Lazada links");

            //Datagridview Width/Height not overflow
            dataGridView2.Columns[Utilities.colNameToIndex("link_tiki", dataGridView2)].Width = 500;
            dataGridView2.Width = this.tabControl1.Width - 10;
            dataGridView2.Height = this.tabControl1.Height - 30;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm_Load");

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
            refreshDatagridviewValueDatagridview2();
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

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int prevtimeoutUpdateDB = this.timeoutUpdateDB;
            getValueFromSettingForm();
            if (this.timeoutUpdateDB != prevtimeoutUpdateDB)
            {
                timeoutUpdateDBChangedFromSettingFormEvent();
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

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mydataGridView = (DataGridView)sender;

            string productId = "";
            if (Utilities.verifyClickEventIsApproriate(mydataGridView, e.RowIndex, out productId) == false)
            {
                return;
            }

            if (e.ColumnIndex == Utilities.colNameToIndex("link_tiki", mydataGridView))
            {
                Utilities.updateLinkTikiCellValue(mydataGridView, e.RowIndex, productId, this.connection);
            }
            else if (e.ColumnIndex == Utilities.colNameToIndex("active", mydataGridView))
            {
                Utilities.updateActiveCheckboxCellValue(mydataGridView, e.RowIndex, productId, this.connection);
            }
            else if (e.ColumnIndex == Utilities.colNameToIndex("minimum_price", mydataGridView))
            {
                Utilities.updateMinimumPriceCellValue(mydataGridView, e.RowIndex, productId, lastMinimumPrice, this.connection);
            }
        }

        private string lastMinimumPrice = "";
        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView mydataGridView = (DataGridView)sender;

            string productId = "";
            if (Utilities.verifyClickEventIsApproriate(mydataGridView, e.RowIndex, out productId) == false)
            {
                return;
            }

            if (e.ColumnIndex == Utilities.colNameToIndex("minimum_price", mydataGridView))
            {
                var minimumPriceCell = mydataGridView.Rows[e.RowIndex].Cells[Utilities.colNameToIndex("minimum_price", mydataGridView)];

                if (minimumPriceCell.Value == null)
                {
                    Console.WriteLine("New line");
                    return;
                }

                lastMinimumPrice = minimumPriceCell.Value.ToString();
                Console.WriteLine("Begin edit {0} Grid {1}", lastMinimumPrice, mydataGridView.Name);
            }
            else
            {
                lastMinimumPrice = "";
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mydataGridView = (DataGridView)sender;
            Console.WriteLine("End edit {0}", mydataGridView.Name);

            string productId = "";
            if (Utilities.verifyClickEventIsApproriate(mydataGridView, e.RowIndex, out productId) == false)
            {
                return;
            }

            lastMinimumPrice = "";
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mydataGridView = (DataGridView)sender;
            Console.WriteLine("dataGridView_CellClick: {0}", mydataGridView.Name);

            string productId = "";
            if (Utilities.verifyClickEventIsApproriate(mydataGridView, e.RowIndex, out productId) == false)
            {
                return;
            }

            if (e.ColumnIndex == Utilities.colNameToIndex("link_tiki", mydataGridView))
            {
                Utilities.openLinkInBrowser(mydataGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == Utilities.colNameToIndex("active", mydataGridView))
            {
                Utilities.updateActiveCheckboxCellValue(mydataGridView, e.RowIndex, productId, this.connection);                
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

            Console.WriteLine("Manual fetching triggered");

            //Manual fetching 
            isManualFetching = true;

            //Stop the timer update DB
            timerUpdateDb.Enabled = false;
            stopProgressBarUpdateDb();

            polulateLinkToDictionary();
            refreshDBPricesMultiThreadAsync();
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }

        private void timerUpdateDB_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer update DB ticked");

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
                string selectCmd = "select id,link_tiki,active,link_lazada from product;";

                MySqlCommand command = new MySqlCommand(selectCmd, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string id = reader.GetString(0);
                            string link_tiki = reader.GetString(1);
                            string active = reader.GetString(2).ToLower();
                            string linkLazada = reader.GetString(3);

                            ProductData dat = new ProductData();
                            dat.linkTiki = link_tiki;
                            dat.productId = Int32.Parse(id);
                            dat.isActive = active=="true"? true:false;
                            dat.linkLazada = linkLazada;
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
                    //Console.WriteLine("Skip this product, it is not active");
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

            if (globalRunningState == false && isManualFetching == false)
            {
                return; //button stop clicked
            }

            if (progressBarFetching.Value < progressBarFetching.Maximum)
            {
                progressBarFetching.Value++;
            }

            if (backgroundWorkerForFetchingList.Count == 0) //All threads have finished the job
            {
                Console.WriteLine("All threads have finished the job");

                updateDbWhenFetchingDone(); //Store to DB
                refreshDatagridviewValue(); //Fetch from DB
                progressBarFetching.Value = 0;

                //Restart the timer update DB
                timerUpdateDb.Interval = timeoutUpdateDB * 1000;
                timerUpdateDb.Enabled = true;

                //Reset the manual fetching, it is done
                if (isManualFetching == true)
                {
                    isManualFetching = false;
                    if(globalRunningState == false)
                    {
                        return;
                    }
                }

                //Start timer update DB
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
                Console.WriteLine("updating Db When Fetching Done");
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

                    //Console.WriteLine("{0} _ {1} _ {2} _ {3} _ {4}", item.Key, data.link_tiki, data.sellerName, data.currentPrice.ToString(), data.lowestPrice.ToString());

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

        private void toolStripMenuRefreshView_Click(object sender, EventArgs e)
        {
            refreshDatagridviewValue();
        }

        private void refreshDatagridviewValueDatagridview2()
        {
            Console.WriteLine("Refresh view datagridview2");
            dataGridView2.Rows.Clear();

            if (this.OpenConnection() == true)
            {
                string columnsToDisplay = "id,seller_name,product_group,product_name,sku,msku,current_price,minimum_price,lowest_price,discount_price,other_seller,active,link_tiki,link_lazada";
                mySqlDataAdapter = new MySqlDataAdapter("select "+ columnsToDisplay + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();
                
                DataTable tableFromDb = DS.Tables[0];
                List<MyRow> rowList = new List<MyRow>();

                foreach (DataRow _row in tableFromDb.Rows)
                {
                    int currentPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("current_price", dataGridView2)].ToString());
                    int lowestPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price", dataGridView2)].ToString());
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

                    dataGridView2.Rows.Add(_row.ItemArray);

                    //DataGridViewComboBoxCell cellCombo = new DataGridViewComboBoxCell();
                    //string str = _row["other_seller"].ToString();
                    //List<string> s = str.Split('\n').ToList();
                    //cellCombo.DataSource = s;
                    //dataGridView2.Rows[rowIndex].Cells["other_seller"].Value = str;

                    //Color
                    int currentPrice = Int32.Parse( dataGridView2.Rows[rowIndex].Cells["current_price"].Value.ToString());
                    int lowestPrice = Int32.Parse(dataGridView2.Rows[rowIndex].Cells["lowest_price"].Value.ToString());

                    if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0 )
                    {
                        dataGridView2.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView2.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Green;
                    }

                    //Link color
                    var obj = (DataGridViewTextBoxCell)dataGridView2.Rows[rowIndex].Cells["link_tiki"];
                    obj.Style.ForeColor = Color.Blue;

                    rowIndex++;
                }
                

            }
        }


        private void refreshDatagridviewValue()
        {
            Console.WriteLine("Refresh view");
            dataGridView1.Rows.Clear();

            if (this.OpenConnection() == true)
            {
                string columnsToDisplay = "id,seller_name,product_group,product_name,sku,msku,current_price,minimum_price,lowest_price,discount_price,other_seller,active,link_tiki,link_lazada";
                mySqlDataAdapter = new MySqlDataAdapter("select " + columnsToDisplay + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();

                DataTable tableFromDb = DS.Tables[0];
                List<MyRow> rowList = new List<MyRow>();

                foreach (DataRow _row in tableFromDb.Rows)
                {
                    int currentPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("current_price", dataGridView1)].ToString());
                    int lowestPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price", dataGridView1)].ToString());
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
                foreach (MyRow row in sortedRowList)
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
                    int currentPrice = Int32.Parse(dataGridView1.Rows[rowIndex].Cells["current_price"].Value.ToString());
                    int lowestPrice = Int32.Parse(dataGridView1.Rows[rowIndex].Cells["lowest_price"].Value.ToString());

                    if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0)
                    {
                        dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Green;
                    }

                    //Link color
                    var obj = (DataGridViewTextBoxCell)dataGridView1.Rows[rowIndex].Cells["link_tiki"];
                    obj.Style.ForeColor = Color.Blue;

                    rowIndex++;
                }
            }




            //Datagridview 2
            refreshDatagridviewValueDatagridview2();
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
        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.bgWorkerExportExcelFile_DoWork(this.connection);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            Console.WriteLine(selectedIndex);
        }
    }
}
