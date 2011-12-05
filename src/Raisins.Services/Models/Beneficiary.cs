using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services.Data;

namespace Raisins.Services.Models
{
    public class Beneficiary
    {
        public int BeneficiaryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public static Beneficiary Get(int id)
        {
            RaisinsDB db = new RaisinsDB();

            return db.Beneficiaries.Where(beneficiary => beneficiary.BeneficiaryID == id).FirstOrDefault();
        }

        public static Beneficiary[] GetAllForPayment()
        {
            

            if (Account.CurrentUser != null)
            {
                if (Account.CurrentUser.Setting != null && Account.CurrentUser.Setting.BeneficiaryID > 0)
                {
                    return new Beneficiary[] { Get(Account.CurrentUser.Setting.BeneficiaryID) };
                }
                else
                {
                    RaisinsDB db = new RaisinsDB();
                    return db.Beneficiaries.DefaultIfEmpty().ToArray();
                }
            }
            else
            {
                return null;
            }
        }

        public static Beneficiary[] GetAll()
        {
            RaisinsDB db = new RaisinsDB();

            return db.Beneficiaries.DefaultIfEmpty().ToArray();
        }

        public static Beneficiary[] GetAllForReport()
        {
            RaisinsDB db = new RaisinsDB();

            if (Account.CurrentUser.RoleType == (int)RoleType.Administrator)
            {
                return db.Beneficiaries.Include("Payments").Include("Payments.Tickets").Include("Payments.Currency").ToArray();
            }
            else
            {
                return db.Beneficiaries.Include("Payments").Include("Payments.Tickets").Include("Payments.Currency").Where(b => b.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID).ToArray();
            }
        }
    }
}