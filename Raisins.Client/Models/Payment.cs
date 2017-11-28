using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Payment
    {
        public Payment(string name, decimal amount, Currency currency, Beneficiary beneficiary)
        {
            Name = name;
            Amount = amount;
            Beneficiary = beneficiary;
            Currency = currency;
        }
        public Payment(int paymentID, string name, decimal amount, Currency currency, Beneficiary beneficiary)
        {
            PaymentID = paymentID;
            Name = name;
            Amount = amount;
            Beneficiary = beneficiary;
            Currency = currency;
        }
        public Payment(string name, decimal amount, Currency currency, Beneficiary beneficiary,
            string email, DateTime createdDate, DateTime paymentDate, string createdBy, PaymentSource source, PaymentType type, bool optOut)
        {
            Name = name;
            Amount = amount;
            Beneficiary = beneficiary;
            Currency = currency;
            Email = email;
            CreatedDate = createdDate;
            PaymentDate = paymentDate;
            ModifiedDate = createdDate;
            CreatedBy = createdBy;
            Source = source;
            Type = type;
            OptOut = optOut;
        }

        public Payment(int paymentID, string name, decimal amount, Currency currency, Beneficiary beneficiary,
            string email, DateTime createdDate, DateTime? modifiedDate, DateTime paymentDate, string createdBy, 
            string modifiedBy, PaymentSource source, PaymentType type, bool optOut)
        {
            PaymentID = paymentID;
            Name = name;
            Amount = amount;
            Beneficiary = beneficiary;
            Currency = currency;
            Email = email;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            PaymentDate = paymentDate;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
            Source = source;
            Type = type;
            OptOut = optOut;
        }

        public Payment(string name, decimal amount, Currency currency, Beneficiary beneficiary,
            string email, DateTime createdDate, DateTime paymentDate, string createdBy, PaymentSource source, PaymentType type, string optOut)
        {
            Name = name;
            Amount = amount;
            Beneficiary = beneficiary;
            Currency = currency;
            Email = email;
            CreatedDate = createdDate;
            PaymentDate = paymentDate;
            CreatedBy = createdBy;
            Source = source;
            Type = type;
            OptOutStatus = optOut;
        }

        public Payment() { }

        public virtual Beneficiary Beneficiary { get; set; }
        public virtual Currency Currency { get; set; }

        [Display(Name = "Donor's Name")]
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
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public PaymentSource Source { get; set; }
        public PaymentType Type { get; set; }
        public bool OptOut { get; set; }
        public string OptOutStatus { get; set; }
        public string Remarks { get; set; }
    }
}