using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(string ticketCode, string name, int paymentID)
        {
            TicketCode = ticketCode;
            Name = name;
            PaymentID = paymentID;
        }

        [Key]
        public long TicketID { get; set; }

        public string TicketCode { get; set; }

        public string Name { get; set; }
        public int PaymentID { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
