using T = Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Raffles.Interfaces
{
    public interface IRaffleService
    {
        T.Ticket GetRandomTicket(string paymentSource);
        IEnumerable<T.Ticket> GetTickets(string paymentSource);
    }
}
