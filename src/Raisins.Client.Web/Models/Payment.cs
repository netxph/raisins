using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Raisins.Client.Web.Data;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        #region Properties

        public int PaymentID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0.0D, double.MaxValue, ErrorMessage="Amount must be greater than zero")]
        public decimal Amount { get; set; }

        public string Location { get; set; }
        public Currency Currency { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Locked { get; set; }

        [Required]
        public int Class { get; set; }
        public string Remarks { get; set; }
        public Account AuditedBy { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Account CreatedBy { get; set; }
        public Beneficiary Beneficiary { get; set; }
 
        #endregion

        #region Methods

        public static Payment[] GetAll()
        {
            var db = new RaisinsDB();

            return db.Payments.Include("Beneficiary").Include("Currency").OrderBy(payment => payment.Currency.CurrencyCode).ToArray();
        }

        public static decimal GetCashOnHand()
        {
            decimal result = 0.0m;

            var db = new RaisinsDB();

            if (((RoleType)Account.CurrentUser.RoleType) == RoleType.User)
            {
                result = db.Payments
                    .Where((payment) => payment.CreatedBy.UserName == Account.CurrentUser.UserName && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }
            else
            {
                result = db.Payments
                    .Where((payment) => payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }

            return result;

        }

        public static Payment SetDefaults(Payment payment)
        {
            if (Account.CurrentUser != null)
            {
                payment.CreatedBy = Account.CurrentUser;

                if (Account.CurrentUser.Setting != null)
                {
                    payment.Location = Account.CurrentUser.Setting.Location;
                }
            }

            payment.Beneficiary = Beneficiary.Get(payment.Beneficiary.BeneficiaryID);
            payment.Currency = Currency.Get(payment.Currency.CurrencyID);

            return payment;
        }

        public static bool CreateNew(Payment item)
        {
            SetDefaults(item);

            RaisinsDB.Instance.Payments.Add(item);

            RaisinsDB.Instance.SaveChanges();

            return true;
        }

        public static bool Update(Payment item)
        {
            var payment = Get(item.PaymentID);
            SetDefaults(item);

            payment.Beneficiary = item.Beneficiary;
            payment.Amount = item.Amount;
            payment.Currency = item.Currency;
            payment.Class = item.Class;
            payment.Name = item.Name;
            payment.Email = item.Email;
            payment.Remarks = item.Remarks;

            RaisinsDB.Instance.SaveChanges();

            return true;
        }

        #endregion


        public static bool Delete(int id)
        {
            RaisinsDB.Instance.Payments.Remove(RaisinsDB.Instance.Payments.Where(payment => payment.PaymentID == id).FirstOrDefault());
            RaisinsDB.Instance.SaveChanges();

            return true;
        }

        public static Payment Get(int id)
        {
            return RaisinsDB.Instance.Payments.Where(payment => payment.PaymentID == id).FirstOrDefault();
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