using D = Raisins.Roles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Roles.Interfaces
{
    public interface IRoleRepository
    {
        D.Role Get(string Name);
        D.Role Get(int roleID);
        D.Roles GetList();
        void Add(D.Role role);
        void Edit(D.Role role);
        bool Any(string roleName);
    }
}
