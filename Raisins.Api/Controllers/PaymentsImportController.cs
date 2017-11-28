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
    public class PaymentsImportController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public PaymentsImportController() : this(new ApiResolver().PaymentService)
        {
        }
        public PaymentsImportController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsImportController:service");
            }
            _service = service;
        }
        [HttpPost]
        public HttpResponseMessage ImportPayments([FromBody]List<API.Payment> payments)
        {
            Service.Import(Mapper.Map<List<API.Payment>, List<D.Payment>>(payments));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
