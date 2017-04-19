using Raisins.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Interfaces
{
    public interface IRoleService
    {
        Role Get(string Name);
        Role Get(int roleID);
        Roles GetList();
    }
}
