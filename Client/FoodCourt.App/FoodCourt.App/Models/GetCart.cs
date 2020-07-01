using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCourt.Models
{
    public class GetCart
    {
        public int id { get; set; }
        public double price { get; set; }
        public double totalAmount { get; set; }
        public int qty { get; set; }
        public string productName { get; set; }
    }
}
