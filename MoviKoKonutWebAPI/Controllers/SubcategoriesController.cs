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
    
    public class SubcategoriesController : ApiController
    {
        private readonly ICategoryRepository categoryRepository;

        public SubcategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public HttpResponseMessage GetSubcategories(int categoryId)
        {
            var subcategories = this.categoryRepository.GetSubcategories(categoryId);
            if (subcategories == null)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentCulture, Strings.CategoryNotFound));
            }

            var result = new List<Subcategory>();
            
            Mapper.Map(subcategories, result);
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
