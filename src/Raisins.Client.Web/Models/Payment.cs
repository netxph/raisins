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

        public long PaymentID { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }

        public Currency Currency { get; set; }

        public string Email { get; set; }

        public bool Locked { get; set; }

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

        public static decimal GetCashOnHand(string userName)
        {
            decimal result = 0.0m;

            var db = new RaisinsDB();

            var account = Account.FindUser(userName);

            if (((RoleType)account.RoleType) == RoleType.User)
            {
                result = db.Payments
                    .Where((payment) => payment.CreatedBy.UserName == userName && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }
            else
            {
                result = db.Payments
                    .Where((payment) => payment.Beneficiary.BeneficiaryID == account.Setting.BeneficiaryID && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }

            return result;

        }

        public static bool Add(Payment payment)
        {

            RaisinsDB.Instance.Payments.Add(payment);

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