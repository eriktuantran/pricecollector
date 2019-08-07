using System;
using System.Collections.Generic;
using System.Linq;
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

        public int productId = 0;
        public string productName = "";
        public int currentPrice = 0;
        public string storeName = "";
        public string sku = "";

        public List<Product> otherSeller = new List<Product>();

        public List<Product> SortedList()
        {
            List<Product> SortedList = otherSeller.OrderBy(o => o.price).ToList();
            return SortedList;
        }

        public void dump()
        {
            Console.WriteLine("currentName : {0} : price: {1}", storeName, currentPrice);
            foreach (var d in otherSeller)
            {
                Console.WriteLine("Product name: {0} : price: {1}", d.name, d.price);
            }
        }

    }
}
