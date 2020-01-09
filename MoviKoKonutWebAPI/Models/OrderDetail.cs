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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MoviKokonut.Domain.Order;
    
    public class OrderDetail
    {
        public Guid TrackingId { get; set; }

        public int CustomerId { get; set; }

        public AddressInfo BillingAddress { get; set; }

        public AddressInfo ShippingAddress { get; set; }

        public CreditCardInfo CreditCard { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Freight { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemInfo> OrderItems { get; set; }
    }
}