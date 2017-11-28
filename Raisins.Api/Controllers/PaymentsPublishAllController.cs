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
    public class PaymentsPublishAllController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public PaymentsPublishAllController() : this(new ApiResolver().PaymentService)
        {
        }
        public PaymentsPublishAllController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsController:service");
            }
            _service = service;
        }
        [HttpPut]
        public HttpResponseMessage PublishAllPayment([FromBody]List<API.Payment> payments)
        {
            Service.PublishAll(Mapper.Map<List<API.Payment>, List<D.Payment>>(payments));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
