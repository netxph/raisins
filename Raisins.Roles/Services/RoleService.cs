using Raisins.Roles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Roles.Models;

namespace Raisins.Roles.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        protected IRoleRepository RoleRepository { get { return _roleRepository; } }
        public RoleService(IRoleRepository roleRepository)
        {
            if (roleRepository == null)
            {
                throw new ArgumentNullException("RoleService:roleRepository");
            }
            _roleRepository = roleRepository;
        }
        public void Add(D.Role role)
        {
            RoleRepository.Add(role);
        }

        public void Edit(D.Role role)
        {
            RoleRepository.Edit(role);
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
