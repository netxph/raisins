using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.0D, double.MaxValue, ErrorMessage="Amount must be greater than zero")]
        public decimal Amount { get; set; }

        public string Location { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Remarks { get; set; }

        public int AuditedByID 
        { 
            get 
            { 
                #if DEBUG
                if (AuditedBy == null)
                {
                    AuditedBy = new Account() { ID = 1, UserName = "admin", Password = "R@isin5"};
                }
                #endif
                return AuditedBy.ID; 

            } 
        }
        public int CreatedByID 
        { 
            get 
            { 
                #if DEBUG
                if (CreatedBy == null)
                {
                    CreatedBy = new Account() { ID = 1, UserName = "admin", Password = "R@isin5" };
                }
                #endif 
                return CreatedBy.ID; 
            } 
        }

        public virtual Account AuditedBy { get; set; }
        public virtual Account CreatedBy { get; set; }

        [Required]
        public Beneficiary Beneficiary { get; set; }

        public List<Ticket> Tickets { get; set; }

        [EnumDataType(typeof(PaymentClass))]
        public PaymentClass Class { get; set; }
        public bool Locked { get; set; }

    }
}