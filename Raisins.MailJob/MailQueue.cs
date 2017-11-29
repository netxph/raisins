using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.MailJob
{
    public class MailQueue
    {
        public int PaymentID { get; set; }
        public bool Status { get; set; }
        public string To { get; set; }
    }
}
