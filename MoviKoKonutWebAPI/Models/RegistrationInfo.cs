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
using System.ComponentModel.DataAnnotations;

    public class RegistrationInfo
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public IEnumerable<string> EmailAddresses { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public IEnumerable<AddressInfo> Addresses { get; set; }

        [Required]
        public IEnumerable<CreditCardInfo> CreditCards { get; set; }
    }
}