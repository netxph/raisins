using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using NHibernate.Criterion;


namespace Raisins.Services
{
    [ActiveRecord]
    public class Payment : ActiveRecordBase<Payment>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public long ID { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public decimal Amount { get; set; }

        [Property]
        public string Location { get; set; }

        [BelongsTo("CurrencyID")]
        public Currency Currency { get; set; }

        [Property]
        public string Email { get; set; }

        [Property]
        public bool Locked { get; set; }

        [Property]
        public PaymentClass Class { get; set; }

        [Property]
        public string Remarks { get; set; }

        [BelongsTo("AuditedAccountID")]
        public Account AuditedBy { get; set; }

        [BelongsTo("BeneficiaryID")]
        public Beneficiary Beneficiary { get; set; }

        [HasMany]
        public IList<Ticket> Tickets { get; set; }

        [BelongsTo("CreatedAccountID")]
        public Account CreatedBy { get; set; }

        public static Payment[] FindByUser(string userName)
        {
            return Payment.FindAll().Where(payment => payment.CreatedBy.UserName == userName).OrderByDescending(payment => payment.ID).ToArray();
        }

        public static Payment[] FindByBeneficiary(string beneficiary)
        {
            return Payment.FindAll().Where(payment => payment.Beneficiary.Name == beneficiary).OrderByDescending(payment => payment.ID).ToArray();
        }
    }

    public enum PaymentClass
    { 
        NotSpecified,
        Internal,
        Foreign,
        External
    }
}
