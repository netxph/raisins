using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IAccountRepository
    {
        Account GetUserAccount(string userName);
        Account GetCurrentUserAccount();
        bool Exists(string userName);
        bool Any(string userName);
        void Add(Account account);
        void Edit(Account account);
    }
}