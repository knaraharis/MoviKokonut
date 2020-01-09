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
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Domain.Catalog;

    public class OrderItem
    {
        [Required]
        public short Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}