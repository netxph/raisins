using Raisins.Accounts.Interfaces;
using Raisins.Accounts.Models;
using System;
using System.Linq;

namespace Raisins.Data.Repository
{
    public class RestrictAccountRepository : IAccountRepository
    {
        private readonly IAccountRepository _repository;
        private const string SUPER = "super";

        protected IAccountRepository Repository { get { return _repository; } }

        public RestrictAccountRepository(IAccountRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }
        public Account Get(string userName)
        {
            if (userName != SUPER)
            {
                return Repository.Get(userName);
            }

            return null;
        }

        public Accounts.Models.Accounts GetAll()
        {
            var r = Repository.GetAll();
            var accounts = r.Where(i => i.Role.Name.ToLower() != SUPER)
                    .ToList();

            return new Accounts.Models.Accounts(accounts);
        }

        public bool Exists(string userName)
        {
            return Repository.Exists(userName);
        }

        public bool Any(string userName)
        {
            return Repository.Any(userName);
        }

        public void Add(Account account, AccountProfile profile)
        {
            //added
            if(account.UserName.ToLower() != SUPER)
            {
                Repository.Add(account, profile);
            }
        }

        public void Edit(Account account, AccountProfile profile)
        {
            Repository.Edit(account, profile);
        }

    }
}