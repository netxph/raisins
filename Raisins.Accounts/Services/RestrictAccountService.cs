using Raisins.Accounts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Accounts.Models;

namespace Raisins.Accounts.Services
{
    public class RestrictAccountService : IAccountService
    {

        private readonly IAccountService _service;
        protected IAccountService Service { get { return _service; } }

        public RestrictAccountService(IAccountService service)
        {
            if(service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            _service = service;
        }

        public string Authenticate(string userName, string password)
        {
            return Service.Authenticate(userName, password);
        }

        public Token Validate(string token)
        {
            return Service.Validate(token);
        }

        public void Create(Account account, AccountProfile profile)
        {
            if (account.UserName.ToLower() != "super")
            {
                Service.Create(account, profile);
            }
            else
            {
                throw new InvalidUserException(nameof(account.UserName));
            }
        }

        public void Edit(Account account, AccountProfile profile)
        {
            Service.Edit(account, profile);
        }

        public Models.Accounts GetAll()
        {
            return Service.GetAll();
        }

        public Account Get(string userName)
        {
            return Service.Get(userName);
        }
    }
}
