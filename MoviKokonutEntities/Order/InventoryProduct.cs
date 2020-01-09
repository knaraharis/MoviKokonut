//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Domain.Order
{
    public class InventoryProduct
    {
        public int ProductId { get; set; }

        public decimal ListPrice { get; set; }
    }
}