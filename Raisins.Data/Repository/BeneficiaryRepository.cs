using Raisins.Beneficiaries.Interfaces;
using EF = Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Beneficiaries.Models;
using System.Data.Entity;

namespace Raisins.Data.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        public RaisinsContext _context { get; set; }
        public BeneficiaryRepository() : this(RaisinsContext.Instance)
        {
        }
        public BeneficiaryRepository(RaisinsContext context)
        {
            _context = context;
        }

        public void Add(D.Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(ConvertToEF(beneficiary));
            _context.SaveChanges();
        }

        public void Delete(D.Beneficiary beneficiary)
        {
            _context.Beneficiaries.Remove(ConvertToEF(beneficiary));
            _context.SaveChanges();
        }

        public void Edit(D.Beneficiary beneficiary)
        {
            EF.Beneficiary efBeneficiary = ConvertToEFwithID(beneficiary);


            _context.Entry(efBeneficiary).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public D.Beneficiary Get(string name)
        {
            return ConvertToDomain(_context.Beneficiaries
                   .FirstOrDefault(a => a.Name == name));
        }

        public D.Beneficiary Get(int beneficiaryID)
        {
            return ConvertToDomain(_context.Beneficiaries.Find(beneficiaryID));
        }

        public D.Beneficiaries GetAll()
        {
            return ConvertToDomainList(_context.Beneficiaries);
        }

        private D.Beneficiary ConvertToDomain(EF.Beneficiary efBeneficiary)
        {
            return new D.Beneficiary(efBeneficiary.BeneficiaryID, efBeneficiary.Name, efBeneficiary.Description);
        }

        private D.Beneficiaries ConvertToDomainList(IEnumerable<EF.Beneficiary> efBeneficiaries)
        {
            D.Beneficiaries beneficiaries = new D.Beneficiaries();
            foreach (var efBeneficiary in efBeneficiaries)
            {
                beneficiaries.Add(ConvertToDomain(efBeneficiary));
            }
            return beneficiaries;
        }

        private EF.Beneficiary ConvertToEF(D.Beneficiary beneficiary)
        {
            return new EF.Beneficiary(beneficiary.Name, beneficiary.Description);
        }
        private EF.Beneficiary ConvertToEFwithID(D.Beneficiary beneficiary)
        {
            return new EF.Beneficiary(beneficiary.BeneficiaryID, beneficiary.Name, beneficiary.Description);
        }

    }
}
