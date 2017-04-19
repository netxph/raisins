using Raisins.Accounts.Interfaces;
using Raisins.Accounts.Models;
using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly ICryptProvider _cryptProvider;
        private readonly IDateProvider _dateProvider;

        protected IAccountRepository AccountRepository { get { return _accountRepository; } }
        protected ICryptProvider CryptProvider { get { return _cryptProvider; } }
        protected IDateProvider DateProvider { get { return _dateProvider; } }

        public AccountService(ICryptProvider cryptProvider, IDateProvider dateProvider, IAccountRepository accountRepository)
        {
            if (cryptProvider == null)
            {
                throw new ArgumentNullException("AccountService:cryptProvider");
            }

            _cryptProvider = cryptProvider;

            if (dateProvider == null)
            {
                throw new ArgumentNullException("AccountService:dateProvider");
            }

            _dateProvider = dateProvider;

            if (accountRepository == null)
            {
                throw new ArgumentNullException("AccountService:accountRepository");
            }

            _accountRepository = accountRepository;
        }

        public string Authenticate(string userName, string password)
        {
            var account = AccountRepository.Get(userName);

            if (account != null)
            {
                if (account.ValidatePassword(CryptProvider, password))
                {
                    return CryptProvider.Encrypt(account.CreateToken().GenerateString(DateProvider));
                }
            }
            return string.Empty;
        }

        public Token Validate(string token)
        {
            return new TokenParser(CryptProvider.Decrypt(token)).GetToken();
        }

        public void Create(Account account, AccountProfile profile)
        {
            AccountRepository.Add(account, profile);
        }

        public void Edit(Account account, AccountProfile profile)
        {
            AccountRepository.Edit(account, profile);
        }

        public Models.Accounts GetAll()
        {
            return AccountRepository.GetAll();
        }
        public Models.Account Get(string userName)
        {
            return AccountRepository.Get(userName);
        }
    }
}
