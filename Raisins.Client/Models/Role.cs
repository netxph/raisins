using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Role
    {
        public Role(string name)
        {
            Name = name;
        }
        public Role(int roleID, string name, IEnumerable<string> permissions)
        {
            RoleID = roleID;
            Name = name;
            Permissions = permissions.ToList();
        }
        public Role() { }
        public int RoleID { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}