using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raisins.Client.Raffle
{
    public class TestDataProvider : IRaisinsDataProvider
    {
        private List<Ticket> _tickets;

        protected List<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return Tickets;
        }

        public IEnumerable<Ticket> GetTicketsByPaymentClass(PaymentClass paymentClass)
        {
            var code = GetCode(paymentClass);

            return Tickets.Where(t => t.TicketCode.StartsWith(code));
        }

        protected virtual string GetCode(PaymentClass paymentClass)
        {
            return Convert.ToInt32(paymentClass).ToString("00");
        }

        public void LoadData()
        {
            _tickets = new List<Ticket>();

            Random rnd = new Random();
            var paymentClasses = Enum.GetValues(typeof(PaymentClass)).Cast<PaymentClass>().ToList();

            for (int i = 0; i < 1000; i++)
            {
                _tickets.Add(new Ticket()
                {
                    ID = i,
                    Name = Path.GetRandomFileName(),
                    TicketCode = GetCode(paymentClasses[rnd.Next(0, paymentClasses.Count)])
                });
            }
        }
    }
}
