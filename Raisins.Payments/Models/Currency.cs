using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class Currency
    {
        public Currency(string currencyCode, decimal ratio, decimal exchangeRate)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentNullException("Currency:currencyCode");
            }
            CurrencyCode = currencyCode;
            if (ratio < 0)
            {
                throw new ArgumentNullException("Currency:ratio");
            }
            Ratio = ratio;
            if (exchangeRate < 0)
            {
                throw new ArgumentNullException("Currency:exchangeRate");
            }
            ExchangeRate = exchangeRate;
        }
        public Currency() { }
        public string CurrencyCode { get; private set; }
        public decimal Ratio { get; private set; }
        public decimal ExchangeRate { get; private set; }
    }
}
