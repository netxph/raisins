using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private RaisinsDB _raisinsDb;

        public AccountRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }

        public Account GetUserAccount(string userName)
        {
            return _raisinsDb.Accounts
                    .Include(a => a.Roles)
                    .Include(a => a.Profile)
                    .Include(a => a.Profile.Beneficiaries)
                    .Include(a => a.Profile.Currencies)
                    .FirstOrDefault(a => a.UserName == userName);
        }

        public Account GetCurrentUserAccount()
        {
            var http = ObjectProvider.CreateHttpHelper();
            var userName = http.GetCurrentUserName();
            return GetUserAccount(userName);
        }

        public bool Exists(string userName)
        {
            if (GetUserAccount(userName) == null) return false;
            else return true;
        }

        public void Add(Account account)
        {
            _raisinsDb.Accounts.Add(account);
        }

        public void Edit(Account account)
        {
            _raisinsDb.Entry(account).State = EntityState.Modified;
        }

        public bool Any(string userName)
        {
            return _raisinsDb.Accounts.Any(a => a.UserName == userName);
        }
    }
}