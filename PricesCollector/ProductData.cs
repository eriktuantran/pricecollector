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
        public bool isFullFetching = false;

        public string linkTiki = "";
        public bool isActive = true;

        public int productId = 0;
        public string productName = "";
        public int currentPrice = 0;
        public int discountPrice = 0;
        public string sellerName = "";
        public string sku = "";

        public string linkLazadaRaw = "";
        public string linkShopeeRaw = "";
        public string linkSendoRaw = "";
        public List<Product> otherSellerLazada = new List<Product>();
        public List<Product> otherSellerShopee = new List<Product>();
        public List<Product> otherSellerSendo = new List<Product>();


        public List<Product> otherSellerTiki = new List<Product>();


        public List<Product> SortedListTiki()
        {
            List<Product> SortedList = otherSellerTiki.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public List<Product> SortedListLazada()
        {
            List<Product> SortedList = otherSellerLazada.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public List<Product> SortedListShopee()
        {
            List<Product> SortedList = otherSellerShopee.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public List<Product> SortedListSendo()
        {
            List<Product> SortedList = otherSellerSendo.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public int lowestPriceTiki // get the first price of the sorted list
        {
            get
            {
                try
                {
                    List<Product> otherSellerList = this.SortedListTiki();
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

        public int lowestPriceLazada
        {
            get
            {
                try
                {
                    List<Product> otherSellerList = this.SortedListLazada();
                    if (otherSellerList.Count == 0)
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

        public int lowestPriceShopee
        {
            get
            {
                try
                {
                    List<Product> otherSellerList = this.SortedListShopee();
                    if (otherSellerList.Count == 0)
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

        public int lowestPriceSendo
        {
            get
            {
                try
                {
                    List<Product> otherSellerList = this.SortedListSendo();
                    if (otherSellerList.Count == 0)
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

        private Product geProductDataFromLazada(string link)
        {
            Product product = new Product();
            product.price = 0;
            product.name = "Lazada";

            string html = getHtmlFromWebsite(link);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Parse the Javascript values
            var javascriptGroups = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "script");

            foreach (var group in javascriptGroups)
            {
                if (group.InnerText.Contains("priceCurrency"))
                {
                    JObject json = JObject.Parse(group.InnerText);
                    JToken productPrice = json["offers"]["price"];
                    JToken sellerName = json["offers"]["seller"]["name"];

                    product.price = Int32.Parse(productPrice.ToString());
                    product.name = sellerName.ToString();

                    break;
                }
                else
                {
                    continue;
                }
            }

            return product;
        }

        private Product geProductDataFromShopee(string link)
        {
            Product product = new Product();

            product.name = "Shopee";
            product.price = 0;


            string html = getHtmlFromWebsite(link);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            foreach(var line in html.Split('\n'))
            {
                if(line.Contains("class='price'"))
                {
                    Regex rx = new Regex(@"<div class=.price.*emprop=.offers.*itemscope.[\s\S]+content=.(\d+)\.\d+.\/><link",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    // Find matches.
                    MatchCollection matches = rx.Matches(line);

                    foreach (Match match in matches)
                    {
                        try
                        {

                            GroupCollection groups = match.Groups;
                            string productPrice = groups[1].Value;
                            product.price = Int32.Parse(productPrice);
                        }
                        catch
                        { }
                    }
                }
            }

            return product;
        }

        private Product geProductDataFromSendo(string link)
        {
            Product product = new Product();

            product.name = "Sendo";
            product.price = 0;

            
            string html = getHtmlFromWebsite(link);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            //Parse the Javascript values
            var javascriptGroups = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "script");

            foreach (var group in javascriptGroups)
            {
                if (group.InnerText.Contains("ProductBasic"))
                {
                    string textToFind = "__INITIAL_STATE__=";
                    int index = group.InnerText.IndexOf(textToFind);
                    if (index != -1)
                    {

                        string jsonStr = group.InnerText.Substring(index);
                        jsonStr = jsonStr.Replace(textToFind, "");
                        
                        JObject json = JObject.Parse(jsonStr);

                        JToken token = json["@"]["data"]["ProductBasic"]["_"];
                        string key = token["__active__"].ToString();

                        try
                        {
                            product.price = Int32.Parse(token[key]["data"]["final_price"].ToString());
                            product.name = token[key]["data"]["shop_info"]["shop_name"].ToString();
                        }
                        catch
                        {
                        }

                        break;
                    }
                }
                else
                {
                    continue;
                }
            }

            return product;

            
        }

        private void populateOtherWebsite()
        {
            this.otherSellerLazada.Clear();
            this.otherSellerShopee.Clear();
            this.otherSellerSendo.Clear();

            foreach (var link in this.linkLazadaRaw.Split('\n'))
            {
                if (link.Trim() == "") continue;
                Console.WriteLine("populateOtherWebsite: Lazada {0}--{1}", this.productId, link);

                Product p = geProductDataFromLazada(link);
                this.otherSellerLazada.Add(p);
            }

            foreach (var link in this.linkShopeeRaw.Split('\n'))
            {
                if (link.Trim() == "") continue;
                Console.WriteLine("populateOtherWebsite: Shopee {0}--{1}", this.productId, link);

                Product p = geProductDataFromShopee(link);
                this.otherSellerShopee.Add(p);
            }

            foreach (var link in this.linkSendoRaw.Split('\n'))
            {
                if (link.Trim() == "") continue;
                Console.WriteLine("populateOtherWebsite: Sendo {0}--{1}", this.productId, link);

                Product p = geProductDataFromSendo(link);
                this.otherSellerSendo.Add(p);
            }

        }

        private string getHtmlFromWebsite(string urlAddress)
        {
            string html = "";

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
                    //Console.WriteLine("LINK ERROR: " + response.StatusCode.ToString() + " : " + this.link);
                    this.sellerName = "### LINK ERROR, Code: " + response.StatusCode.ToString();
                }
            }
            catch
            {
                //Console.WriteLine("LINK ERROR: Code: 404 - "  + this.link);
                this.sellerName = "### LINK ERROR, Code: 404 Not Found";
            }

            return html;
        }

        public void populateDataFromTikiLinkVersion2()
        {
            string urlAddress = this.linkTiki;
            string html = getHtmlFromWebsite(urlAddress);

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
                                    this.otherSellerTiki.Clear();
                                    foreach (var obj in jsVarValue)
                                    {
                                        Product p = new Product();
                                        p.name = (string)obj["name"];
                                        p.price = Int32.Parse((string)obj["price"]);
                                        this.otherSellerTiki.Add(p);
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

            if (this.isFullFetching)
            {
                // Fetch other websites
                populateOtherWebsite();
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
                var stream = client.OpenRead(this.linkTiki);
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
                Console.WriteLine("LINK ERROR: " + this.linkTiki);

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
                this.otherSellerTiki.Clear();

                foreach (var obj in otherSellerJson["otherSeller"])
                {
                    Product p = new Product();
                    p.name = (string)obj["name"];
                    p.price = Int32.Parse((string)obj["price"]);
                    this.otherSellerTiki.Add(p);
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
            foreach (var d in otherSellerTiki)
            {
                Console.WriteLine("Product name: {0} : price: {1}", d.name, d.price);
            }
        }

    }
}
