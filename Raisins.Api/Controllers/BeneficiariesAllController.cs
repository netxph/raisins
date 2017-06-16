using Raisins.Beneficiaries.Interfaces;
using Raisins.Beneficiaries.Services;
using Raisins.Data.Repository;
using D = Raisins.Beneficiaries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class BeneficiariesAllController : ApiController
    {
        private readonly IBeneficiaryService _service;
        protected IBeneficiaryService Service { get { return _service; } }
        public BeneficiariesAllController()
            : this(new BeneficiaryService(
                    new BeneficiaryRepository(), 
                    new AccountRepository()))
        {
        }
        public BeneficiariesAllController(IBeneficiaryService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("BeneficiariesController:service");
            }
            _service = service;
        }

        [HttpGet]
        public D.Beneficiaries GetBeneficiaries()
        {
            return Service.GetAll();
        }
    }
}
