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
    using System.Collections.Generic;
    using MoviKokonut.Domain.Person;

    public interface IStateProvinceRepository
    {
        StateProvince GetStateProvince(int stateProvinceId);

        ICollection<StateProvince> GetStateProvinces();
    }
}