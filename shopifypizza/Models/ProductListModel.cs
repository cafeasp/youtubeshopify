using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopifypizza.Models
{
    public class ProductListModel
    {
        public ProductListModel() { Products = new List<ProductModel>(); }
        public int Count { get; set; }
        public List<ProductModel> Products { get; set; }
    }

    public class ProductModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }

    }
}