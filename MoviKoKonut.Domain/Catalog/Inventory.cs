using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKoKonut.Domain.Catalog
    {
    public class Inventory
        {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public long Quantity { get; set; }
        public double SellerPrice { get; set; }
        }
    }
