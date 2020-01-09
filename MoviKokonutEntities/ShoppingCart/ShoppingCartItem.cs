//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Domain.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartItem
    {
        [Range(1, 99)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public string CheckoutErrorMessage { get; set; }
    }
}
