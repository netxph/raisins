using Raisins.Roles.Interfaces;
using D = Raisins.Roles.Models;
using EF = Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Raisins.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private RaisinsContext _context;
        public RoleRepository() : this(RaisinsContext.Instance)
        {
        }

        public RoleRepository(RaisinsContext context)
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
            return ConvertToDomainList(_context.Roles);
        }
        public void Add(D.Role role)
        {
            EF.Role efRole = ConvertToEF(role);
            
            _context.Roles.Add(efRole);
            _context.SaveChanges();
        }
        public void Edit(D.Role role)
        {
            EF.Role efRole = ConvertToEFwithID(role);
            _context.Entry(efRole).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Any(string roleName)
        {
            return _context.Roles.Any(a => a.Name == roleName);
        }

        private D.Role ConverToDomain(EF.Role efRole)
        {
            D.Role role = new D.Role(efRole.RoleID, efRole.Name);
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
        private EF.Role ConvertToEF(D.Role role)
        {
            return new EF.Role(role.Name, string.Join(";", role.Permissions));
        }
        private EF.Role ConvertToEFwithID(D.Role role)
        {
            return new EF.Role(role.RoleID, role.Name, string.Join(";", role.Permissions));
        }        
    }
}