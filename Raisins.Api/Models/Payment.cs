using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Api.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool Locked { get; set; }
        public int BeneficiaryID { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }
        public int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }

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
        public string Remarks { get; set; }
    }
}