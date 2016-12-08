using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Raffle
{
    public class EntityFrameworkRaisinsDataProvider : IRaisinsDataProvider
    {
        private readonly IEnumerable<Ticket> _tickets;

        protected IEnumerable<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
        }

        public EntityFrameworkRaisinsDataProvider()
        {
            var db = new RaisinsDB();

            _tickets = db.Tickets;
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return Tickets;
        }

        public IEnumerable<Ticket> GetTicketsByPaymentClass(PaymentClass paymentClass)
        {
            var code = ((int)paymentClass).ToString("00");

            return Tickets.Where(t => t.TicketCode.StartsWith(code));
        }
    }
}
