using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class Payments : IEnumerable<Payment>
    {
        private readonly List<Payment> _payments;

        public int TotalTickets { get; internal set; }

        public Payments()
        {
            _payments = new List<Payment>();
        }

        public void AddRange(IEnumerable<Payment> payments)
        {
            _payments.AddRange(payments);
        }

        public void Add(Payment payment)
        {
            _payments.Add(payment);
        }

        public IEnumerator<Payment> GetEnumerator()
        {
            return _payments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _payments.GetEnumerator();
        }
        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (var payment in _payments)
            {
                total += (payment.Amount * payment.Currency.ExchangeRate);
            }
            return total;
        }
    }
}
