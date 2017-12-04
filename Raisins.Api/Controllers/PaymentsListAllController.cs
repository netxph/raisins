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
    public class PaymentsListAllController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }

        public PaymentsListAllController() : this(new ApiResolver().PaymentService)
        {
        }

        public PaymentsListAllController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsListAllController:service");
            }
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Payment> GetPaymentsList()
        {
            var payments = Service.GetAll();

            //var filteredPayments = new Payments.Models.Payments();

            //filteredPayments.AddRange( payments.Where(p => p.Source.Source.Equals("Local", StringComparison.InvariantCultureIgnoreCase)));

            //int index = 100;
            //int counter = 0;

            //foreach (var payment in filteredPayments)
            //{
            //    var ticketCount = payment.Tickets;

            //    counter += ticketCount;

            //    if(counter == index)
            //    {
            //        var winnerName = payment.Name;
            //        var paymentid = payment.PaymentID;
            //    }
            //}

            //return filteredPayments;

            return payments;
        }
    }
}
