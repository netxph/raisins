using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class PaymentSource
    {
        public PaymentSource(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException("PaymentSource:source");
            }
            Source = source;
        }
        public PaymentSource()
        {  }
        public string Source { get; private set; }
    }
}
