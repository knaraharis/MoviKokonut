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

    public class OrderHistoryInfo : OrderDetail
    {
        public Guid HistoryId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Status { get; set; }
    }
}