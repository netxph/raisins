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
        public void Add(Ticket payment)
        {
            _tickets.Add(payment);
        }

        public IEnumerator<Ticket> GetEnumerator()
        {
            return _tickets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tickets.GetEnumerator();
        }

    }
}
