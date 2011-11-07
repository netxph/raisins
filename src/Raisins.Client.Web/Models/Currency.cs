using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Currency
    {
        public int CurrencyID { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Ratio { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}