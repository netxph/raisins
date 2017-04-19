using Raisins.Beneficiaries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Beneficiaries.Models;

namespace Raisins.Beneficiaries.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        protected IBeneficiaryRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            if (beneficiaryRepository == null)
            {
                throw new ArgumentNullException("BeneficiaryService:beneficiaryRepository");
            }

            _beneficiaryRepository = beneficiaryRepository;
        }
        public void Add(D.Beneficiary beneficiary)
        {
            BeneficiaryRepository.Add(beneficiary);
        }

        public void Edit(D.Beneficiary beneficiary)
        {
            BeneficiaryRepository.Edit(beneficiary);
        }

        public D.Beneficiary Get(string name)
        {
            return BeneficiaryRepository.Get(name);
        }

        public D.Beneficiary Get(int beneficiaryID)
        {
            return BeneficiaryRepository.Get(beneficiaryID);
        }

        public D.Beneficiaries GetAll()
        {
            return BeneficiaryRepository.GetAll();
        }
    }
}
