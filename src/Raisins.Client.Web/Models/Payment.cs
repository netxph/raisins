using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

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

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Remarks { get; set; }

        public List<Ticket> Tickets { get; set; }

        [EnumDataType(typeof(PaymentClass))]
        public PaymentClass Class { get; set; }
        
        public bool Locked { get; set; }

        [Required]
        public int BeneficiaryID { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }

        [Required]
        public int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }

        [Required]
        public int CreatedByID { get; set; }
        public virtual Account CreatedBy { get; set; }

        public int? AuditedByID { get; set; }
        public virtual Account AuditedBy { get; set; }

        public static List<Payment> GetAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var payments = db.Payments.Include(p => p.Beneficiary).Include(p => p.Currency).Include(p => p.CreatedBy).Include(p => p.AuditedBy);

                return payments.ToList();
            }
        }

        public static Payment Find(int id = 0)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Payments.Find(id);
            }
        }

        public static Payment Add(Payment payment)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                payment.CreatedByID = Account.GetCurrentUser().ID;

                db.Payments.Add(payment);
                db.SaveChanges();

                return payment;
            }
        }

        public static Payment Edit(Payment payment)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();

                return payment;
            }
        }

        public static void Delete(int id)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Payment payment = db.Payments.Find(id);
                db.Payments.Remove(payment);
                db.SaveChanges();
            }
        }

    }
}