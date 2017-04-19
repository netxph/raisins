using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Payment
    {
        public Payment(int paymentID)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("Payment:paymentID");
            }
            PaymentID = paymentID;
        }
        public int PaymentID { get; private set; }
    }
}
