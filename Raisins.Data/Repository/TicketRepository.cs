using DATA = Raisins.Data.Models;
using Raisins.Tickets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Tickets.Models;

namespace Raisins.Data.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private RaisinsContext _context;

        public TicketRepository() : this(RaisinsContext.Instance)
        {
        }
        public TicketRepository(RaisinsContext context)
        {
            _context = context;
        }
        public void Add(D.Ticket ticket)
        {
            _context.Tickets.Add(ConvertToEF(ticket));
            _context.SaveChanges();
        }
        public void Add(D.Tickets tickets)
        {
            _context.Tickets.AddRange(ConvertToEFList(tickets));
            _context.SaveChanges();
        }

        public D.Tickets GetAll()
        {
            return ConvertToDomainList(_context.Tickets);
        }

        public D.Ticket GetByCode(string ticketCode)
        {
            return ConvertToDomain(_context.Tickets.FirstOrDefault(t => t.TicketCode == ticketCode));
        }

        public D.Ticket GetByID(int ticketID)
        {
            return ConvertToDomain(_context.Tickets.FirstOrDefault(t => t.TicketID == ticketID));
        }

        private D.Ticket ConvertToDomain(DATA.Ticket efTicket)
        {
            return new D.Ticket(efTicket.TicketCode, efTicket.Name, efTicket.PaymentID);
        }
        private D.Tickets ConvertToDomainList(IEnumerable<DATA.Ticket> efTickets)
        {
            D.Tickets tickets = new D.Tickets();
            foreach (var efTicket in efTickets)
            {
                tickets.Add(ConvertToDomain(efTicket));
            }
            return tickets;
        }
        private DATA.Ticket ConvertToEF(D.Ticket ticket)
        {
            return new DATA.Ticket(ticket.TicketCode, ticket.Name, ticket.PaymentID);
        }
        private IEnumerable<DATA.Ticket> ConvertToEFList(D.Tickets tickets)
        {
            List<DATA.Ticket> eftickets = new List<DATA.Ticket>();
            foreach (var ticket in tickets)
            {
                eftickets.Add(ConvertToEF(ticket));
            }
            return eftickets;            
        }
    }
}
