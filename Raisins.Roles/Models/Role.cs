using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Roles.Models
{
    public class Role
    {
        private readonly HashSet<string> _permissions;

        public Role(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            _permissions = new HashSet<string>();
        }

        public Role(int roleID, string name)
        {
            if (roleID < 0)
            {
                throw new ArgumentNullException("Role:roleID");
            }
            RoleID = roleID;

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            _permissions = new HashSet<string>();
        }

        public Role(string name, string permissions)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            if (!string.IsNullOrEmpty(permissions))
            {
                IEnumerable<string> tempPermissions = permissions.Split(';');
                _permissions = new HashSet<string>();
                foreach (var permission in tempPermissions)
                {
                    AddPermission(permission);
                }
            }
        }

        public Role(int roleID, string name, string permissions)
        {
            if (roleID < 0)
            {
                throw new ArgumentNullException("Role:roleID");
            }
            RoleID = roleID;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            if (!string.IsNullOrEmpty(permissions))
            {
                IEnumerable<string> tempPermissions = permissions.Split(';');
                _permissions = new HashSet<string>();
                foreach (var permission in tempPermissions)
                {
                    AddPermission(permission);
                }
            }
        }

        public Role(int roleID, string name, IEnumerable<string> permissions)
        {
            if (roleID < 0)
            {
                throw new ArgumentNullException("Role:roleID");
            }
            RoleID = roleID;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            if (permissions == null)
            {
                throw new ArgumentNullException("Role:permissions");
            }
            _permissions = new HashSet<string>();
            foreach (var permission in permissions)
            {
                AddPermission(permission);
            }
        }

        public Role(string name, IEnumerable<string> permissions)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Role:name");
            }
            Name = name;
            if (permissions == null)
            {
                throw new ArgumentNullException("Role:permissions");
            }
            _permissions = new HashSet<string>();
            foreach (var permission in permissions)
            {
                AddPermission(permission);
            }
        }
        public Role() { }
        public int RoleID { get; private set; }

        public string Name { get; private set; }
        public IEnumerable<string> Permissions { get { return _permissions; } }

        public void AddPermission(string permission)
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _permissions.Add(permission);
            }
        }

    }
}