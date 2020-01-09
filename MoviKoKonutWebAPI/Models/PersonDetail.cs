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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class PersonDetail
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is settable to make mapping easier.")]
        public ICollection<string> EmailAddresses { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is settable to make mapping easier.")]
        public ICollection<AddressInfo> Addresses { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is settable to make mapping easier.")]
        public ICollection<CreditCardInfo> CreditCards { get; set; }
    }
}