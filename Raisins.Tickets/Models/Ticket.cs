using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Ticket
    {
        public Ticket()
        {

        }

        public Ticket(string ticketCode, string name, int paymentID)
        {
            if (string.IsNullOrEmpty(ticketCode))
            {
                throw new ArgumentNullException("Ticket:ticketCode");
            }
            TicketCode = ticketCode;

            PaymentSource = GetPaymentSource(ticketCode);

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

        public Ticket(string paymentSource, int paymentID, int beneficiaryID, int iteration, string name)
        {
            if (paymentID < 0 || beneficiaryID < 0 || iteration < 0)
            {
                throw new ArgumentNullException("Ticket:ticketCode");
            }

            if (paymentSource == null)
            {
                throw new ArgumentNullException("Ticket: paymentSource");
            }

            PaymentSource = paymentSource;

            TicketCode = GenerateCode(paymentSource, paymentID, beneficiaryID, iteration);

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

        public string PaymentSource { get; set; }

        const string PAYMENTSOURCE_FORMAT = "00";

        //TODO: manage this in payment model
        private string GetPaymentSource(string ticketCode)
        {
            var code = TicketCode.Substring(0, PAYMENTSOURCE_FORMAT.Length);
            string source = string.Empty;

            switch (code)
            {
                case "01":
                    source = "External";
                    break;
                case "02":
                    source = "International";
                    break;
                default:
                    source = "Local";
                    break;
            }

            return source;
        }

        //TODO Create domain obj for Ticket Code
        protected virtual string GenerateCode(string paymentSource, int paymentID, int beneficiaryID, int iteration)
        {
            return string.Format("{0}{1}{2}{3}",
                Convert(paymentSource).ToString(PAYMENTSOURCE_FORMAT),
                beneficiaryID.ToString("00"),
                paymentID.ToString("X").PadLeft(5, '0'),
                iteration.ToString("00000"));
        }

        //TODO: manage this in payment model
        private int Convert(string paymentSource)
        {
            if (paymentSource.Equals("External", StringComparison.InvariantCultureIgnoreCase))
            {
                return 1;
            }
            else if (paymentSource.Equals("International", StringComparison.InvariantCultureIgnoreCase))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
