using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Activity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Role> Roles { get; set; }

        public bool IsUserAllowed(List<Role> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                if (Roles.Exists(r => r.Name == userRole.Name))
                {
                    return true;
                }

            }
            return false;
        }

    }
}