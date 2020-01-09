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
    using System.Collections.Generic;
    using MoviKokonut.Domain.Catalog;

    public interface IProductRecommendationRepository
    {
        ICollection<RecommendedProduct> GetProductRecommendations(int productId);
    }
}