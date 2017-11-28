using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Services;
using Raisins.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class GoalController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public GoalController() : this(new ApiResolver().PaymentService)
        {
        }

        public GoalController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("GoalController:service");
            }
            _service = service;
        }

        [HttpGet]
        public decimal GetTotal()
        {
            return Service.GetTotal();
        }
    }
}
