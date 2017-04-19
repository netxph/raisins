using Raisins.Beneficiaries.Interfaces;
using Raisins.Beneficiaries.Services;
using D = Raisins.Beneficiaries.Models;
using Raisins.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Api.Models;
using AutoMapper;

namespace Raisins.Api.Controllers
{
    public class BeneficiariesController : ApiController
    {
        private readonly IBeneficiaryService _service;
        protected IBeneficiaryService Service { get { return _service; } }
        public BeneficiariesController() : this(new BeneficiaryService(new BeneficiaryRepository()))
        {
        }
        public BeneficiariesController(IBeneficiaryService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("BeneficiariesController:service");
            }
            _service = service;
        }
        [HttpPost]
        public HttpResponseMessage CreateBeneficiary([FromBody]Beneficiary beneficiary)
        {
            Service.Add(Mapper.Map<Beneficiary, D.Beneficiary>(beneficiary));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public D.Beneficiary GetBeneficiaries(int beneficiaryID)
        {
            return Service.Get(beneficiaryID);
        }
        [HttpPut]
        public HttpResponseMessage EditBeneficiary([FromBody]Beneficiary beneficiary)
        {
            Service.Edit(Mapper.Map<Beneficiary, D.Beneficiary>(beneficiary));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
