using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
            string expireStr = "2019-07-30";
            DateTime expireDate = Convert.ToDateTime(expireStr);

            if(DateTime.Now > expireDate)
            {
                MessageBox.Show("Trial version expired!");
                this.Close();
                return;
            }

            if(!txtUrl.Text.Contains("tiki.vn"))
            {
                MessageBox.Show("Only support tiki.vn!");
                txtOutput.Text = "";
                return;
            }

            string selectedLine = "";
            string price = "";

            using (var client = new WebClient())
            using (var stream = client.OpenRead(txtUrl.Text))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("otherSeller"))
                    {
                        selectedLine = line;
                        break;
                    }
                }
            }

            string jsonStr = selectedLine.Trim();

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
                JObject jsonObject = JObject.Parse(jsonStr);

                Console.WriteLine(jsonObject);

                foreach (var obj in jsonObject["otherSeller"])
                {
                    Console.WriteLine( obj["link"] );
                    price += obj["name"] + ":\n";
                    price += obj["price"] + " VND\n";
                    price += "\n";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            txtOutput.Text = price;
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            txtOutput.Text = "";
        }
    }
}
