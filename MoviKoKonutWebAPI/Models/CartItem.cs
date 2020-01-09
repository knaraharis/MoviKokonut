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
    using System.ComponentModel.DataAnnotations;
    
    public class CartItem
    {
        public string ShoppingCartId { get; set; }

        [Range(1, 99)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string CheckoutErrorMessage { get; set; }
    }
}