using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client
{
    public class PaymentType
    {
        public PaymentType(string type)
        {
            Type = type;
        }
        public PaymentType()
        {
        }
        public string Type { get; set; }
    }
}