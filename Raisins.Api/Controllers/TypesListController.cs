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
    public class TypesListController : ApiController
    {
        private readonly IPaymentTypeService _service;
        protected IPaymentTypeService Service { get { return _service; } }

        public TypesListController() : this (new PaymentTypeService(new PaymentTypeRepository()))
        {
        }

        public TypesListController(IPaymentTypeService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("TypesListController:service");
            }
            _service = service;
        }

        [HttpGet]
        public IEnumerable<D.PaymentType> GetAll()
        {
            return Service.GetAll();
        }
    }
}
