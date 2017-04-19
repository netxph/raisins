using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Models
{
    public class MailQueue
    {
        public MailQueue(int paymentID, string to)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("MailQueue:paymentID");
            }
            PaymentID = paymentID;
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("MailQueue:to");
            }
            To = to;           
        }
        public MailQueue(int paymentID, bool status)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("MailQueue:paymentID");
            }
            PaymentID = paymentID;
            Status = status;
        }

        public MailQueue(int paymentID, string to, bool status)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("MailQueue:paymentID");
            }
            PaymentID = paymentID;
            Status = status;
            To = to;
        }
        public int PaymentID { get; private set; }
        public bool Status { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
    }
}
