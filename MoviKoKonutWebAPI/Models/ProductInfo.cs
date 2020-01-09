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
    public class ProductInfo : ModelBase
    {
        public decimal ListPrice { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }
    }
}