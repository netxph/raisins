using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Role
    {
        public Role()
        {
        }
        public Role(string name, string permissions)
        {
            Name = name;
            Permissions = permissions;
        }
        public Role(int roleID, string name, string permissions)
        {
            RoleID = roleID;
            Name = name;
            Permissions = permissions;
        }
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}
