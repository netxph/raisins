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

        public static Payment[] GetAll()
        {
            var db = new RaisinsDB();

            return db.Payments.OrderBy((payment) => payment.Currency).ToArray();
        }

        public static decimal GetCashOnHand(string userName)
        {
            var db = new RaisinsDB();

            var account = Account.FindUser(userName);

            if (((RoleType)account.Role.RoleType) == RoleType.User)
            {
                return db.Payments
                    .Where((payment) => payment.CreatedBy.UserName == userName && !payment.Locked)
                    .Sum((payment) => payment.Amount * payment.Currency.ExchangeRate);
            }
            else
            {
                return db.Payments
                    .Where((payment) => payment.Beneficiary.BeneficiaryID == account.Setting.BeneficiaryID && !payment.Locked)
                    .Sum((payment) => payment.Amount * payment.Currency.ExchangeRate);
            }

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