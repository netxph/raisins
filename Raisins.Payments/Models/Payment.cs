using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class Payment
    {     
        public Payment(int paymentID, string name, decimal amount, Currency currency, Beneficiary beneficiary, bool locked,
            string email, DateTime createdDate, DateTime? modifiedDate, DateTime paymentDate, DateTime? publishDate, string createdBy, string modifiedBy, PaymentSource source, PaymentType type,
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
            if (string.IsNullOrEmpty(createdBy))
            {
                throw new ArgumentNullException("Payment:createdBy");
            }
            CreatedBy = createdBy;
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
            ModifiedBy = modifiedBy;
        }

        public Beneficiary Beneficiary { get; private set; }
        public Currency Currency { get; private set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public bool Locked { get; private set; }
        public int PaymentID { get; private set; }

        //added
        public string Email { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public DateTime? PublishDate { get; private set; }
        public string CreatedBy { get; private set; }
        public string ModifiedBy { get; private set;  }
        public PaymentSource Source { get; private set; }
        public PaymentType Type { get; private set; }
        public bool OptOut { get; private set; }


        public void Publish()
        {
            Locked = true;
        }
    }
}
