using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Currency
    {
        public int CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Ratio { get; set; }
        public decimal ExchangeRate { get; set; }

        public Currency(string code, decimal ratio, decimal exchangeRate)
            :this(0, code, ratio, exchangeRate)
        {

        }

        public Currency(int id, string code, decimal ratio, decimal exchangeRate)
        {
            CurrencyID = id;
            CurrencyCode = code;
            Ratio = ratio;
            ExchangeRate = exchangeRate;
        }

        public bool IsBulkEligible()
        {
            return CurrencyCode.Equals("PHP", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
