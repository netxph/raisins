using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Roles.Models
{
    public class Roles : IEnumerable<Role>
    {
        private readonly List<Role> _roles;
        public Roles()
        {
            _roles = new List<Role>();
        }
        public void AddRange(IEnumerable<Role> roles)
        {
            _roles.AddRange(roles);
        }
        public void Add(Role role)
        {
            _roles.Add(role);
        }

        public IEnumerator<Role> GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _roles.GetEnumerator();
        }

    }
}
