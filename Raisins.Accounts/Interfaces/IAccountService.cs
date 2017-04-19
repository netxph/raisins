using D = Raisins.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Interfaces
{
    public interface IAccountService
    {
        string Authenticate(string userName, string password);
        D.Token Validate(string token);
        void Create(D.Account account, D.AccountProfile profile);
        void Edit(D.Account account, D.AccountProfile profile);
        D.Accounts GetAll();
        D.Account Get(string userName);
    }
}
