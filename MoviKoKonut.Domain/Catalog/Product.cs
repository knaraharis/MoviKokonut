//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKokonut.Domain.Catalog
{
    public class Product : MoviKokonutDocument
        {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        public string SizeUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public string WeightUnitMeasureCode { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public Subcategory Subcategory { get; set; }
        public IEnumerable<SellerProductShortInventory> SellerProductsInventory { get; set; }
        }
}
