using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.ViewModels
{
    public class RoleListViewModel
    {
        public List<Role> Roles { get; set; }
        public RoleListViewModel()
        {
        }
        public RoleListViewModel(List<Role> roles)
        {
            Roles = roles;
        }
    }

    public class RoleEditViewModel
    {
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        [Required]
        public string Permissions { get; set; }

        public List<string> PermissionStrings { get; set; }

        public RoleEditViewModel(Role role)
        {
            System.Diagnostics.Debugger.Launch();

            RoleID = role.RoleID;
            Name = role.Name;
            Permissions = string.Join(", ", role.Permissions);
            PermissionStrings = role.Permissions ?? new List<string>();
        }

        public RoleEditViewModel()
        {
        }

        public IEnumerable<string> Convert()
        {
            //return Array.ConvertAll(
            //    Permissions.Split(new char[] { ',', ';' },
            //    StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
            return PermissionStrings;
        }
    }
}