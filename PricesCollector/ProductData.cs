using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PricesCollector
{
    class Product
    {
        public int price = 0;
        public string name = "";
    }
    class ProductData
    {
        public string link = "";
        public bool isActive = true;

        public int productId = 0;
        public string productName = "";
        public int currentPrice = 0;
        public int listPrice = 0;
        public string sellerName = "";
        public string sku = "";

        public List<Product> otherSeller = new List<Product>();

        public List<Product> SortedList()
        {
            List<Product> SortedList = otherSeller.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public int lowestPrice
        {
            get
            {
                try
                {
                    List<Product> otherSellerList = this.SortedList();
                    if(otherSellerList.Count==0)
                    {
                        return 0;
                    }
                    return otherSellerList[0].price;
                }
                catch
                {
                    return 0;
                }
            }
            
        }

        public void populateDataFromTikiLink()
        {
            string currentSellerLine = "";
            string productName = "";
            string otherSellerLine = "";
            string listPrice = "0";

            string tempPrice = "0";

            try
            {
                var client = new WebClient();
                var stream = client.OpenRead(this.link);

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
                        if (line.Contains("var listPrice ="))
                        {
                            listPrice = line.Replace("var listPrice =", "").Trim();
                            listPrice = listPrice.Replace("\"", "");
                            listPrice = listPrice.Replace(";", "");
                        }

                        if (line.Contains("var price ="))
                        {
                            tempPrice = line.Replace("var price =", "").Trim();
                            tempPrice = tempPrice.Replace("\"", "");
                            tempPrice = tempPrice.Replace(";", "");
                        }

                        if (currentSellerLine != "" && otherSellerLine != "" && productName != "" && listPrice != "" && tempPrice != "")
                        {
                            break;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("LINK ERROR: " + this.link);

                this.sellerName = "### LINK ERROR";
                return;
            }

            JObject currentSellerJson = convertTikiDataToJson(currentSellerLine);
            JObject otherSellerJson = convertTikiDataToJson(otherSellerLine);

            try
            {
                this.sellerName = (string)currentSellerJson["currentSeller"]["name"];
                this.productName = productName;
                try
                {
                    string priceStr = (string)currentSellerJson["currentSeller"]["price"];
                    if (priceStr.Trim() != "")
                    {
                        this.currentPrice = Int32.Parse(priceStr);
                    }
                    else
                    {
                        this.currentPrice = Int32.Parse(tempPrice);
                    }
                }
                catch
                {
                    this.currentPrice = Int32.Parse(tempPrice);
                }
                this.listPrice = Int32.Parse(listPrice);
                this.sku = (string)currentSellerJson["currentSeller"]["sku"];
                this.otherSeller.Clear();

                foreach (var obj in otherSellerJson["otherSeller"])
                {
                    Product p = new Product();
                    p.name = (string)obj["name"];
                    p.price = Int32.Parse((string)obj["price"]);
                    this.otherSeller.Add(p);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }

        private JObject convertTikiDataToJson(string raw)
        {
            JObject jsonObject = new JObject();

            string jsonStr = raw.Trim();

            try
            {
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
            }
            catch { }
            try
            {
                jsonObject = JObject.Parse(jsonStr);
            }
            catch { }

            return jsonObject;
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

        public void dump()
        {
            Console.WriteLine("currentName : {0} : price: {1}", sellerName, currentPrice);
            foreach (var d in otherSeller)
            {
                Console.WriteLine("Product name: {0} : price: {1}", d.name, d.price);
            }
        }

    }
}
