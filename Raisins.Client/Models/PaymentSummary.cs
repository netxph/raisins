using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class PaymentSummary
    {
        public string Beneficiary { get; set; }
        public decimal Total { get; set; }
        public string CurrencyCode { get; set; }
    }
}