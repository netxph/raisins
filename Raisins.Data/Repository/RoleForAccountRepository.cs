using Raisins.Accounts.Interfaces;
using D = Raisins.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class RoleForAccountRepository : IRoleForAccountRepository
    {
        private RaisinsContext _context;
        public RoleForAccountRepository() : this(RaisinsContext.Instance)
        {
        }

        public RoleForAccountRepository(RaisinsContext context)
        {
            _context = context;
        }

        public D.Role Get(string roleName)
        {
            return ConverToDomain(_context.Roles.FirstOrDefault(r => r.Name == roleName));
        }
        public D.Role Get(int roleID)
        {
            return ConverToDomain(_context.Roles.FirstOrDefault(r => r.RoleID == roleID));
        }

        public D.Roles GetList()
        {
            return ConvertToDomainList(_context.Roles.Where(r => r.Name == "super").ToList());
        }
        public void Add(D.Role role)
        {
            EF.Role efRole = new EF.Role(role.Name, String.Join(";", role.Permissions));   
            _context.Roles.Add(efRole);
        }

        public bool Any(string roleName)
        {
            return _context.Roles.Any(a => a.Name == roleName);
        }

        private D.Role ConverToDomain(EF.Role efRole)
        {
            D.Role role = new D.Role(efRole.Name);
            IEnumerable<string> permissions = efRole.Permissions.Split(';');
            foreach (var permission in permissions)
            {
                role.AddPermission(permission);
            }
            return role;
        }
        private D.Roles ConvertToDomainList(IEnumerable<EF.Role> efRoles)
        {
            D.Roles roles = new D.Roles();
            foreach (var efRole in efRoles)
            {
                roles.Add(ConverToDomain(efRole));
            }
            return roles;
        }        
    }
}