using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using API = Raisins.Api.Models;
using D = Raisins.Payments.Models;
using Raisins.Payments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;

namespace Raisins.Api.Controllers
{
    public class PaymentsPublishController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public PaymentsPublishController() : this(new PaymentService(new PaymentRepository(), new BeneficiaryForPaymentRepository(), new ProfileRepository()))
        {
        }
        public PaymentsPublishController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsController:service");
            }
            _service = service;
        }
        [HttpPut]
        public HttpResponseMessage PublishPayment([FromBody]API.Payment payment)
        {
            Service.Publish(Mapper.Map<API.Payment, D.Payment>(payment));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
