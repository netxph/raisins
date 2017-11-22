using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Payments.Services;
using Raisins.Data.Repository;
using API = Raisins.Api.Models;
using D = Raisins.Payments.Models;
using AutoMapper;

namespace Raisins.Api.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }

        public PaymentsController() : this(new PaymentService(new PaymentRepository(), new BeneficiaryForPaymentRepository(), new ProfileRepository()))
        {
        }

        public PaymentsController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsController:service");
            }
            _service = service;
        }

        [HttpGet]
        public D.Payment GetPayment(int paymentID)
        {
            return Service.Get(paymentID);
        }

        [HttpPost]
        public HttpResponseMessage NewPayment([FromBody]API.Payment payment)
        {
            Service.Create(Mapper.Map<API.Payment, D.Payment>(payment));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage EditPayment([FromBody]API.Payment payment)
        {
            Service.Edit(Mapper.Map<API.Payment, D.Payment>(payment));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
