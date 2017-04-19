using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class PaymentSummary
    {
        public PaymentSummary(string beneficiary, IEnumerable<Payment> payments, string currencyCode)
        {
            if (string.IsNullOrEmpty(beneficiary))
            {
                throw new ArgumentNullException("PaymentSummary:beneficiary");
            }
            Beneficiary = beneficiary;
            if (payments == null)
            {
                throw new ArgumentNullException("PaymentSummary:payments");
            }
            CalculateTotal(payments);
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException("PaymentSummary:currencyCode");
            }
            CurrencyCode = currencyCode;
        }
        public string Beneficiary { get; private set; }
        public decimal Total { get; private set; }
        public string CurrencyCode { get; private set; }

        public void CalculateTotal(IEnumerable<Payment> payments)
        {
            decimal total = 0;
            foreach (var payment in payments)
            {
                total += (payment.Amount * payment.Currency.ExchangeRate);
            }
            Total = total;
        }

    }
}
