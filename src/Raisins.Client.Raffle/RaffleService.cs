using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {

        public List<Ticket> Tickets { get; set; }
        public Random Random { get; set; }

        public RaffleService()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            Tickets = Ticket.GetAll();
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
