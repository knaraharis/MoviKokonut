//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Services.Interface
{
    using MoviKokonut.Domain;

    public interface IOrderService
    {
        void ProcessOrder(Order.Order newOrder);

        void ProcessOrphanPendingOrders();
    }
}