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
    public class OrderProcessor : IOrderProcessor
    {
        private readonly ISalesOrderRepository orderRepository;
        private readonly IOrderHistoryRepository orderHistoryRepository;

        public OrderProcessor(
            ISalesOrderRepository orderRepository,
            IOrderHistoryRepository orderHistoryRepository)
        {
            this.orderRepository = orderRepository;
            this.orderHistoryRepository = orderHistoryRepository;
        }

        public void Execute(Guid trackingId)
        {
            this.SaveOrderAndSaveOrderHistory(trackingId);
        }

        // The eventual consistency for writes is implemented in the following way:
        // 1. We are writing to two different databases (order db and order history db) by calling the SaveOrderAndSaveOrderHistory method.
        // 2. Both writes need to be successful to achieve consistency.
        // 3. Before each write, we do a read to find out if the record is already in the DB. If the record already exists, we don't write.
        // 4. If write fails, we will try to write again some time in the future by calling the SaveOrderAndSaveOrderHistory method.
        // 5. The SaveOrderAndSaveOrderHistory method is idempotent.
        // 6. After a certain number of failed writes, we will resort to manual intervention to fix the issue.
        private void SaveOrderAndSaveOrderHistory(Guid trackingId)
        {
            // This method writes to two diferent databases. both writes need to succeed to achieve consistency. 
            // This method is idempotent. 
            bool isOrderSavedInOrderRepo = this.orderRepository.IsOrderSaved(trackingId);

            bool isOrderSavedInOrderHistoryRepoAsCompleted = this.orderHistoryRepository.IsOrderCompleted(trackingId);

            if (!isOrderSavedInOrderRepo || !isOrderSavedInOrderHistoryRepoAsCompleted)
            {
                var newOrder = this.orderHistoryRepository.GetPendingOrderByTrackingId(trackingId);

                if (newOrder != null)
                {
                    if (!isOrderSavedInOrderRepo)
                    {
                        // Once the following orderRepository.SaveOrder method is completed successfully, the flag 
                        // isOrderSavedInOrderRepo should return true the next time the SaveOrderAndSaveOrderHistory 
                        // method gets called for the same trackingId.
                        this.orderRepository.SaveOrder(newOrder);
                    }

                    if (!isOrderSavedInOrderHistoryRepoAsCompleted)
                    {
                        newOrder.Status = OrderStatus.Completed;

                        // Once the following SaveOrderHistory method is completed successfully, the flag 
                        // isOrderSavedInOrderHistoryRepoAsCompleted should return true the next time 
                        // the SaveOrderAndSaveOrderHistory method gets called for the same trackingId.
                        this.orderHistoryRepository.SaveOrderHistory(new OrderHistory(newOrder));
                    }
                }
            }
        }
    }
}
