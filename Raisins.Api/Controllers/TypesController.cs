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
    public class TypesController : ApiController
    {
        private readonly IPaymentTypeService _service;
        protected IPaymentTypeService Service { get { return _service; } }

        public TypesController() : this (new PaymentTypeService(new PaymentTypeRepository()))
        {
        }

        public TypesController(IPaymentTypeService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("TypesController:service");
            }
            _service = service;
        }

        [HttpGet]
        public D.PaymentType GetType(string type)
        {
            return Service.Get(type);
        }
    }
}
