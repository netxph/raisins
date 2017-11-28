using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class Payment
    {
        public Payment()
        {
        }

        public Payment(int paymentID, string name, decimal amount, Currency currency, Beneficiary beneficiary, bool locked,
            string email, DateTime createdDate, DateTime? modifiedDate, DateTime paymentDate, DateTime? publishDate, int createdById, int modifiedById, PaymentSource source, PaymentType type,
            bool optOut)
        {
            if (paymentID < 0)
            {
                throw new ArgumentNullException("Payment:paymentID");
            }
            PaymentID = paymentID;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Payment:name");
            }
            Name = name;
            if (amount < 0)
            {
                throw new ArgumentNullException("Payment:amount");
            }
            Amount = amount;
            if (currency == null)
            {
                throw new ArgumentNullException("Payment:currency");
            }
            Currency = currency;
            if (beneficiary == null)
            {
                throw new ArgumentNullException("Payment:beneficiary");
            }
            Beneficiary = beneficiary;

            // added
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Payment:email");
            }
            Email = email;

            if (createdDate == null)
            {
                throw new ArgumentNullException("Payment:createdDate");
            }
            CreatedDate = createdDate;

            if (paymentDate == null)
            {
                throw new ArgumentNullException("Payment:paymentDate");
            }
            PaymentDate = paymentDate;

            if (source == null)
            {
                throw new ArgumentNullException("Payment:source");
            }
            Source = source;

            if (type == null)
            {
                throw new ArgumentNullException("Payment:type");
            }

            Type = type;
            Locked = locked;
            OptOut = optOut;
            ModifiedDate = modifiedDate;
            PublishDate = publishDate;
            CreatedById = createdById;
            ModifiedById = modifiedById;
        }

        public virtual Beneficiary Beneficiary { get; set; }
        public Currency Currency { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Tickets { get; set; }
        public bool Locked { get; set; }
        public int PaymentID { get; set; }

        //added
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set;  }
        public PaymentSource Source { get; set; }
        public PaymentType Type { get; set; }
        public bool OptOut { get; set; }
        public string Remarks { get; set; }

        public void Publish()
        {
            Locked = true;
        }
    }
}
