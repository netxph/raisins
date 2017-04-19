using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class PaymentSource
    {
        public PaymentSource(string source)
        {
            Source = source;
        }
        public PaymentSource()
        {
        }
        public string Source { get; set; }
    }
}