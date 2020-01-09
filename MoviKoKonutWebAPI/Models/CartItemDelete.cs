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
    public class CartItemDelete
    {
        public string ShoppingCartId { get; set; }

        public int ProductId { get; set; }
    }
}