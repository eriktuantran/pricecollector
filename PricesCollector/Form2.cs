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

namespace PricesCollector
{
    using MyDictionary = System.Collections.Generic.Dictionary<string, ProductData>;

    public partial class Form2 : Form
    {
        private string connectionString = "server=127.0.0.1;user id=root;password=3V5wn0Kv9RRc8gQA;persistsecurityinfo=True;database=pricecollector";
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


            connection = new MySqlConnection(connectionString);

            refreshDatagridviewValue();
            timerUpdateDb.Enabled = true;
            timerUpdateDb.Interval = 3*1000;
            startProgressBar(3);
        }

        private int column(string columnName)
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


        class customObj
        {
            public string Value { get; set; }
            public string Display { get; set; }
            public customObj(string value, string display)
            {
                this.Value = value;
                this.Display = display;
            }

        }

        private int column2(string columnName)
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

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            syncDatagridviewToDataSource();
        }

        private void syncDatagridviewToDataSource()
        {
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
            if (e.ColumnIndex == column2("link"))
            {
                try
                {
                    string url = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (url.Trim() != "")
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
                MessageBox.Show("It is busy, please wait!");
                return;
            }
            polulateLinkToDictionary();
            refreshDBPricesMultiThreadAsync();
            refreshDatagridviewValue();
        }

        private void timerUpdateDB_Tick(object sender, EventArgs e)
        {
            //Stop the timer update DB
            timerUpdateDb.Enabled = false;

            polulateLinkToDictionary();
            refreshDBPricesMultiThreadAsync();
            //refreshDatagridviewValue(); //No need to update right now, it will be updated later when multi thread done
        }

        private void polulateLinkToDictionary()
        {
            myDict.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlDataReader reader = null;
                string selectCmd = "select id,link from product;";

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
                            ProductData dat = new ProductData();
                            dat.link = link;
                            dat.productId = Int32.Parse(id);
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



        List<BackgroundWorker> backgroundWorkerForFetchingList = new List<BackgroundWorker>();

        private void refreshDBPricesMultiThreadAsync()
        {
            foreach (var item in myDict)
            {
                ProductData data = item.Value;
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

            data.populateDataFromTikiLink();

            //Do your work
            e.Result = data;
        }
        private void bgWorkerForFetching_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if(progressBarFetching.Value < progressBarFetching.Maximum)
            {
                progressBarFetching.Value++;
            }

            BackgroundWorker bgw = (BackgroundWorker)sender;
            backgroundWorkerForFetchingList.Remove(bgw);
            bgw.Dispose();
            if (backgroundWorkerForFetchingList.Count == 0)
            {
                updateDbWhenFetchingDone();
                refreshDatagridviewValue();
                progressBarFetching.Value = 0;

                //Restart the timer update DB
                timerUpdateDb.Interval = (int)timerValue.Value * 1000;
                timerUpdateDb.Enabled = true;
                startProgressBar((int)timerValue.Value);
            }

            //if (e.Error != null)
            //{
            //    Console.WriteLine("ERROR: " + e.Error.ToString());
            //}
            //else
            //{
            //    ProductData data = (ProductData)e.Result;
            //    Console.WriteLine("Job Done: {0} {1} {2}",  data.productId , data.sku, myDict[data.productId.ToString()].sku);
            //}
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

                    Console.WriteLine("{0} _ {1} _ {2} _ {3} _ {4}", item.Key, data.link, data.sellerName, data.currentPrice.ToString(), data.lowestPrice.ToString());

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "update product set ";
                    cmd.CommandText += "seller_name ='" + data.sellerName + "',";
                    cmd.CommandText += "sku ='" + data.sku + "',";
                    cmd.CommandText += "current_price ='" + data.currentPrice.ToString() + "',";
                    cmd.CommandText += "common_price ='" + data.listPrice.ToString() + "',";
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
                mySqlDataAdapter = new MySqlDataAdapter("select id,seller_name,product_group,sku,active,current_price,lowest_price,common_price,other_seller,link from product", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);

                int rowIndex = 0;
                foreach (DataRow _row in DS.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(_row.ItemArray);

                    DataGridViewComboBoxCell cellCombo = new DataGridViewComboBoxCell();
                    string str = _row["other_seller"].ToString();
                    List<string> s = str.Split('\n').ToList();
                    cellCombo.DataSource = s;
                    dataGridView1.Rows[rowIndex].Cells["other_seller"] = cellCombo;
                    //cellCombo.DisplayMember = "";
                    

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

                    rowIndex++;
                }


                //close connection
                this.CloseConnection();
            }
        }




        BackgroundWorker _backgroundWorkerForProgressBar;

        private BackgroundWorker CreateBackgroundWorker()
        {
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bgWorkerProgressBarUpdateDb_DoWork;
            bw.ProgressChanged += bgWorkerProgressBarUpdateDb_ProgressChanged;
            return bw;
        }

        private void timerValue_ValueChanged(object sender, EventArgs e)
        {
            if(isFetching)
            {
                Console.WriteLine("Fetching threads are already running");
                return;
            }

            timerUpdateDb.Enabled = false;
            int timerInSecond = (int)timerValue.Value;
            timerUpdateDb.Interval = timerInSecond * 1000;
            timerUpdateDb.Enabled = true;

            startProgressBar(timerInSecond);
        }

        private void startProgressBar(int timerInSecond)
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
            progressBarUpdateDb.Value = Int32.Parse(e.ProgressPercentage.ToString());
        }

        private void bgWorkerUpdateDb_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            bgw.Dispose();
        }
    }
}
