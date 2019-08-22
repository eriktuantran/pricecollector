using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesCollector
{
    class MyRow
    {
        public bool isLowest = false;
        public DataRow row;
    };

    class Utilities
    {
        static public string getColName(string key)
        {
            switch(key)
            {
                case "id":
                    return "Mã số";
                case "product_sync_code":
                    return "Mã đồng bộ";
                case "product_group":
                    return "Nhóm";
                case "seller_name":
                    return "Tên Seller";
                case "product_name":
                    return "Tên sản phẩm";
                case "product_code":
                    return "Mã sản phẩm";
                case "sku":
                case "msku":
                    return key.ToUpper();
                case "active":
                    return "Trạng thái";
                case "current_price":
                    return "Giá bán";
                case "minimum_price":
                    return "Giá bán thấp nhất";
                case "lowest_price":
                    return "Giá bán thấp nhất (từ nhà cung cấp khác)";
                case "discount_price":
                    return "Giảm giá";
                case "other_seller":
                    return "Nhà cung cấp khác";
                case "link_tiki":
                    return "Link Tiki";
                case "link_lazada":
                    return "Link Lazada";

                default: return key;
            }
        }
    }
}
