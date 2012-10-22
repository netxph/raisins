using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class BeneficiaryService
    {

        protected RaisinsDB DB { get; set; }

        public BeneficiaryService()
            : this(new RaisinsDB())
        {

        }

        public BeneficiaryService(RaisinsDB db)
        {
            DB = db;
        }

        public List<Beneficiary> GetAll()
        {
            return DB.Beneficiaries.ToList();
        }

        public Beneficiary Find(int id = 0)
        {
            return DB.Beneficiaries.Find(id);
        }

        public Beneficiary Add(Beneficiary Beneficiary)
        {
            DB.Beneficiaries.Add(Beneficiary);
            DB.SaveChanges();

            return Beneficiary;
        }

        public Beneficiary Edit(Beneficiary Beneficiary)
        {
            DB.Entry(Beneficiary).State = EntityState.Modified;
            DB.SaveChanges();

            return Beneficiary;
        }

        public void Delete(int id)
        {
            var Beneficiary = Find(id);

            DB.Beneficiaries.Remove(Beneficiary);
            DB.SaveChanges();
        }

    }
}