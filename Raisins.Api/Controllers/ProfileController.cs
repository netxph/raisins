using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using Raisins.Payments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public ProfileController() : this(new PaymentService(new PaymentRepository(), new BeneficiaryForPaymentRepository(), new ProfileRepository()))
        {
        }
        public ProfileController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("ProfileController:service");
            }
            _service = service;
        }
        [HttpGet]
        public AccountProfile GetProfile(string userName)
        {
            return Service.GetProfile(userName);
        }
    }
}
