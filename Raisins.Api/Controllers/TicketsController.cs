using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Services;
using Raisins.Tickets.Interfaces;
using T = Raisins.Tickets.Models;
using Raisins.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API = Raisins.Api.Models;

namespace Raisins.Api.Controllers
{
    public class TicketsController : ApiController
    {
        private readonly ITicketService _ticketService;
        protected ITicketService TicketService { get { return _ticketService; } }
        public TicketsController() : this(new TicketService(new TicketRepository(), new BeneficiaryForTicketRepository()))
        {
        }
        public TicketsController (ITicketService ticketService)
        {
            if (ticketService == null)
            {
                throw new ArgumentNullException("TicketsController:ticketService");
            }
            _ticketService = ticketService;
        }
        [HttpPost]
        public HttpResponseMessage GenerateTickets([FromBody]API.Payment payment)
        {
            T.Beneficiary beneficiary = TicketService.GetBeneficiary(payment.Beneficiary.Name);

            TicketService.GenerateTickets(payment.PaymentID, beneficiary.BeneficiaryID, payment.Amount, payment.Currency.ExchangeRate, payment.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpGet]
        public T.Tickets GetTickets()
        {
            return TicketService.GetAll();
        }
    }
}
