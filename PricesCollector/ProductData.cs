using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        public int discountPrice = 0;
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

        public void populateDataFromTikiLinkVersion2()
        {

            ////the url of the page we want to test
            //var url = "https://tiki.vn/totolink-n150usm-usb-wifi-chuan-n-toc-do-150mbps-p481696.html?spid=9799612";
            //var httpClient = new HttpClient();
            //var html = httpClient.GetStringAsync(url);


            string urlAddress = this.link;
            string html="";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    html = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                else
                {
                    Console.WriteLine("LINK ERROR: " + response.StatusCode.ToString() + " : " + this.link);
                    this.sellerName = "### LINK ERROR, Code: " + response.StatusCode.ToString();
                    return;
                }
            }
            catch
            {
                    Console.WriteLine("LINK ERROR: Code: 404 - "  + this.link);
                    this.sellerName = "### LINK ERROR, Code: 404 Not Found";
                    return;
            }
            

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Parse the discount-container
            try
            {
                var discountHtmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='discount-container']");
                var subNode = discountHtmlNode.SelectSingleNode("//span[@class='price']");
                if (subNode != null)
                {
                    string discountValue = subNode.InnerHtml;
                    Regex digitsOnly = new Regex(@"[^\d]");
                    discountValue = digitsOnly.Replace(discountValue, "");
                    this.discountPrice = Int32.Parse(discountValue);
                }
            }
            catch { }

            //Parse the Javascript values
            var javascriptGroups = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "script");

            string lines = "";
            foreach (var group in javascriptGroups)
            {
                if (group.InnerText.Contains("currentSeller"))
                {
                    lines = group.InnerText;
                    Regex rx = new Regex(@"var (\w+)\s*=\s*(.*);", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    // Find matches.
                    MatchCollection matches = rx.Matches(lines);

                    // Report on each match.
                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        try
                        {
                            JObject json = JObject.Parse("{value :" + groups[2].Value + "}");

                            string jsVarName = groups[1].Value;
                            JToken jsVarValue = json["value"];
                            switch (jsVarName)
                            {
                                case "currentSeller":
                                    this.sku = jsVarValue["sku"].ToString();
                                    this.sellerName = jsVarValue["name"].ToString();
                                    break;
                                case "otherSeller":
                                    this.otherSeller.Clear();
                                    foreach (var obj in jsVarValue)
                                    {
                                        Product p = new Product();
                                        p.name = (string)obj["name"];
                                        p.price = Int32.Parse((string)obj["price"]);
                                        this.otherSeller.Add(p);
                                    }
                                    break;
                                case "price":
                                    this.currentPrice = Int32.Parse(jsVarValue.ToString());
                                    break;
                                case "name":
                                    this.productName = jsVarValue.ToString();
                                    break;
                                case "listPrice":
                                    break;
                                case "defaultProduct":
                                    break;
                                case "stockItem":
                                    break;
                                default: break;
                            }
                        }
                        catch {}
                    }
                    break; // Found "currentSeller"
                }
            }
        }

        public void populateDataFromTikiLinkVersion1()
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
                this.discountPrice = Int32.Parse(listPrice);
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
