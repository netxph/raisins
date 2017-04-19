using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class CurrenciesController : ApiController
    {
        private readonly ICurrencyRepository _service;
        protected ICurrencyRepository Service { get { return _service; } }
        public CurrenciesController() : this(new CurrencyRepository())
        {
        }
        public CurrenciesController(ICurrencyRepository service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("CurrenciesController:service");
            }
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Currency> GetCurrencies()
        {
            return Service.GetAll();
        }
    }
}
