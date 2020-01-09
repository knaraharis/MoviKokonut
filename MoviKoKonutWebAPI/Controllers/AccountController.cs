
namespace MoviKokonutWebAPI.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Http;
    using AutoMapper;
    using MoviKokonut.Domain.Person;
    using MoviKokonutWebAPI.Models;
    using MoviKokonutWebAPI.Resources;
    using MoviKokonut.Repository;

    public class AccountController : ApiController
    {
        private IPersonRepository personRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private ISalesOrderRepository orderRepository;
        private IStateProvinceRepository stateProvinceRepository;

        public AccountController(
            IPersonRepository personRepository,
            IShoppingCartRepository shoppingCartRepository,
            ISalesOrderRepository orderRepository,
            IStateProvinceRepository stateProvinceRepository)
        {
            this.personRepository = personRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.orderRepository = orderRepository;
            this.stateProvinceRepository = stateProvinceRepository;
        }

        public HttpResponseMessage Get(string id)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Strings.InvalidPersonIdentifier);
            }

            var person = this.personRepository.GetPerson(guid);

            if (person == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.PersonNotFoundById, id));
            }

            var personDetail = new PersonDetail();
            Mapper.Map(person, personDetail);

            // load the state provinces to match up with the person's address
            var stateProvinces = this.stateProvinceRepository.GetStateProvinces();
            foreach (var address in personDetail.Addresses)
            {
                address.CountryName = stateProvinces.SingleOrDefault(s => s.StateProvinceId == address.StateProvinceId).StateProvinceCode;
            }

            return Request.CreateResponse(HttpStatusCode.OK, personDetail);
        }

        [HttpPost]
        public HttpResponseMessage Login(LoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                return Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.StringCannotBeNullEmptyOrOnlyWhiteSpace, "LoginInfo"));
            }

            var person = this.personRepository.GetPersonByEmail(loginInfo.UserName);

            if (person == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.InvalidUsernameOrPassword));
            }

            if (AccountController.HashPassword(person.PasswordSalt, loginInfo.Password) != person.PasswordHash)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.InvalidUsernameOrPassword));
            }

            // check for an orphaned shopping cart
            var shoppingCart = this.shoppingCartRepository.GetShoppingCart(person.PersonGuid.ToString());
            if (this.orderRepository.IsOrderSaved(shoppingCart.TrackingId))
            {
                // order is already sumbmitted, delete the shopping cart; 
                this.shoppingCartRepository.DeleteShoppingCart(shoppingCart.UserCartId);
            }

            return Request.CreateResponse(HttpStatusCode.OK, person.PersonGuid.ToString());
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is likely due to the IL generation and the inline returns, so we will suppress it.")]
        [HttpPost]
        public HttpResponseMessage Register(RegistrationInfo registrationInfo)
        {
            if (registrationInfo == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.ParameterCannotBeNull, "registrationInfo"));
            }

            try
            {
                // catch any validation exceptions that exist based on the registrationInfo's data annotations
                Validator.ValidateObject(registrationInfo, new ValidationContext(registrationInfo), true);
            }
            catch (ValidationException valEx)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, valEx.Message);
            }

            var person = new Person()
            {
                FirstName = registrationInfo.FirstName,
                LastName = registrationInfo.LastName
            };

            try
            {
                foreach (var emailAddress in registrationInfo.EmailAddresses)
                {
                    // check already registered email
                    if (this.personRepository.GetPersonByEmail(emailAddress) != null)
                    {
                        return Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest,
                            string.Format(CultureInfo.CurrentCulture, Strings.DuplicateEmailAddress, emailAddress));
                    }

                    person.AddEmailAddress(emailAddress);
                }

                foreach (var addressInfo in registrationInfo.Addresses)
                {
                    person.AddAddress(new Address()
                    {
                        AddressLine1 = addressInfo.AddressLine1,
                        AddressLine2 = addressInfo.AddressLine2,
                        City = addressInfo.City,
                        PostalCode = addressInfo.PostalCode,
                        StateProvinceId = addressInfo.StateProvinceId
                    });
                }

                foreach (var creditCard in registrationInfo.CreditCards)
                {
                    // check already registered credit card
                    if (this.personRepository.IsCreditCardRegistered(creditCard.CardNumber))
                    {
                        return Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest,
                            string.Format(CultureInfo.CurrentCulture, Strings.CreditCardAlreadyRegistered, creditCard.CardNumber));
                    }

                    person.AddCreditCard(new CreditCard()
                    {
                        CardNumber = creditCard.CardNumber,
                        CardType = creditCard.CardType,
                        ExpMonth = creditCard.ExpMonth,
                        ExpYear = creditCard.ExpYear
                    });
                }

                var salt = AccountController.GenerateSalt();
                var hashedPassword = AccountController.HashPassword(salt, registrationInfo.Password);

                person.PasswordSalt = salt;
                person.PasswordHash = hashedPassword;
            }
            catch (ValidationException valEx)
            {
                // catch any validation exceptions that occur when adding to the collection based on their data annotations
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, valEx.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            try
            {
                this.personRepository.SavePerson(person);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.UserRegistrationFailed, ex.Message));
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static string GenerateSalt()
        {
            Random random = new Random();
            int saltSize = random.Next(2, 4);

            byte[] saltBytes = new byte[saltSize];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string encodedSalt, string password)
        {
            using (HashAlgorithm hashAlgorithm = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(encodedSalt);
                byte[] saltedPasswordBytes = new byte[(int)saltBytes.Length + (int)passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, 0, (int)saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, (int)saltBytes.Length, (int)passwordBytes.Length);
                byte[] hashedSaltedPasswordBytes = hashAlgorithm.ComputeHash(saltedPasswordBytes);
                string saltedHashedPassword = Convert.ToBase64String(hashedSaltedPasswordBytes);
                return saltedHashedPassword;
            }
        }
    }
}
