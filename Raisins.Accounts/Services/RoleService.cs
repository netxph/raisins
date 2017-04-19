using Raisins.Accounts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Accounts.Models;

namespace Raisins.Accounts.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleForAccountRepository _roleRepository;
        protected IRoleForAccountRepository RoleRepository { get { return _roleRepository; } }
        public RoleService(IRoleForAccountRepository roleRepository)
        {
            if (roleRepository == null)
            {
                throw new ArgumentNullException("RoleService:roleRepository");
            }
            _roleRepository = roleRepository;
        }
        public D.Role Get(int roleID)
        {
            return RoleRepository.Get(roleID);
        }

        public D.Role Get(string name)
        {
            return RoleRepository.Get(name);
        }

        public D.Roles GetList()
        {
            return RoleRepository.GetList();
        }
    }
}
