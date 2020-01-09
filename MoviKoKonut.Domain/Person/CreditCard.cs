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
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{13,16}$")]
        public string CardNumber { get; set; }

        [Required]
        public string CardType { get; set; }
        
        [Required]
        [Range(1, 12)]
        public byte ExpMonth { get; set; }

        [Required]
        [Range(2013, 2099)]
        public short ExpYear { get; set; }
    }
}