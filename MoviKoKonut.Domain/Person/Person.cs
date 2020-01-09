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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using MoviKokonut.Domain.Resources;

    public class Person
    {
        private readonly IList<string> emailAddresses = new List<string>();
        private readonly IList<Address> addresses = new List<Address>();
        private readonly IList<CreditCard> creditCards = new List<CreditCard>();

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public Guid PersonGuid { get; set; }

        public IReadOnlyCollection<string> EmailAddresses
        {
            get { return new ReadOnlyCollection<string>(this.emailAddresses); }
        }

        public IReadOnlyCollection<Address> Addresses
        {
            get { return new ReadOnlyCollection<Address>(this.addresses); }
        }

        public IReadOnlyCollection<CreditCard> CreditCards
        {
            get { return new ReadOnlyCollection<CreditCard>(this.creditCards); }
        }

        public void AddEmailAddress(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ParameterCannotBeNullEmptyOrOnlyWhiteSpace, "emailAddress"));
            }

            var regex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$");
            var match = regex.Match(emailAddress);
            if (!match.Success)
            {
                throw new ValidationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidEmailAddress, emailAddress));
            }

            this.emailAddresses.Add(emailAddress);
        }

        public void AddAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Strings.ParameterCannotBeNull, "address"));
            }

            Validator.ValidateObject(address, new ValidationContext(address), true);

            this.addresses.Add(address);
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Strings.ParameterCannotBeNull, "creditCard"));
            }

            Validator.ValidateObject(creditCard, new ValidationContext(creditCard), true);

            this.creditCards.Add(creditCard);
        }
    }
}