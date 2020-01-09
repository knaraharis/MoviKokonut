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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using AutoMapper;
    using MoviKokonutWebAPI.Models;
    using MoviKokonutWebAPI.Resources;
    using MoviKokonut.Repository;
    
    public class ProductsController : ApiController
    {
        private readonly IProductRepository productRepository;
        private readonly IProductRecommendationRepository productRecommendationRepository;

        public ProductsController(
            IProductRepository productRepository,
            IProductRecommendationRepository productRecommendationRepository)
        {
            this.productRepository = productRepository;
            this.productRecommendationRepository = productRecommendationRepository;
        }

        public HttpResponseMessage GetProducts(int subcategoryId)
        {
            var products = this.productRepository.GetProducts(subcategoryId);
            if (products == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.SubcategoryNotFound));
            }

            var productInfos = new List<ProductInfo>();
            
            Mapper.Map(products, productInfos);

            return Request.CreateResponse(HttpStatusCode.OK, productInfos);
        }

        public HttpResponseMessage Get(int id)
        {
            var product = this.productRepository.GetProduct(id);

            // check to make sure the product exists
            if (product == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.ProductNotFoundById, id));
            }

            var productDetail = new ProductDetail();

            Mapper.Map(product, productDetail);

            return Request.CreateResponse(HttpStatusCode.OK, productDetail);
        }

        public HttpResponseMessage GetRecommendations(int productId)
        {
            // check to make sure the product has recommendations associated with it
            var recommendedProducts = this.productRecommendationRepository.GetProductRecommendations(productId);
            if (recommendedProducts == null || recommendedProducts.Count == 0)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.NoRecommendationsFoundForProduct, productId));
            }

            var recommendations = new List<Recommendation>();

            Mapper.Map(recommendedProducts, recommendations);
            
            return Request.CreateResponse(HttpStatusCode.OK, recommendations);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
