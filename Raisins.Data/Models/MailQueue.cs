using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class MailQueue
    {
        public MailQueue()
        {
        }
        public MailQueue(int paymentID, string to)
        {
            PaymentID = paymentID;
            To = to;
        }
        public MailQueue(int paymentID, bool status, string from, string to, string subject, string content)
        {
            PaymentID = paymentID;
            Status = status;
            From = from;
            To = to;
            Subject = subject;
            Content = content;
        }
        [Key]
        public int MailQueueID { get; set; }
        public int PaymentID { get; set; }
        public bool Status { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
