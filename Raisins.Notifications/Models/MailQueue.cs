using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Models
{
    public class MailQueue
    {
        private readonly List<Ticket> _tickets;


        public MailQueue(int paymentID, string to)
            : this(paymentID, to, false)
        {
        }
        public MailQueue(int paymentID, bool status)
            : this(paymentID, string.Empty, status)
        {
        }

        public MailQueue(int paymentID, string to, bool status)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("MailQueue:paymentID");
            }
            PaymentID = paymentID;
            Status = status;

            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("MailQueue:to");
            }

            To = to;

            _tickets = new List<Ticket>();
            Name = string.Empty;
        }

        public int PaymentID { get; private set; }
        public bool Status { get; private set; }
        public string To { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public decimal Amount { get; private set; }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public IEnumerable<Ticket> Tickets { get { return _tickets; } }

        public void SetTickets(IEnumerable<Ticket> tickets)
        {
            _tickets.Clear();
            _tickets.AddRange(tickets);
        }
    }

    public class Ticket
    {
        public Ticket(string code)
        {
            Code = code;
        }

        public string Code { get; private set; }
    }
}
