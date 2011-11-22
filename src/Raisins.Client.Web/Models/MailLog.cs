using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class MailLog
    {

        public int MailLogID { get; set; }
        public int PaymentID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EmailAddress { get; set; }
        public bool IsSuccessful { get; set; }

    }
}