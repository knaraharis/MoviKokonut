using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKokonut.Domain.Catalog
    {
    public class ProductAdvertisement : MoviKokonutDocument
        {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double XCoOrdinates { get; set; }
        public double YCoOrdinates { get; set; }
        public IEnumerable<SellerProductShortInventory> Seller { get; }
        }
    }
