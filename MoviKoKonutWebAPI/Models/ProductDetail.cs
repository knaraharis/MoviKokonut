//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonutWebAPI.Models
{
    public class ProductDetail : ProductInfo
    {
        public string ProductNumber { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public string SizeUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public string WeightUnitMeasureCode { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }
    }
}