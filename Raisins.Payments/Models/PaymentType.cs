using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class PaymentType
    {
        public PaymentType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("PaymentType:type");
            }
            Type = type;
        }
        public string Type { get; private set; }
    }
}
