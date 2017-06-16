using Raisins.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Accounts.Models;

namespace Raisins.Accounts.Interfaces
{
    public interface IAccountRepository
    {
        D.Account Get(string userName);

        //Account GetCurrentUserAccount();
        D.Accounts GetAll();
        bool Exists(string userName);
        bool Any(string userName);
        void Add(D.Account account, D.AccountProfile profile);
        void Edit(D.Account account, D.AccountProfile profile);
        void AddBeneficiary(D.Account account, Beneficiary beneficiary);
    }
}
