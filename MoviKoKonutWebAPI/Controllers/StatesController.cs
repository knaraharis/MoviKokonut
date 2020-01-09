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
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using MoviKokonut.Repository;

    public class StatesController : ApiController
    {
        private IStateProvinceRepository stateProvinceRepository;

        public StatesController(IStateProvinceRepository stateProvinceRepository)
        {
            this.stateProvinceRepository = stateProvinceRepository;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, this.stateProvinceRepository.GetStateProvinces());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
