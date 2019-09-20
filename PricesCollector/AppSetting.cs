using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MyDictionary = System.Collections.Generic.Dictionary<string, string>;

namespace PricesCollector
{
    public partial class AppSetting : Form
    {
        public string connectionString = "";
        public int timeoutTiki = 300;
        public int timeoutOtherWebsite = 300;

        //DB
        private MySqlConnection connection;
        private string server;
        private string database;
        public string uid;
        private string password;

        //Store config
        MyDictionary globalDictionary = new MyDictionary();
        Configuration config;

        //Button OK clicked
        public bool isOKButtonClicked = false;

        public AppSetting()
        {
            InitializeComponent();

            config = new Configuration();

            updateAllFormValueFromDictionary(config.loadDictionaryFromFile());

            // Db tester + prepgress bar
            progressbarWorker.WorkerReportsProgress = true;
            progressbarWorker.WorkerSupportsCancellation = true;
            progressBar1.Visible = false;
        }

        void updateAllFormValueFromDictionary(MyDictionary readConfigDict)
        {
            if (readConfigDict.ContainsKey("dbip") && readConfigDict["dbip"] != "")
            {
                txtDbIP.Text = readConfigDict["dbip"];
            }
            if (readConfigDict.ContainsKey("dbport") && readConfigDict["dbport"] != "")
            {
                txtDbPort.Text = readConfigDict["dbport"];
            }
            if (readConfigDict.ContainsKey("dbuser") && readConfigDict["dbuser"] != "")
            {
                txtDbUser.Text = readConfigDict["dbuser"];
            }
            if (readConfigDict.ContainsKey("dbpass") && readConfigDict["dbpass"] != "")
            {
                txtDbPasswd.Text = readConfigDict["dbpass"];
            }
            if (readConfigDict.ContainsKey("dbname") && readConfigDict["dbname"] != "")
            {
                txtDbName.Text = readConfigDict["dbname"];
            }
            if (readConfigDict.ContainsKey("timeouttiki") && readConfigDict["timeouttiki"] != "")
            {
                timerValueTiki.Value = (decimal) Int32.Parse( readConfigDict["timeouttiki"]);
            }
            if (readConfigDict.ContainsKey("timeoutother") && readConfigDict["timeoutother"] != "")
            {
                timerValueOtherWebsite.Value = (decimal)Int32.Parse(readConfigDict["timeoutother"]);
            }
        }

        void updateDictionaryEvent()
        {

            globalDictionary["connectstring"] = this.connectionString;

            globalDictionary["dbip"] = txtDbIP.Text;
            globalDictionary["dbport"] = txtDbPort.Text;
            globalDictionary["dbuser"] = txtDbUser.Text;
            globalDictionary["dbpass"] = txtDbPasswd.Text;
            globalDictionary["dbname"] = txtDbName.Text;

            globalDictionary["timeouttiki"] = timerValueTiki.Value.ToString();
            globalDictionary["timeoutother"] = timerValueOtherWebsite.Value.ToString();
        }

        string updateConnectionString()
        {
            server = txtDbIP.Text.Trim();
            database = txtDbName.Text.Trim();
            uid = txtDbUser.Text.Trim();
            password = txtDbPasswd.Text.Trim();
            string _connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";CharSet=utf8mb4;";

            this.connectionString = _connectionString;
            return _connectionString;
        }

        string testConnection()
        {
            string result = "";
            result = "Connecting...";
            connection = new MySqlConnection(updateConnectionString());
            try
            {
                connection.Open();
                connection.Close();
                result = "Successful!";
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        result = "Cannot connect";
                        break;
                    case 1045:
                        result = "Invalid user/passwd";
                        break;
                    default:
                        result = "Cannot connect to host";
                        break;
                }
                MessageBox.Show("Exception: " + ex.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if(txtDbName.Text == "" )
            {
                MessageBox.Show("Please fill in the empty fields!", "Some fields are missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            updateConnectionString();

            updateDictionaryEvent();

            config.saveDictionaryToFile(globalDictionary);
            isOKButtonClicked = true;
            this.Close();
        }

        //private void btnImageDirSelect_Click(object sender, EventArgs e)
        //{
        //    using (var fbd = new FolderBrowserDialog())
        //    {
        //        DialogResult result = fbd.ShowDialog();

        //        if (result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
        //        {
        //            //txtImageDir.Text = fbd.SelectedPath;
        //        }
        //    }

        //    updateDictionaryEvent();
        //}

        //private void txtUrl_TextChanged(object sender, EventArgs e)
        //{
        //    updateDictionaryEvent();
        //}
        //private void txtImageDir_TextChanged(object sender, EventArgs e)
        //{
        //    updateDictionaryEvent();
        //}

        private void btnTestDb_Click(object sender, EventArgs e)
        {
            lblDbStatus.Text = "Connecting...";

            if (dbConnectionTester.IsBusy != true)
            {
                dbConnectionTester.RunWorkerAsync();

                if (progressbarWorker.WorkerSupportsCancellation == true)
                {
                    progressbarWorker.CancelAsync();
                }

                btnTestDb.Enabled = false;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                try
                {
                    progressbarWorker.RunWorkerAsync();
                }
                catch { }
            }
        }
        private void dbConnectionTester_DoWork(object sender, DoWorkEventArgs e)
        {
            string result = testConnection();
            e.Result = result;
        }

        private void dbConnectionTester_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnTestDb.Enabled = true;
            progressBar1.Visible = false;
            progressBar1.Value = 0;

            if (progressbarWorker.WorkerSupportsCancellation == true)
            {
                progressbarWorker.CancelAsync();
            }

            if (e.Error == null)
            {
                lblDbStatus.Text = (string)e.Result;
            }
        }

        private void progressbarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i <= 100; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(150);
                    worker.ReportProgress(i);
                }
            }
        }

        private void progressbarWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = Int32.Parse(e.ProgressPercentage.ToString());
        }

        private void txtDbIP_TextChanged(object sender, EventArgs e)
        {
            updateConnectionString();
        }

        private void timerValue_ValueChanged(object sender, EventArgs e)
        {
            timeoutTiki = (int)timerValueTiki.Value;
        }

        private void timerValueOtherWebsite_ValueChanged(object sender, EventArgs e)
        {
            timeoutOtherWebsite = (int)timerValueOtherWebsite.Value;
        }
    }
}
