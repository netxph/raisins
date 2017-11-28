using Raisins.Data.Repository;
using P = Raisins.Payments.Models;
using Raisins.Tickets.Interfaces;
using Raisins.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using T = Raisins.Tickets.Models;
using API = Raisins.Api.Models;

namespace Raisins.Api.Controllers
{
    public class TicketsAllController : ApiController
    {
        private readonly ITicketService _ticketService;
        protected ITicketService TicketService { get { return _ticketService; } }

        public TicketsAllController() : this(new ApiResolver().TicketService)
        {
        }

        public TicketsAllController(ITicketService ticketService)
        {
            if (ticketService == null)
            {
                throw new ArgumentNullException("TicketsAllController:ticketService");
            }
            _ticketService = ticketService;
        }

        [HttpPost]
        public HttpResponseMessage GenerateTickets([FromBody]IEnumerable<API.Payment> payments)
            //nasa Payment class, gawin same sa TicketsController
        {
            foreach (var payment in payments)
            {                
                T.Beneficiary beneficiary = TicketService.GetBeneficiary(payment.Beneficiary.Name);

                TicketService.GenerateTickets(payment.PaymentID, beneficiary.BeneficiaryID, payment.Amount, payment.Currency.ExchangeRate, payment.Name);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }        
    }
}
