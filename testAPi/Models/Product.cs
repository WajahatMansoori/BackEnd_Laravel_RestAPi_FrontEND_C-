using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testAPi.Models
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}