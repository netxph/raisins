using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Ticket : ActiveRecordBase<Ticket>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public long ID { get; set; }

        [Property]
        public string TicketCode { get; set; }

        [Property]
        public string Name { get; set; }

        [BelongsTo("PaymentID")]
        public Payment Payment { get; set; }

        [OneToOne]
        public WinnerLog WinnerLog { get; set; }

        public override void Save()
        {
            base.Save();

            string classPart = ((int)Payment.Class).ToString().PadRight(2, '0');
            string userPart = Payment.CreatedBy.ID.ToString().PadLeft(3, '0');
            string paymentPart = Payment.ID.ToString().PadLeft(6, '0');
            string sequencePart = ID.ToString().PadLeft(6, '0');

            TicketCode = string.Format("{0}-{1}{2}{3}", classPart, userPart, paymentPart, sequencePart);

            base.Save();
        }

        public static Ticket[] FindAllByPaymentClass(PaymentClass paymentClass)
        {
            return FindAll().Where(ticket => ticket.Payment.Class == paymentClass).ToArray();
        }

        
    }
}
