using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PricesCollector
{
    using MyDictionary = System.Collections.Generic.Dictionary<int, ProductData>;

    public partial class Form1 : Form
    {
        MyDictionary productDict = new MyDictionary();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = (int)timerValue.Value * 1000 * 60;
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
        private void buttonPopulateDictionaryLink_Click(object sender, EventArgs e)
        {
            productDict.Clear();

            int productIndex = 0;
            foreach (var lk in txtUrl.Text.Split('\n'))
            {
                string link = lk.Trim();
                if (link == "" && !link.Contains("tiki.vn")) continue;

                ProductData dat = new ProductData();
                dat.link = link;

                Random r = new Random();
                dat.isActive = r.Next(100) <= 50 ? true : false;

                productDict[productIndex++] = dat;
            }

            foreach (var d in productDict)
            {
                Console.WriteLine(d.Value.link);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string expireStr = "2019-09-30";
            DateTime expireDate = Convert.ToDateTime(expireStr);

            if (DateTime.Now > expireDate)
            {
                MessageBox.Show("Trial version expired!");
                this.Close();
                return;
            }
        }


        private void btnParse_Click(object sender, EventArgs e)
        {
            populateDatagridview();
        }

        void populateDatagridview()
        {
            dataGridView1.Rows.Clear();

            foreach (var d in productDict)
            {
                d.Value.productId = d.Key;
                d.Value.populateDataFromTikiLink();
                Console.WriteLine("Tuan key=[{0}]", d.Key);
            }

            foreach (var d in productDict)
            {
                datagridview(d.Value);
            }

        }

        private void datagridview(ProductData data)
        {
            string[] row = new string[]
            {
                data.productId.ToString(),
                "True",
                data.link,
                data.productName,
                data.sku,
                data.storeName,
                data.currentPrice.ToString()
            };
            dataGridView1.Rows.Add(row);


            //dataGridView1.Rows[data.productId].Cells[column("Lowest")].Value = list[0].price.ToString();

            DataGridViewCheckBoxCell checkBox = (DataGridViewCheckBoxCell)dataGridView1.Rows[data.productId].Cells[column("Active")];
            

            try
            {
                List<Product> list = data.SortedList();
                DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dataGridView1.Rows[data.productId].Cells[column("OtherSeller")];
                foreach (var i in list)
                {
                    cmb.Items.Add(i.price + ": " + i.name);
                }

                dataGridView1.Rows[data.productId].Cells[column("Lowest")].Value = list[0].price.ToString();

                if (list[0].price < data.currentPrice)
                {
                    dataGridView1.Rows[data.productId].Cells[column("Lowest")].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[data.productId].Cells[column("Lowest")].Style.BackColor = Color.Green;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed at lowest price");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            {
                //MessageBox.Show((e.RowIndex) + "  Row  " + (e.ColumnIndex) + "  Column button clicked ");
            }
        }


        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer Tick");
            populateDatagridview();
        }

        private void timerValue_ValueChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = (int)timerValue.Value * 1000 * 60;
        }


    }
}
