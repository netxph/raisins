using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role Get(string roleName);
        Role Find(int roleId);
        void Add(Role role);
        void Edit(Role role);
        void MultipleEdit(IEnumerable<Role> roles);
        bool Any(string roleName);
    }
}