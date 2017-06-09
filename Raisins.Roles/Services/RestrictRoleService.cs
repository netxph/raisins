using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Roles.Interfaces;
using Raisins.Roles.Models;
using Raisins.Roles;

namespace Raisins.Accounts.Services
{
    public class RestrictRoleService : IRoleService
    {
        private readonly IRoleService _service;
        protected IRoleService Service { get { return _service; } }

        public RestrictRoleService(IRoleService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            _service = service;
        }

        public void Add(Role role)
        {
            if (role.Name.ToLower() != "super")
            {
                Service.Add(role);
            }
            else
            {
                throw new InvalidRoleException(nameof(role.Name));
            }
        }

        public void Edit(Role role)
        {
            Service.Edit(role);
        }

        public Role Get(string Name)
        {
            return Service.Get(Name);
        }

        public Role Get(int roleID)
        {
            return Service.Get(roleID);
        }

        public Roles.Models.Roles GetList()
        {
            return Service.GetList();
        }
    }
}
