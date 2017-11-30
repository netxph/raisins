using M = Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Interfaces
{
    public interface ITicketRepository
    {
        M.Tickets GetAll();
        M.Tickets GetAll(string paymentSource);
        M.Ticket GetByCode(string ticketCode);
        M.Ticket GetByID(int ticketID);
        void Add(M.Ticket ticket);
        void Add(M.Tickets tickets);
    }
}
