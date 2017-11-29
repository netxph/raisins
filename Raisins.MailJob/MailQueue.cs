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
        public string Name { get; set; }
        public string Beneficiary { get; set; }
        public decimal Amount { get; set; }
        public List<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        public string Code { get; set; }
    }
}
