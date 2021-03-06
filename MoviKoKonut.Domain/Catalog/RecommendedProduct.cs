﻿//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Catalog
{
    public class RecommendedProduct
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Percentage { get; set; }
    }
}