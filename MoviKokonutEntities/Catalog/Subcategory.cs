//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Domain.Catalog
{
    public class Subcategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
