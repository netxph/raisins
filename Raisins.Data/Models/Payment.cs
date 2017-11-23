using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Payment
    {
        public Payment()
        {
        }

        public Payment(string name, decimal amount, int beneficiaryID, int currencyID, string email,
            DateTime createdDate, DateTime paymentDate, DateTime modifiedDate, int createdByID, int sourceID, int typeID, bool optOut, int modifiedByID)
        {
            Name = name;
            Amount = amount;
            BeneficiaryID = beneficiaryID;
            CurrencyID = currencyID;
            Email = email;
            CreatedDate = createdDate;
            PaymentDate = paymentDate;
            ModifiedDate = createdDate;
            CreatedByID = createdByID;
            PaymentSourceID = sourceID;
            PaymentTypeID = typeID;
            OptOut = optOut;
            ModifiedByID = modifiedByID;
        }

        public Payment(int paymentID, string name, decimal amount, int beneficiaryID, int currencyID, bool locked,
            string email, DateTime createdDate, DateTime paymentDate, DateTime? modifiedDate, DateTime? publishDate,
            int createdByID, int modifiedByID, int sourceID, int typeID, bool optOut)
        {
            PaymentID = paymentID;
            Name = name;
            Amount = amount;
            BeneficiaryID = beneficiaryID;
            CurrencyID = currencyID;
            Locked = locked;
            Email = email;
            CreatedDate = createdDate;
            PaymentDate = paymentDate;
            //DateTime convertedModifiedDate = (DateTime)modifiedDate;
            //convertedModifiedDate = convertedModifiedDate.ToLocalTime();
            //ModifiedDate = convertedModifiedDate;
            //DateTime convertedPublishDate = (DateTime)publishDate;
            //convertedPublishDate = convertedPublishDate.ToLocalTime();
            //PublishDate = convertedPublishDate;
            ModifiedDate = modifiedDate;
            PublishDate = publishDate;
            CreatedByID = createdByID;
            ModifiedByID = modifiedByID;
            PaymentSourceID = sourceID;
            PaymentTypeID = typeID;
            OptOut = optOut;
        }

        [Key]
        public int PaymentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public bool Locked { get; set; }
        public bool OptOut { get; set; }
        public int BeneficiaryID { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }
        public int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }
        public int CreatedByID { get; set; }
        public int ModifiedByID { get; set; }

        //TODO to be deleted. Needs to update database
        public virtual Account CreatedBy { get; set; }
        public virtual Account ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public int PaymentSourceID { get; set; }
        public virtual PaymentSource PaymentSource { get; set; }
        public int PaymentTypeID { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
