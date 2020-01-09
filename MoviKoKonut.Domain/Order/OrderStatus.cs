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
    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        PaymentUncleared = 2,
        PaymentCleared = 3,
        Dispatched = 4,
        Completed = 5,
        Canceled = 6,
        AwaitingStock = 7,
        InternalError = 8
    }
}
