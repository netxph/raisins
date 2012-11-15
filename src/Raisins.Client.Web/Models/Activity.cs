using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Activity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Role> Roles { get; set; }

        public static bool IsInRole(string activityName)
        {
            return IsInRole(activityName, Account.GetCurrentUser().Roles);
        }

        public static bool IsInRole(string activityName, List<Role> roles)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var activity = db.Activities.Include("Roles").FirstOrDefault(a => a.Name == activityName);

                if (activity != null)
                {
                    var activityRoles = activity.Roles;

                    foreach (var userRole in roles)
                    {
                        if (activityRoles.Exists(r => r.Name == userRole.Name))
                        {
                            return true;
                        }

                    }

                    return false;
                }

                //if no activities defined. allow user
                return true;
            }
        }

        public static Activity Add(Activity activity)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Activities.Add(activity);
                db.SaveChanges();

                return activity;
            }
        }

    }
}