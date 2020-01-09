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
    public class OrderInfo
    {
        public string CartId { get; set; }

        public int ShippingAddressId { get; set; }

        public int BillingAddressId { get; set; }

        public int CreditCardId { get; set; }
    }
}