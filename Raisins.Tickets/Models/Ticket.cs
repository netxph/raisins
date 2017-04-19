using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Ticket
    {
        public Ticket(string ticketCode, string name, int paymentID)
        {
            if (string.IsNullOrEmpty(ticketCode))
            {
                throw new ArgumentNullException("Ticket:ticketCode");
            }
            TicketCode = ticketCode;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Ticket:name");
            }
            Name = name;
            if (paymentID < 0)
            {
                throw new ArgumentNullException("Ticket:paymentID");
            }
            PaymentID = paymentID;
        }
        public Ticket(int paymentID, int beneficiaryID, int iteration, string name)
        {
            if (paymentID < 0 || beneficiaryID < 0 || iteration < 0)
            {
                throw new ArgumentNullException("Ticket:ticketCode");
            }
            TicketCode = GenerateCode(paymentID, beneficiaryID, iteration);
            PaymentID = paymentID;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Ticket:name");
            }
            Name = name;
        }
        public string TicketCode { get; private set; }

        public string Name { get; private set; }

        public int PaymentID { get; private set; }
        public virtual Payment Payment { get; private set; }

        private string GenerateCode(int paymentID, int beneficiaryID, int iteration)
        {
            string TicketCode = string.Format("{0}{1}{2}", beneficiaryID.ToString("00"), paymentID.ToString("X").PadLeft(5, '0'), iteration.ToString("00000"));

            return TicketCode;
        }
    }
}
