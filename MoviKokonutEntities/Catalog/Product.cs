//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Domain.Catalog
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public string Color { get; set; }

        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        public string SizeUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public string WeightUnitMeasureCode { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public Subcategory Subcategory { get; set; }
    }
}
