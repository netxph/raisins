using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        public RaisinsDB _raisinsDb { get; set; }

        public BeneficiaryRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }

        public IEnumerable<Beneficiary> GetAll()
        {
            return _raisinsDb.Beneficiaries;
        }

        public Beneficiary Find(int id = 0)
        {
            return _raisinsDb.Beneficiaries.Find(id);
            
        }

        public void Add(Beneficiary beneficiary)
        {
            _raisinsDb.Beneficiaries.Add(beneficiary);
        }

        public void Edit(Beneficiary beneficiary)
        {
            _raisinsDb.Entry(beneficiary).State = EntityState.Modified;
        }

        public void MultipleEdit(IEnumerable<Beneficiary> beneficiaries)
        {
            foreach (Beneficiary beneficiary in beneficiaries)
            {
                _raisinsDb.Entry<Beneficiary>(beneficiary).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            Beneficiary Beneficiary = Find(id); 
            _raisinsDb.Beneficiaries.Remove(Beneficiary);
        }

        public bool Any(string name)
        {
            return _raisinsDb.Beneficiaries.Any(b => b.Name == name);
        }
    }
}