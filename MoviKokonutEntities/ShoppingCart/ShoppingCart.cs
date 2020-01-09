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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();

        public ShoppingCart(string userCartId)
        {
            this.UserCartId = userCartId;
            this.TrackingId = Guid.NewGuid();
        }

        public string UserCartId { get; set; }

        public IReadOnlyCollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return new ReadOnlyCollection<ShoppingCartItem>(this.shoppingCartItems); }
        }

        public Guid TrackingId { get; set; }

        public void AddItem(ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
            {
                throw new ArgumentNullException("shoppingCartItem");
            }

            Validator.ValidateObject(shoppingCartItem, new ValidationContext(shoppingCartItem), true);

            // first check to see if the item already exists in the cart; if so just increment the quantity
            var existingItem = this.shoppingCartItems
                .FirstOrDefault(c => c.ProductId == shoppingCartItem.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += shoppingCartItem.Quantity;
            }
            else
            {
                this.shoppingCartItems.Add(shoppingCartItem);
            }
        }

        public void RemoveProduct(int productId)
        {
            this.shoppingCartItems.Remove(this.shoppingCartItems.FirstOrDefault(c => c.ProductId == productId));
        }
    }
}