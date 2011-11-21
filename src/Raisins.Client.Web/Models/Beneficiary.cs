using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Data;

namespace Raisins.Client.Web.Models
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
    }
}