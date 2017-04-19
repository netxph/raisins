using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using D = Raisins.Payments.Models;

namespace Raisins.Api.Controllers
{
    public class SourcesController : ApiController
    {
        private readonly IPaymentSourceService _service;
        protected IPaymentSourceService Service { get { return _service; } }

        public SourcesController() : this (new PaymentSourceService(new PaymentSourceRepository()))
        {
        }

        public SourcesController(IPaymentSourceService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("SourcesController:service");
            }
            _service = service;
        }

        [HttpGet]
        public D.PaymentSource GetSource(string source)
        {
            return Service.Get(source);
        }
    }
}
