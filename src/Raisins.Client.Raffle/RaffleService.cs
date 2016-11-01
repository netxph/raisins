using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {
        private RaisinsDB _raisinsDb;

        public List<Ticket> Tickets { get; set; }
        public Random Random { get; set; }

        public RaffleService()
        {
            _raisinsDb = new RaisinsDB();
            Initialize();
        }

        protected virtual void Initialize()
        {
            Tickets = _raisinsDb.Tickets.ToList();
            Random = new Random(this.GetHashCode());
        }

        public Ticket GetRandomTicket(PaymentClass paymentClass)
        {
            var code = ((int)paymentClass).ToString("00");

            var tickets = Tickets.Where(t => t.TicketCode.StartsWith(code)).ToList();

            var index = Random.Next(0, tickets.Count());

            return tickets[index];
        }
    }
}
