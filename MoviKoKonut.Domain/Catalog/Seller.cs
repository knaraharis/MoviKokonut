using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKokonut.Domain.Catalog
    {
    public class Seller : MoviKokonutDocument
        {
        public string SellerId { get; set; }
        public Contact sellertContactDetails { get; set; }
        public AccountDetails SellerAccountDetails { get; set; }
        }
    }
