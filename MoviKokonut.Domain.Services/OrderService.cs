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
    using MoviKokonut.Domain.Order;
    using MoviKokonut.Domain.Services.Interface;
    using MoviKokonut.Repository;

    // For a detailed description on how to manage cross-database consistency,
    // see the documentation  "How the Shopping Application Creates an Order"
    // on http://go.microsoft.com/fwlink/p/?LinkID=317537 
    public sealed class OrderService : IOrderService
    {
        private readonly IOrderHistoryRepository orderHistoryRepository;
        private readonly IOrderQueue orderQueue;

        public OrderService(
            IOrderHistoryRepository orderHistoryRepository,
            IOrderQueue orderQueue)
        {
            this.orderHistoryRepository = orderHistoryRepository;
            this.orderQueue = orderQueue;
        }

        public void ProcessOrder(Order newOrder)
        {
            if (newOrder == null)
            {
                throw new ArgumentNullException("newOrder");
            }

            if (newOrder.Status != OrderStatus.Pending)
            {
                // No need to do anything if the order is not Pending.
                return;
            }

            // Save the order to history as Pending to make sure we capture the order
            this.orderHistoryRepository.SaveOrderHistory(new OrderHistory(newOrder));

            // At this point you have everything you need from the requestor. What you can do next 
            // is to send a message with the trackingId to a queue.
            this.orderQueue.Send(newOrder.TrackingId);
        }

        public void ProcessOrphanPendingOrders()
        {
            // In a real background process, there should be a task that would find orphaned
            // orders in the order history database that are Pending, but a message was never successfully
            // sent to the queue. Each of those a messages should be automatically added to the
            // queue, so it can be processed. This process may not need to run very frequently, 
            // as the normal operation dictates that orphaned orders will not appear very frequently either.
        }
    }
}
