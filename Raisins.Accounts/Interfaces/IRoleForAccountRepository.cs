using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Accounts.Models;

namespace Raisins.Accounts.Interfaces
{
    public interface IRoleForAccountRepository
    {
        Role Get(string Name);
        Role Get(int roleID);
        Roles GetList();
        void Add(Role role);
        bool Any(string roleName);
    }
}
 