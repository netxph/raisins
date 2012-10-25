using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Role
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public static Role Add(Role role)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Roles.Add(role);
                db.SaveChanges();

                return role;
            }
        }


        public static Role Find(string roleName)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Roles.FirstOrDefault(r => r.Name == roleName);
            }
        }
    }
}
