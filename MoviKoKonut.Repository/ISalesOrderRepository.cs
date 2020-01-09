//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Repository
{
    using System;
    using MoviKokonut.Domain.Order;

    public interface ISalesOrderRepository
    {
        Order SaveOrder(Order order);

        bool UpdateOrderStatus(Guid trackingId, OrderStatus newStatus);

        bool IsOrderSaved(Guid trackingId);
    }
}
