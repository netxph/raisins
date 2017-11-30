using Raisins.Raffles.Interfaces;
using Raisins.Raffles.Services;
using Raisins.Tickets.Models;
using Raisins.Data.Repository;
using Raisins.Client.Randomizer.RandomOrg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class RaffleController : ApiController
    {
        private readonly IRaffleService _service;

        public IRaffleService Service
        {
            get { return _service; }
        }

        const string RANDOMIZER_API_KEY = "d8fd8706-0482-4460-8017-59719fd3ccb9";


        public RaffleController()
            : this(new RaffleService(
                    new TicketRepository(),
                    new RandomOrgIntegerRandomizerService(RANDOMIZER_API_KEY)))
        {
        }

        public RaffleController(IRaffleService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("RaffleController:service");
            }
            _service = service;
        }

        [HttpGet]
        public Ticket GetRandomTicket(string paymentSource)
        {
            return Service.GetRandomTicket(paymentSource);
        }
    }
}