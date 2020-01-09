using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKokonut.Domain.Catalog
    {
    public class SellerProductShortInventory
        {
        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public long Quantity { get; set; }
        public double SellerPrice { get; set; }
        }
    }
