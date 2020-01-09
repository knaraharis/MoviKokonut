//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Order
{
    using System;

    public class OrderHistory : Order
    {
        public OrderHistory()
        {
            this.HistoryId = Guid.NewGuid();
        }

        public OrderHistory(Order order) 
            : this()
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            this.TrackingId = order.TrackingId;
            this.BillToAddress = order.BillToAddress;
            this.CreditCard = order.CreditCard;
            this.CustomerId = order.CustomerId;
            this.DueDate = order.DueDate;
            this.Freight = order.Freight;
            this.OrderDate = order.OrderDate;
            this.ShippingAddress = order.ShippingAddress;
            this.Status = order.Status;

            foreach (var orderItem in order.OrderItems)
            {
                this.AddOrderItem(orderItem);
            }

            var orderHistory = order as OrderHistory;
            if (orderHistory != null)
            {
                this.HistoryId = orderHistory.HistoryId;
                this.ModifiedDate = orderHistory.ModifiedDate;
            }
            else
            {
                this.ModifiedDate = DateTime.UtcNow;
            }
        }

        public Guid HistoryId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
