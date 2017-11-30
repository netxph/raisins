using Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Raffles.Interfaces
{
    public interface IRaffleService
    {
        Ticket GetRandomTicket(string paymentSource);
    }
}
