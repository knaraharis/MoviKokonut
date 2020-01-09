//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace MoviKokonutWebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using AutoMapper;
    using MoviKokonut.Domain.Order;
    using MoviKokonut.Domain.Services.Interface;
    using MoviKokonutWebAPI.Models;
    using MoviKokonutWebAPI.Resources;
    using MoviKokonut.Repository;

    public class OrdersController : ApiController
    {
        private readonly IOrderHistoryRepository orderHistoryRepository;
        private readonly IShoppingCartRepository shoppingCartItemRepository;
        private readonly IPersonRepository personRepository;
        private readonly IProductRepository productRepository;

        private readonly IOrderService orderService;
        private readonly IInventoryService inventoryService;

        public OrdersController(
            IOrderHistoryRepository orderHistoryRepository,
            IShoppingCartRepository shoppingCartItemRepository,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IOrderService orderService,
            IInventoryService inventoryService)
        {
            this.orderHistoryRepository = orderHistoryRepository;
            this.shoppingCartItemRepository = shoppingCartItemRepository;
            this.personRepository = personRepository;
            this.productRepository = productRepository;
            this.orderService = orderService;
            this.inventoryService = inventoryService;
        }

        public HttpResponseMessage Get(string id)
        {
            Guid idGuid = new Guid(id);
            var order = this.orderHistoryRepository.GetOrderHistoryByTrackingId(idGuid);
            if (order == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.OrderNotFoundById, id));
            }

            var orderDetail = new OrderDetail();
            Mapper.Map(order, orderDetail);
            
            return Request.CreateResponse(HttpStatusCode.OK, orderDetail);
        }

        public HttpResponseMessage GetHistory(string personId)
        {
            Guid guid;
            if (!Guid.TryParse(personId, out guid))
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.InvalidPersonIdentifier));
            }

            var person = this.personRepository.GetPerson(guid);
            if (person == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.PersonNotFoundById, personId));
            }

            var ordersHistory = this.orderHistoryRepository.GetOrdersHistories(person.Id);
            var ordersHistoryInfo = new List<OrderHistoryInfo>();

            if (ordersHistory != null)
            {
                Mapper.Map(ordersHistory, ordersHistoryInfo);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ordersHistoryInfo);
        }

        [Description("This method accepts a history id (guid) to look up for the specific order")]
        public HttpResponseMessage GetHistoryDetails(string historyId)
        {
            Guid historyIdguid = new Guid(historyId);
            var orderHistory = this.orderHistoryRepository.GetOrderHistoryByHistoryId(historyIdguid);
            if (orderHistory == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.OrderHistoryNotFoundById, historyId));
            }

            var orderHistoryInfo = new OrderHistoryInfo();
            Mapper.Map(orderHistory, orderHistoryInfo);

            return Request.CreateResponse(HttpStatusCode.OK, orderHistoryInfo);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This method could potentially be refactored, but this class needs to do some validations to process an order.")]
        public HttpResponseMessage Post(OrderInfo order)
        {
            if (order == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.ParameterCannotBeNull, "order"));
            }

            Guid guid;
            if (!Guid.TryParse(order.CartId, out guid))
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(CultureInfo.CurrentCulture, Strings.InvalidPersonIdentifier));
            }

            var person = this.personRepository.GetPerson(guid);
            if (person == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.PersonNotFoundById, order.CartId));
            } 
            
            var shoppingCart = this.shoppingCartItemRepository.GetShoppingCart(order.CartId.ToString());
            if (shoppingCart == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.ShoppingCartNotFoundById, order.CartId));
            }

            if (shoppingCart.ShoppingCartItems.Count() == 0)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.ShoppingCartCannotBeEmpty, order.CartId));
            }

            var newOrder = new Order()
            {
                TrackingId = shoppingCart.TrackingId,
                BillToAddress = person.Addresses.SingleOrDefault(a => a.Id == order.BillingAddressId),
                CreditCard = person.CreditCards.SingleOrDefault(c => c.Id == order.CreditCardId),
                CustomerId = person.Id,
                DueDate = DateTime.Now.AddDays(3),
                Freight = 0,
                OrderDate = DateTime.Now,
                ShippingAddress = person.Addresses.SingleOrDefault(a => a.Id == order.ShippingAddressId),
                Status = OrderStatus.Pending
            };

            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                newOrder.AddOrderItem(new OrderItem()
                {
                    Product = this.productRepository.GetProduct(shoppingCartItem.ProductId),
                    Quantity = (short)shoppingCartItem.Quantity,
                    UnitPrice = shoppingCartItem.ProductPrice
                });
            }

            try
            {
                Validator.ValidateObject(newOrder, new ValidationContext(newOrder), true);
            }
            catch (ValidationException valEx)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, valEx);
            }

            if (this.inventoryService.InventoryAndPriceCheck(shoppingCart))
            {
                // There was an inventory or price discrepancy between the shopping cart and the product database
                // so we save the shopping cart and return an OK since there is nothing RESTfully wrong with the message.
                // The UI will be responsible for how to handle this by redirecting to an appropriate page.
                this.shoppingCartItemRepository.SaveShoppingCart(shoppingCart);

                var cartItems = new List<CartItem>();
                Mapper.Map(shoppingCart.ShoppingCartItems, cartItems);

                return Request.CreateResponse(HttpStatusCode.OK, cartItems);
            }

            try
            {
                this.orderService.ProcessOrder(newOrder);
            }
            catch (Exception ex)
            {
                // this save failed so we cannot continue processing the order
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            // since we have captured the order we can delete the users shopping cart now
            this.shoppingCartItemRepository.DeleteShoppingCart(shoppingCart.UserCartId);

            return Request.CreateResponse(HttpStatusCode.Created, newOrder.TrackingId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
