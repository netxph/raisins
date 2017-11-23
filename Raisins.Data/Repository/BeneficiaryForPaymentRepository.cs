using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class BeneficiaryForPaymentRepository : IBeneficiaryForPaymentRepository
    {
        public RaisinsContext _context { get; set; }
        public BeneficiaryForPaymentRepository() : this(RaisinsContext.Instance)
        {
        }
        public BeneficiaryForPaymentRepository(RaisinsContext context)
        {
            _context = context;
        }

        public IEnumerable<Beneficiary> GetAll()
        {
            return ConvertList(_context.Beneficiaries);
        }

        public Beneficiary Find(int id = 0)
        {
            return ConvertToDomain(_context.Beneficiaries.Find(id));

        }

        public Beneficiary GetBeneficiary(string name)
        {
            return ConvertToDomain(_context.Beneficiaries
                   .FirstOrDefault(a => a.Name == name));
        }

        public void Add(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(ConvertToEF(beneficiary));
        }

        public void Edit(Beneficiary beneficiary)
        {
            _context.Entry(ConvertToEF(beneficiary)).State = EntityState.Modified;
        }

        public void Delete(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Remove(ConvertToEF(beneficiary));
        }

        private Beneficiary ConvertToDomain(DATA.Beneficiary efBeneficiary)
        {
            return new Beneficiary(efBeneficiary.Name, efBeneficiary.Description);
        }

        private IEnumerable<Beneficiary> ConvertList(IEnumerable<DATA.Beneficiary> efBeneficiaries)
        {
            List<Beneficiary> beneficiaries = new List<Beneficiary>();
            foreach (var efBeneficiary in efBeneficiaries)
            {
                beneficiaries.Add(ConvertToDomain(efBeneficiary));
            }
            return beneficiaries;
        }
        private DATA.Beneficiary ConvertToEF(Beneficiary beneficiary)
        {
            return new DATA.Beneficiary(beneficiary.Name, beneficiary.Description);
        }
    }
}