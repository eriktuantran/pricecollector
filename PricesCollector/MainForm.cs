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
using System.Threading;

namespace PricesCollector
{
    using MyDictionary = System.Collections.Generic.Dictionary<string, ProductData>;
    using ConfigDictionary = System.Collections.Generic.Dictionary<string, string>;

    enum Tab
    {
        TabTiki = 0,
        TabOtherWebsite = 1
    }

    public partial class MainForm : Form
    {
        private string connectionString = "";//"server=127.0.0.1;user id=root;password=3V5wn0Kv9RRc8gQA;persistsecurityinfo=True;database=pricecollector";
        private int timeoutUpdateDB = 0;
        private bool globalRunningStateTiki = false;
        private bool globalRunningStateOtherWebsite = false;
        private bool isManualFetching = false;

        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;

        private MyDictionary myDictTiki = new MyDictionary();
        private MyDictionary myDictOtherWebsite = new MyDictionary();

        private bool isFetchingTiki
        {
            get
            {
                return backgroundWorkerForFetchingListTiki.Count != 0;
            }
        }

        private bool isFetchingOtherWebsite
        {
            get
            {
                return backgroundWorkerForFetchingListOtherWebsite.Count != 0;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            createDatagridview1(dataGridView1);
            createDatagridview2(dataGridView2);
            tabControl1.SelectedIndex = (int)Tab.TabTiki;
            
        }

        private void createDatagridview1(DataGridView mydataGridView)
        {
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.HeaderText = "ID";
            idColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(idColumn);

            mydataGridView.Columns.Add("seller_name", "Seller name");
            mydataGridView.Columns.Add("product_group", "Group");
            mydataGridView.Columns.Add("product_name", "Product name");
            mydataGridView.Columns.Add("sku", "SKU");
            mydataGridView.Columns.Add("msku", "MSKU");

            DataGridViewTextBoxColumn currentPrice = new DataGridViewTextBoxColumn();
            currentPrice.Name = "current_price";
            currentPrice.HeaderText = "Tiki price";
            currentPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            currentPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(currentPrice);

            DataGridViewTextBoxColumn discountPrice = new DataGridViewTextBoxColumn();
            discountPrice.Name = "discount_price";
            discountPrice.HeaderText = "Discount price";
            discountPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            discountPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(discountPrice);

            DataGridViewTextBoxColumn minimumPrice = new DataGridViewTextBoxColumn();
            minimumPrice.Name = "minimum_price";
            minimumPrice.HeaderText = "Minimum price";
            minimumPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            minimumPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(minimumPrice);

            DataGridViewTextBoxColumn otherTiki = new DataGridViewTextBoxColumn();
            otherTiki.Name = "other_seller_tiki";
            otherTiki.HeaderText = "Other Tiki";
            otherTiki.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherTiki.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(otherTiki);

            mydataGridView.Columns.Add("lowest_price_tiki", "Lowest Tiki");

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "active";
            checkColumn.HeaderText = "Active";
            checkColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(checkColumn);

            DataGridViewTextBoxColumn linkTiki = new DataGridViewTextBoxColumn();
            linkTiki.Name = "link_tiki";
            linkTiki.HeaderText = "Tiki link";
            linkTiki.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            linkTiki.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(linkTiki);

            //Datagridview Width/Height not overflow
            mydataGridView.Columns[Utilities.colNameToIndex("link_tiki", mydataGridView)].Width = 100;// 500;
            mydataGridView.Width = this.tabControl1.Width - 15;
            mydataGridView.Height = this.tabControl1.Height - 30;
            mydataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mydataGridView.Columns[0].Frozen = true;
        }
        private void createDatagridview2(DataGridView mydataGridView)
        {
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.HeaderText = "ID";
            idColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(idColumn);

            mydataGridView.Columns.Add("seller_name", "Seller name");
            mydataGridView.Columns.Add("product_group", "Group");
            mydataGridView.Columns.Add("product_name", "Product name");
            mydataGridView.Columns.Add("sku", "SKU");
            mydataGridView.Columns.Add("msku", "MSKU");

            DataGridViewTextBoxColumn currentPrice = new DataGridViewTextBoxColumn();
            currentPrice.Name = "current_price";
            currentPrice.HeaderText = "Tiki price";
            currentPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            currentPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(currentPrice);

            DataGridViewTextBoxColumn discountPrice = new DataGridViewTextBoxColumn();
            discountPrice.Name = "discount_price";
            discountPrice.HeaderText = "Discount price";
            discountPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            discountPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(discountPrice);

            DataGridViewTextBoxColumn minimumPrice = new DataGridViewTextBoxColumn();
            minimumPrice.Name = "minimum_price";
            minimumPrice.HeaderText = "Minimum price";
            minimumPrice.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            minimumPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(minimumPrice);

            DataGridViewTextBoxColumn otherTiki = new DataGridViewTextBoxColumn();
            otherTiki.Name = "other_seller_tiki";
            otherTiki.HeaderText = "Other Tiki";
            otherTiki.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherTiki.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(otherTiki);

            DataGridViewTextBoxColumn otherLazada = new DataGridViewTextBoxColumn();
            otherLazada.Name = "other_seller_lazada";
            otherLazada.HeaderText = "Other Lazada";
            otherLazada.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherLazada.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(otherLazada);

            DataGridViewTextBoxColumn otherShopee = new DataGridViewTextBoxColumn();
            otherShopee.Name = "other_seller_shopee";
            otherShopee.HeaderText = "Other Shopee";
            otherShopee.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherShopee.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(otherShopee);

            DataGridViewTextBoxColumn otherSendo = new DataGridViewTextBoxColumn();
            otherSendo.Name = "other_seller_sendo";
            otherSendo.HeaderText = "Other Sendo";
            otherSendo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //otherSendo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(otherSendo);
            
            mydataGridView.Columns.Add("lowest_price_tiki", "Lowest Tiki");
            mydataGridView.Columns.Add("lowest_price_lazada", "Lowest Lazada");
            mydataGridView.Columns.Add("lowest_price_shopee", "Lowest Shopee");
            mydataGridView.Columns.Add("lowest_price_sendo", "Lowest Sendo");


            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "active";
            checkColumn.HeaderText = "Active";
            checkColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(checkColumn);

            DataGridViewTextBoxColumn linkTiki = new DataGridViewTextBoxColumn();
            linkTiki.Name = "link_tiki";
            linkTiki.HeaderText = "Tiki link";
            linkTiki.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            linkTiki.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(linkTiki);

            DataGridViewTextBoxColumn linkLazada = new DataGridViewTextBoxColumn();
            linkLazada.Name = "link_lazada";
            linkLazada.HeaderText = "Lazada links";
            linkLazada.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //linkLazada.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(linkLazada);

            DataGridViewTextBoxColumn linkShopee = new DataGridViewTextBoxColumn();
            linkShopee.Name = "link_shopee";
            linkShopee.HeaderText = "Shopee links";
            linkShopee.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //linkShopee.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(linkShopee);

            DataGridViewTextBoxColumn linkSendo = new DataGridViewTextBoxColumn();
            linkSendo.Name = "link_sendo";
            linkSendo.HeaderText = "Sendo links";
            linkSendo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //linkSendo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            mydataGridView.Columns.Add(linkSendo);



            //Datagridview Width/Height not overflow
            mydataGridView.Columns[Utilities.colNameToIndex("link_tiki", mydataGridView)].Width = 100;// 500;
            mydataGridView.Width = this.tabControl1.Width - 15;
            mydataGridView.Height = this.tabControl1.Height - 30;
            mydataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mydataGridView.Columns[0].Frozen = true;
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
            
            // Db connection
            connection = new MySqlConnection(connectionString);

            // Data gridview
            refreshDatagridviewValue();
            refreshDatagridviewValueDatagridview2();

            //Timer Tiki
            timerUpdateDbTiki.Enabled = true;
            timerUpdateDbTiki.Interval = 10 * 1000;
            progressBarUpdateDbTiki.Value = 0;
            startProgressBarUpdateDbTiki(10);

            //Enable the global flag tiki
            globalRunningStateTiki = true;


            //Timer other website
            timerUpdateDbOtherWebsite.Enabled = true;
            timerUpdateDbOtherWebsite.Interval = 10 * 1000;
            progressBarUpdateDbOtherWebsite.Value = 0;
            startProgressBarUpdateDbOtherWebsite(10);

            //Enable the global flag other website
            globalRunningStateOtherWebsite = true;

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
            else if (e.ColumnIndex == Utilities.colNameToIndex("link_lazada", mydataGridView))
            {
                Utilities.updateLinkLazadaCellValue(mydataGridView, e.RowIndex, productId, this.connection);
            }
            else if (e.ColumnIndex == Utilities.colNameToIndex("link_shopee", mydataGridView))
            {
                Utilities.updateLinkShopeeCellValue(mydataGridView, e.RowIndex, productId, this.connection);
            }
            else if (e.ColumnIndex == Utilities.colNameToIndex("link_sendo", mydataGridView))
            {
                Utilities.updateLinkSendoCellValue(mydataGridView, e.RowIndex, productId, this.connection);
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
            if(isFetchingTiki || isFetchingOtherWebsite)
            {
                Console.WriteLine("Fetching threads are already running");
                MessageBox.Show("It is busy, please wait!", "Busy...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Console.WriteLine("Manual fetching triggered");

            //Manual fetching 
            isManualFetching = true;

            //Stop the timer update DB
            timerUpdateDbTiki.Enabled = false;
            stopProgressBarUpdateDbTiki();

            polulateLinkToDictionary(myDictTiki);
            refreshDBPricesMultiThreadAsyncTiki(myDictTiki);
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }

        private void timerUpdateDBTiki_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer update Tiki ticked");

            //Stop the timer update DB
            timerUpdateDbTiki.Enabled = false;
            stopProgressBarUpdateDbTiki();

            polulateLinkToDictionary(myDictTiki);
            refreshDBPricesMultiThreadAsyncTiki(myDictTiki);
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }
        private void timerUpdateDbOtherWebsite_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer update Other website ticked");

            //Stop the timer update DB
            timerUpdateDbOtherWebsite.Enabled = false;
            stopProgressBarUpdateDbOtherWebsite();

            polulateLinkToDictionary(myDictOtherWebsite);
            refreshDBPricesMultiThreadAsyncOtherWebsite(myDictOtherWebsite);
            //refreshDatagridviewValue(); // No need to update right now, it will be updated later when multi thread done
        }

        private void polulateLinkToDictionary(MyDictionary _myDict)
        {
            _myDict.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlDataReader reader = null;
                string selectCmd = "select id,active,link_tiki,link_lazada,link_shopee,link_sendo from product;";

                MySqlCommand command = new MySqlCommand(selectCmd, connection);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string id = reader.GetString(0);
                            string active = reader.GetString(1).ToLower();
                            string link_tiki = reader.GetString(2);
                            string linkLazada = reader.GetString(3);
                            string linkShopee = reader.GetString(4);
                            string linkSendo = reader.GetString(5);

                            ProductData dat = new ProductData();
                            dat.productId = Int32.Parse(id);
                            dat.isActive = active == "true" ? true : false;
                            dat.linkTiki = link_tiki;
                            dat.linkLazadaRaw = linkLazada;
                            dat.linkShopeeRaw = linkShopee;
                            dat.linkSendoRaw = linkSendo;
                            _myDict[id] = dat;
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

        List<BackgroundWorker> backgroundWorkerForFetchingListTiki = new List<BackgroundWorker>();
        List<BackgroundWorker> backgroundWorkerForFetchingListOtherWebsite = new List<BackgroundWorker>();

        private void refreshDBPricesMultiThreadAsyncTiki(MyDictionary _myDict)
        {
            foreach (var item in _myDict)
            {
                ProductData data = item.Value;
                if(data.isActive == false)
                {
                    //This item is not active
                    //Console.WriteLine("Skip this product, it is not active");
                    continue;
                }

                data.isFullFetching = false;

                startProgressFetchingByMultiThread(data);
            }
            progressBarFetchingTiki.Maximum = backgroundWorkerForFetchingListTiki.Count;
        }
        private void refreshDBPricesMultiThreadAsyncOtherWebsite(MyDictionary _myDict)
        {
            foreach (var item in _myDict)
            {
                ProductData data = item.Value;
                if (data.isActive == false)
                {
                    //This item is not active
                    //Console.WriteLine("Skip this product, it is not active");
                    continue;
                }
           
                data.isFullFetching = true;
                
                startProgressFetchingByMultiThread(data);
            }
            progressBarFetchingOtherWebsite.Maximum = backgroundWorkerForFetchingListOtherWebsite.Count;
        }

        
        private void startProgressFetchingByMultiThread(ProductData data)
        {
            try
            {
                if(data.isFullFetching == false)
                {
                    BackgroundWorker bgw = CreateBackgroundWorkerForFetchingTiki();
                    backgroundWorkerForFetchingListTiki.Add(bgw);
                    bgw.RunWorkerAsync(data);
                }
                else
                {
                    BackgroundWorker bgw = CreateBackgroundWorkerForFetchingOtherWebsite();
                    backgroundWorkerForFetchingListOtherWebsite.Add(bgw);
                    bgw.RunWorkerAsync(data);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private BackgroundWorker CreateBackgroundWorkerForFetchingTiki()
        {
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerForFetching_DoWork;
            bw.RunWorkerCompleted += bgWorkerForFetchingTiki_RunWorkerCompleted;
            return bw;
        }
        private BackgroundWorker CreateBackgroundWorkerForFetchingOtherWebsite()
        {
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerForFetching_DoWork;
            bw.RunWorkerCompleted += bgWorkerForFetchingOtherWebsite_RunWorkerCompleted;
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

        private void bgWorkerForFetchingTiki_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            backgroundWorkerForFetchingListTiki.Remove(bgw);
            bgw.Dispose();

            if (globalRunningStateTiki == false && isManualFetching == false)
            {
                return; //button stop clicked
            }

            if (progressBarFetchingTiki.Value < progressBarFetchingTiki.Maximum)
            {
                progressBarFetchingTiki.Value++;
            }

            if (backgroundWorkerForFetchingListTiki.Count == 0) //All threads have finished the job
            {
                Console.WriteLine("All threads have finished the job");

                updateDbWhenFetchingDone(myDictTiki); //Store to DB
                refreshDatagridviewValue(); //Fetch from DB
                progressBarFetchingTiki.Value = 0;

                //Restart the timer update DB
                timerUpdateDbTiki.Interval = timeoutUpdateDB * 1000;
                timerUpdateDbTiki.Enabled = true;

                //Reset the manual fetching, it is done
                if (isManualFetching == true)
                {
                    isManualFetching = false;
                    if(globalRunningStateTiki == false)
                    {
                        return;
                    }
                }

                //Start timer update DB
                startProgressBarUpdateDbTiki(timeoutUpdateDB);
            }

            if (e.Error != null)
            {
                Console.WriteLine("ERROR: " + e.Error.ToString());
            }
        }
        private void bgWorkerForFetchingOtherWebsite_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            backgroundWorkerForFetchingListOtherWebsite.Remove(bgw);
            bgw.Dispose();

            if (globalRunningStateOtherWebsite == false && isManualFetching == false)
            {
                return; //button stop clicked
            }

            if (progressBarFetchingOtherWebsite.Value < progressBarFetchingOtherWebsite.Maximum)
            {
                progressBarFetchingOtherWebsite.Value++;
            }

            if (backgroundWorkerForFetchingListOtherWebsite.Count == 0) //All threads have finished the job
            {
                Console.WriteLine("All threads have finished the job other website");

                updateDbWhenFetchingDone(myDictOtherWebsite); //Store to DB
                refreshDatagridviewValue(); //Fetch from DB
                progressBarFetchingOtherWebsite.Value = 0;

                //Restart the timer update DB
                timerUpdateDbOtherWebsite.Interval = timeoutUpdateDB * 1000;
                timerUpdateDbOtherWebsite.Enabled = true;

                //Reset the manual fetching, it is done
                if (isManualFetching == true)
                {
                    isManualFetching = false;
                    if (globalRunningStateOtherWebsite == false)
                    {
                        return;
                    }
                }

                //Start timer update DB
                startProgressBarUpdateDbOtherWebsite(timeoutUpdateDB);
            }

            if (e.Error != null)
            {
                Console.WriteLine("ERROR: " + e.Error.ToString());
            }
        }


        private void updateDbWhenFetchingDone(MyDictionary myDict)
        {
            if (this.OpenConnection() == true)
            {
                Console.WriteLine("updating Db When Fetching Done");
                foreach (var item in myDict)
                {
                    ProductData data = item.Value;
                    
                    string otherSellerTikiStringToDB = Utilities.getOtherSellerString(data.SortedListTiki());
                    string otherSellerLazadaStringToDB = Utilities.getOtherSellerString(data.SortedListLazada());
                    string otherSellerShopeeStringToDB = Utilities.getOtherSellerString(data.SortedListShopee());
                    string otherSellerSendoStringToDB = Utilities.getOtherSellerString(data.SortedListSendo());


                    //Console.WriteLine("{0} _ {1} _ {2} _ {3} _ {4}", item.Key, data.link_tiki, data.sellerName, data.currentPrice.ToString(), data.lowestPrice.ToString());

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "update product set ";
                    if (data.isFullFetching == false)
                    {
                        cmd.CommandText += "seller_name ='" + data.sellerName + "',";
                        cmd.CommandText += "product_name ='" + data.productName + "',";
                        cmd.CommandText += "sku ='" + data.sku + "',";
                        cmd.CommandText += "current_price ='" + data.currentPrice.ToString() + "',";
                        cmd.CommandText += "discount_price ='" + data.discountPrice.ToString() + "',";
                        cmd.CommandText += "lowest_price_tiki ='" + data.lowestPriceTiki.ToString() + "',";
                        cmd.CommandText += "other_seller_tiki ='" + otherSellerTikiStringToDB + "' ";
                    }
                    else
                    {
                        cmd.CommandText += "lowest_price_lazada ='" + data.lowestPriceLazada.ToString() + "',";
                        cmd.CommandText += "lowest_price_shopee ='" + data.lowestPriceShopee.ToString() + "',";
                        cmd.CommandText += "lowest_price_sendo ='" + data.lowestPriceSendo.ToString() + "',";
                        cmd.CommandText += "other_seller_lazada ='" + otherSellerLazadaStringToDB + "',";
                        cmd.CommandText += "other_seller_shopee ='" + otherSellerShopeeStringToDB + "',";
                        cmd.CommandText += "other_seller_sendo ='" + otherSellerSendoStringToDB + "' ";
                    }
                    
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
                string columnsToDisplay = "id,seller_name,product_group,product_name,sku,msku," +
                    "current_price,discount_price,minimum_price," +
                    "other_seller_tiki,other_seller_lazada,other_seller_shopee,other_seller_sendo," +
                    "lowest_price_tiki,lowest_price_lazada,lowest_price_shopee,lowest_price_sendo," +
                    "active,link_tiki,link_lazada,link_shopee,link_sendo";
                mySqlDataAdapter = new MySqlDataAdapter("select "+ columnsToDisplay + " from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                //close connection
                this.CloseConnection();
                
                DataTable tableFromDb = DS.Tables[0];
                List<MyRow> rowList = new List<MyRow>();

                // Grouping the red or not read cell
                foreach (DataRow _row in tableFromDb.Rows)
                {
                    int currentPriceTiki = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("current_price", dataGridView2)].ToString());
                    List<int> listOtherPrice = new List<int>();
                    int temp = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price_tiki", dataGridView2)].ToString());
                    if(temp != 0) listOtherPrice.Add(temp);
                    temp = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price_lazada", dataGridView2)].ToString());
                    if (temp != 0) listOtherPrice.Add(temp);
                    temp = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price_shopee", dataGridView2)].ToString());
                    if (temp != 0) listOtherPrice.Add(temp);
                    temp = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price_sendo", dataGridView2)].ToString());
                    if (temp != 0) listOtherPrice.Add(temp);


                    int lowestPriceTotal = 0;
                    if (listOtherPrice.Count != 0) lowestPriceTotal = listOtherPrice.Min();

                    bool isActive = _row.ItemArray[Utilities.colNameToIndex("active", dataGridView2)].ToString().ToLower() == "true" ? true : false;

                    MyRow myRow = new MyRow();
                    myRow.row = _row;
                    if(isActive == false)
                    {
                        myRow.group = 2; // Active = false
                    }
                    else if ((lowestPriceTotal < currentPriceTiki && lowestPriceTotal != 0) || currentPriceTiki == 0 )
                    {
                        myRow.group = 0; // RED group
                    }
                    else
                    {
                        myRow.group = 1; // Green group
                    }
                    rowList.Add(myRow);
                }

                List<MyRow> sortedRowList = rowList.OrderBy(o => o.group).ToList();

                int rowIndex = 0;
                foreach(MyRow row in sortedRowList)
                {
                    var _row = row.row;

                    dataGridView2.Rows.Add(_row.ItemArray);

                    //Color for lowest price [Tiki, Lazada, SHopee, Sendo]
                    string maxCol, minCol;
                    int max, min;
                    Utilities.getMinMaxAndColumnName(dataGridView2.Rows[rowIndex].Cells, out maxCol, out max, out minCol, out min);
                    
                    if (max > 0)
                    {
                        dataGridView2.Rows[rowIndex].Cells[maxCol].Style.BackColor = Color.LightPink;
                    }
                    if (min != Int32.MaxValue && min != max)
                    {
                        dataGridView2.Rows[rowIndex].Cells[minCol].Style.BackColor = Color.LightGreen;
                    }


                    //Color for Tiki price cell
                    bool isActive = dataGridView2.Rows[rowIndex].Cells["active"].Value.ToString().ToLower() == "true" ? true : false;
                    int currentPrice = Int32.Parse( dataGridView2.Rows[rowIndex].Cells["current_price"].Value.ToString());
                    int lowestPrice = min;
                    
                    if(isActive == false)
                    {
                        dataGridView2.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Gray;
                    }
                    else if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0 )
                    {
                        dataGridView2.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView2.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.LightGreen;
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
            if (tabControl1.SelectedIndex == (int)Tab.TabTiki)
            {
                Console.WriteLine("Refresh view");
                dataGridView1.Rows.Clear();

                if (this.OpenConnection() == true)
                {
                    string columnsToDisplay = "id,seller_name,product_group,product_name,sku,msku," +
                        "current_price,discount_price,minimum_price," +
                        "other_seller_tiki," +
                        "lowest_price_tiki," +
                        "active,link_tiki";
                    mySqlDataAdapter = new MySqlDataAdapter("select " + columnsToDisplay + " from product", connection);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);

                    //close connection
                    this.CloseConnection();

                    DataTable tableFromDb = DS.Tables[0];
                    List<MyRow> rowList = new List<MyRow>();

                    foreach (DataRow _row in tableFromDb.Rows)
                    {
                        bool isActive = _row.ItemArray[Utilities.colNameToIndex("active", dataGridView1)].ToString().ToLower() == "true" ? true : false;
                        int currentPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("current_price", dataGridView1)].ToString());
                        int lowestPrice = Int32.Parse(_row.ItemArray[Utilities.colNameToIndex("lowest_price_tiki", dataGridView1)].ToString());
                        //Console.WriteLine("{0}==={1}",currentPrice, lowestPrice);

                        MyRow myRow = new MyRow();
                        myRow.row = _row;
                        if (isActive == false)
                        {
                            myRow.group = 2; // Active = false
                        }
                        else if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0)
                        {
                            myRow.group = 0;
                        }
                        else
                        {
                            myRow.group = 1;
                        }

                        rowList.Add(myRow);
                    }

                    List<MyRow> sortedRowList = rowList.OrderBy(o => o.group).ToList();


                    int rowIndex = 0;
                    foreach (MyRow row in sortedRowList)
                    {
                        var _row = row.row;

                        dataGridView1.Rows.Add(_row.ItemArray);

                        //Color for Tiki price cell
                        bool isActive = dataGridView1.Rows[rowIndex].Cells["active"].Value.ToString().ToLower() == "true" ? true : false;
                        int currentPrice = Int32.Parse(dataGridView1.Rows[rowIndex].Cells["current_price"].Value.ToString());
                        int lowestPrice = Int32.Parse(dataGridView1.Rows[rowIndex].Cells["lowest_price_tiki"].Value.ToString());

                        if (isActive == false)
                        {
                            dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Gray;
                        }
                        else if ((lowestPrice < currentPrice && lowestPrice != 0) || currentPrice == 0)
                        {
                            dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dataGridView1.Rows[rowIndex].Cells["current_price"].Style.BackColor = Color.LightGreen;
                        }

                        //Link color
                        var obj = (DataGridViewTextBoxCell)dataGridView1.Rows[rowIndex].Cells["link_tiki"];
                        obj.Style.ForeColor = Color.Blue;

                        rowIndex++;
                    }
                }

            } // end checking tabControl1 = 0
            else if(tabControl1.SelectedIndex == (int)Tab.TabOtherWebsite)
            {
                //Datagridview 2
                refreshDatagridviewValueDatagridview2();
            }
        }


        /// <summary>
        /// Timeout value changed
        /// </summary>
        /// 
        private void timeoutUpdateDBChangedFromSettingFormEvent()
        {
            if(isFetchingTiki || isFetchingOtherWebsite)
            {
                Console.WriteLine("Fetching threads are already running");
                return;
            }

            //Tiki part
            timerUpdateDbTiki.Enabled = false;
            timerUpdateDbTiki.Interval = timeoutUpdateDB * 1000;
            timerUpdateDbTiki.Enabled = true;
            startProgressBarUpdateDbTiki(timeoutUpdateDB);

            //Other websites part
            timerUpdateDbOtherWebsite.Enabled = false;
            timerUpdateDbOtherWebsite.Interval = timeoutUpdateDB * 1000;
            timerUpdateDbOtherWebsite.Enabled = true;
            startProgressBarUpdateDbOtherWebsite(timeoutUpdateDB);
        }


        /// <summary>
        /// Progress bar update DB
        /// </summary>
        
        BackgroundWorker backgroundWorkerForProgressBarTiki;
        BackgroundWorker backgroundWorkerForProgressBarOtherWebsite;

        private void startProgressBarUpdateDbTiki(int timerInSecond)
        {
            try
            {
                //Cancel whatever it is you're doing!
                if (backgroundWorkerForProgressBarTiki != null)
                {
                    backgroundWorkerForProgressBarTiki.CancelAsync();
                }

                backgroundWorkerForProgressBarTiki = CreateBackgroundWorkerTiki();

                //And start doing this immediately!
                backgroundWorkerForProgressBarTiki.RunWorkerAsync(timerInSecond);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void startProgressBarUpdateDbOtherWebsite(int timerInSecond)
        {
            try
            {
                //Cancel whatever it is you're doing!
                if (backgroundWorkerForProgressBarOtherWebsite != null)
                {
                    backgroundWorkerForProgressBarOtherWebsite.CancelAsync();
                }

                backgroundWorkerForProgressBarOtherWebsite = CreateBackgroundWorkerOtherWebsite();

                //And start doing this immediately!
                backgroundWorkerForProgressBarOtherWebsite.RunWorkerAsync(timerInSecond);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private BackgroundWorker CreateBackgroundWorkerTiki()
        {
            Console.WriteLine("### CreateBackgroundWorker");
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerProgressBarUpdateDb_DoWork;
            bw.ProgressChanged += bgWorkerProgressBarUpdateDbTiki_ProgressChanged;
            bw.RunWorkerCompleted += bgWorkerProgressBarUpdateDb_RunWorkerCompleted;
            return bw;
        }
        private BackgroundWorker CreateBackgroundWorkerOtherWebsite()
        {
            Console.WriteLine("### CreateBackgroundWorker Other website");
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerProgressBarUpdateDb_DoWork;
            bw.ProgressChanged += bgWorkerProgressBarUpdateDbOtherWebsite_ProgressChanged;
            bw.RunWorkerCompleted += bgWorkerProgressBarUpdateDb_RunWorkerCompleted;
            return bw;
        }

        private void stopProgressBarUpdateDbTiki()
        {
            try
            {
                if (backgroundWorkerForProgressBarTiki != null)
                {
                    backgroundWorkerForProgressBarTiki.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void stopProgressBarUpdateDbOtherWebsite()
        {
            try
            {
                if (backgroundWorkerForProgressBarOtherWebsite != null)
                {
                    backgroundWorkerForProgressBarOtherWebsite.CancelAsync();
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

        private void bgWorkerProgressBarUpdateDbTiki_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(globalRunningStateTiki == false)
            {
                return;// btn stop clicked
            }

            progressBarUpdateDbTiki.Value = Int32.Parse(e.ProgressPercentage.ToString());
        }
        private void bgWorkerProgressBarUpdateDbOtherWebsite_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (globalRunningStateOtherWebsite == false)
            {
                return;// btn stop clicked
            }

            progressBarUpdateDbOtherWebsite.Value = Int32.Parse(e.ProgressPercentage.ToString());
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
            if(this.tabControl1.SelectedIndex == (int)Tab.TabTiki )
            {
                Utilities.triggerExportExcelFileVersion2(this.connection, dataGridView1);
            }
            else
            {
                Utilities.triggerExportExcelFileVersion2(this.connection, dataGridView2);
            }
        }


        /// <summary>
        /// Button STOP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnStopTiki_Click(object sender, EventArgs e)
        {
            if(isFetchingTiki)
            {
                MessageBox.Show("It is fetching, please wait!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (globalRunningStateTiki == true)
            {
                globalRunningStateTiki = false;
                btnStopTiki.Text = "Start";
                timerUpdateDbTiki.Stop();
                stopProgressBarUpdateDbTiki();
                progressBarFetchingTiki.Value = 0;
                progressBarUpdateDbTiki.Value = 0;
            }
            else
            {
                globalRunningStateTiki = true;
                btnStopTiki.Text = "Stop";
                timerUpdateDbTiki.Interval = timeoutUpdateDB * 1000;
                timerUpdateDbTiki.Start();
                startProgressBarUpdateDbTiki(timeoutUpdateDB);
            }
        }
        private void btnStopOtherWebsite_Click(object sender, EventArgs e)
        {
            if (isFetchingOtherWebsite)
            {
                MessageBox.Show("It is fetching, please wait!", "Resource busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (globalRunningStateOtherWebsite == true)
            {
                globalRunningStateOtherWebsite = false;
                btnStopOtherWebsite.Text = "Start";
                timerUpdateDbOtherWebsite.Stop();
                stopProgressBarUpdateDbOtherWebsite();
                progressBarFetchingOtherWebsite.Value = 0;
                progressBarUpdateDbOtherWebsite.Value = 0;
            }
            else
            {
                globalRunningStateOtherWebsite = true;
                btnStopOtherWebsite.Text = "Stop";
                timerUpdateDbOtherWebsite.Interval = timeoutUpdateDB * 1000;
                timerUpdateDbOtherWebsite.Start();
                startProgressBarUpdateDbOtherWebsite(timeoutUpdateDB);
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
            refreshDatagridviewValue();
        }


    }
}
