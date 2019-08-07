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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = (int)timerValue.Value * 1000 * 60;

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

        public string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            StringBuilder builder = new StringBuilder(input);
            builder[index] = newChar;
            return builder.ToString();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {



            populateDatagridview();
        }

        void populateDatagridview()
        {
            dataGridView1.Rows.Clear();
            int productIndex = 0;
            foreach (var lk in txtUrl.Text.Split('\n'))
            {
                string link = lk.Trim();
                if (link == "" && !link.Contains("tiki.vn")) continue;

                Console.WriteLine(link);

                ProductData data = getDataFromTikiLink(link);
                data.link = link;
                data.productId = productIndex;
                productIndex++;
                datagridview(data);
            }
        }

        

        private ProductData getDataFromTikiLink(string link)
        {
            ProductData data = new ProductData();

            string currentSellerLine = "";
            string productName = "";
            string otherSellerLine = "";

            try
            {
                var client = new WebClient();
                var stream = client.OpenRead(link);

                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("currentSeller"))
                        {
                            currentSellerLine = line;

                        }
                        if (line.Contains("otherSeller"))
                        {
                            otherSellerLine = line;

                        }
                        if (line.Contains("var name ="))
                        {
                            productName = line.Replace("var name =", "").Trim();
                            productName = productName.Replace("\"", "");
                        }

                        if (currentSellerLine != "" && otherSellerLine != "" && productName != "")
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                return data;
            }

            JObject currentSellerJson = convertTikiDataToJson(currentSellerLine);
            JObject otherSellerJson = convertTikiDataToJson(otherSellerLine);

            try
            {
                data.storeName = (string)currentSellerJson["currentSeller"]["name"];
                data.productName = productName;
                data.currentPrice = Int32.Parse((string)currentSellerJson["currentSeller"]["price"]);
                data.sku = (string)currentSellerJson["currentSeller"]["sku"];

                foreach (var obj in otherSellerJson["otherSeller"])
                {
                    Product p = new Product();
                    p.name = (string)obj["name"];
                    p.price = Int32.Parse((string)obj["price"]);
                    data.otherSeller.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return data;
        }

        private void datagridview(ProductData data)
        {
            string[] row = new string[] { data.productId.ToString(), data.productName, data.sku, data.storeName, data.currentPrice.ToString() };
            dataGridView1.Rows.Add(row);


            //DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();

            //cmb.HeaderText = "Other Seller";
            //cmb.Name = "cmb";
            //cmb.MaxDropDownItems = 4;

            List<Product> list = data.SortedList();

            DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dataGridView1.Rows[data.productId].Cells[6];
            foreach (var i in list)
            {
                cmb.Items.Add(i.price + ": " + i.name);
            }

            try
            {

                dataGridView1.Rows[data.productId].Cells[5].Value = list[0].price.ToString();

                if (list[0].price < data.currentPrice)
                {
                    dataGridView1.Rows[data.productId].Cells[5].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[data.productId].Cells[5].Style.BackColor = Color.Green;
                }
            }
            catch { }

            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.HeaderText = "Click Data";
            //btn.Text = "Click Here";
            //btn.Name = "btn";
            //btn.UseColumnTextForButtonValue = true;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            {
                //MessageBox.Show((e.RowIndex) + "  Row  " + (e.ColumnIndex) + "  Column button clicked ");
            }
        }

        private JObject convertTikiDataToJson(string raw)
        {
            JObject jsonObject = new JObject();

            string jsonStr = raw.Trim();

            //head
            jsonStr = jsonStr.Replace("var ", "{");
            int pos = jsonStr.IndexOf("=");
            jsonStr = ReplaceAt(jsonStr, pos, ':');

            //tail
            pos = jsonStr.LastIndexOf(";");
            if (pos >= 0)
            {
                jsonStr = jsonStr.Remove(pos);
            }
            jsonStr += "}";

            try
            {
                jsonObject = JObject.Parse(jsonStr);
            }
            catch { }

            return jsonObject;
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
