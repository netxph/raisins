using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Tickets : IEnumerable<Ticket>
    {
        private readonly List<Ticket> _tickets;
        public Tickets()
        {
            _tickets = new List<Ticket>();
        }
        public void AddRange(IEnumerable<Ticket> tickets)
        {
            _tickets.AddRange(tickets);
        }
        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public IEnumerator<Ticket> GetEnumerator()
        {
            return _tickets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tickets.GetEnumerator();
        }

        public List<Ticket> GetAll()
        {
            return _tickets;
        }

        /*public Ticket GetRandomTicket(string paymentSource)
        {
            Random random = new Random();
            List<Ticket> chosenTickets = new List<Ticket>();
            foreach(Ticket t in _tickets)
            {
                if(t.PaymentSource==paymentSource)
                {
                    chosenTickets.Add(t);
                }
            }
            int index = random.Next() % (chosenTickets.Count);
            return _tickets[index];
        }*/
    }
}
