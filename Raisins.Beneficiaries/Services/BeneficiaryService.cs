using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BI = Raisins.Beneficiaries.Interfaces;
using DM = Raisins.Beneficiaries.Models;
using AI = Raisins.Accounts.Interfaces;
using AM = Raisins.Accounts.Models;

namespace Raisins.Beneficiaries.Services
{
    public class BeneficiaryService : BI.IBeneficiaryService, AI.IAccountService
    {
        private readonly BI.IBeneficiaryRepository _beneficiaryRepository;
        private readonly AI.IAccountRepository _accountRepository;

        protected BI.IBeneficiaryRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }
        protected AI.IAccountRepository AccountRepository { get { return _accountRepository; } }

        public BeneficiaryService(BI.IBeneficiaryRepository beneficiaryRepository, AI.IAccountRepository accountRepository)
        {
            if (beneficiaryRepository == null)
            {
                throw new ArgumentNullException("BeneficiaryService:beneficiaryRepository");
            }
            if (accountRepository == null)
            {
                throw new ArgumentNullException("BeneficiaryService:accountRepository");
            }

            _beneficiaryRepository = beneficiaryRepository;
            _accountRepository = accountRepository;
        }

        public void Add(DM.Beneficiary beneficiary)
        {
            BeneficiaryRepository.Add(beneficiary);

            AccountRepository.AddBeneficiary(AccountRepository.Get("Super"), Convert(beneficiary));
        }

        public void Edit(DM.Beneficiary beneficiary)
        {
            BeneficiaryRepository.Edit(beneficiary);
        }

        public DM.Beneficiary Get(string name)
        {
            return BeneficiaryRepository.Get(name);
        }

        public DM.Beneficiary Get(int beneficiaryID)
        {
            return BeneficiaryRepository.Get(beneficiaryID);
        }

        public DM.Beneficiaries GetAll()
        {
            return BeneficiaryRepository.GetAll();
        }

        public string Authenticate(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public AM.Token Validate(string token)
        {
            throw new NotImplementedException();
        }

        public void Create(AM.Account account, AM.AccountProfile profile)
        {
            throw new NotImplementedException();
        }

        public void Edit(AM.Account account, AM.AccountProfile profile)
        {
            throw new NotImplementedException();
        }

        Accounts.Models.Accounts AI.IAccountService.GetAll()
        {
            throw new NotImplementedException();
        }

        AM.Account AI.IAccountService.Get(string userName)
        {
            throw new NotImplementedException();
        }

        public Accounts.Models.Beneficiary Convert(Beneficiaries.Models.Beneficiary beneficiary)
        {
            return new Accounts.Models.Beneficiary(beneficiary.Name, beneficiary.BeneficiaryID, beneficiary.Description);
        }
    }
}
