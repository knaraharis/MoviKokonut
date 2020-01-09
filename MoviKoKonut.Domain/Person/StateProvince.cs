//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonut.Domain.Person
{
    public class StateProvince
    {
        public int StateProvinceId { get; set; }

        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }
    }
}