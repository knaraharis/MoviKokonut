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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using MoviKokonut.Domain.Person;

    public class Order
    {
        private readonly IList<OrderItem> orderItems = new List<OrderItem>();

        [Required]
        public Guid TrackingId { get; set; }
        
        [Required]
        public Address BillToAddress { get; set; }

        [Required]
        public CreditCard CreditCard { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public decimal Freight { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems
        {
            get { return new ReadOnlyCollection<OrderItem>(this.orderItems); }
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem");
            }

            Validator.ValidateObject(orderItem, new ValidationContext(orderItem), true);

            this.orderItems.Add(orderItem);
        }
    }
}