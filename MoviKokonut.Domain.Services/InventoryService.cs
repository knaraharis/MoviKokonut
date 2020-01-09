//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Services
{
    using System;
    using System.Globalization;
    using MoviKokonut.Domain.Services.Interface;
    using MoviKokonut.Domain.Services.Resources;
    using MoviKokonut.Repository;

    public class InventoryService : IInventoryService
    {
        private readonly IInventoryProductRepository inventoryProductRepository;

        public InventoryService(IInventoryProductRepository inventoryProductRepository)
        {
            this.inventoryProductRepository = inventoryProductRepository;
        }

        public bool InventoryAndPriceCheck(ShoppingCart.ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart");
            }

            bool hasInventoryPriceErrors = false;
            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                var inventoryProduct = this.inventoryProductRepository.GetInventoryProduct(shoppingCartItem.ProductId);
                if (inventoryProduct == null)
                {
                    shoppingCartItem.CheckoutErrorMessage += string.Format(CultureInfo.CurrentCulture, Strings.DiscontinuedProduct) +
                        Environment.NewLine;
                    hasInventoryPriceErrors = true;
                }
                else if (!string.IsNullOrWhiteSpace(shoppingCartItem.CheckoutErrorMessage))
                {
                    shoppingCartItem.CheckoutErrorMessage = string.Empty;
                }

                if (shoppingCartItem.ProductPrice != inventoryProduct.ListPrice)
                {
                    shoppingCartItem.CheckoutErrorMessage += string.Format(
                        CultureInfo.CurrentCulture,
                        Strings.ProductPriceHasChanged,
                        shoppingCartItem.ProductPrice,
                        inventoryProduct.ListPrice);
                    shoppingCartItem.CheckoutErrorMessage += Environment.NewLine;
                    shoppingCartItem.ProductPrice = inventoryProduct.ListPrice;
                    hasInventoryPriceErrors = true;
                }
                else if (!string.IsNullOrWhiteSpace(shoppingCartItem.CheckoutErrorMessage))
                {
                    shoppingCartItem.CheckoutErrorMessage = string.Empty;
                }

                // There would be many other checks to validate invetory quanity vs. the quanity in the shopping cart
                // as well as locking the quanity during the checkout process, but this is beyond the scope of this
                // guidance and depends on the needs of the business and domain.
            }

            return hasInventoryPriceErrors;
        }
    }
}