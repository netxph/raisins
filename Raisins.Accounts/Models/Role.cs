using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
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

        public Role() { }

        public string Name { get; private set; }
        public IEnumerable<string> Permissions { get { return _permissions; }}

        public void AddPermission(string permission)
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _permissions.Add(permission);
            }
        }

    }
}