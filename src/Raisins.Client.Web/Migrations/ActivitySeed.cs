using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raisins.Client.Web.Models;


namespace Raisins.Client.Web.Migrations
{
    public class ActivitySeed : IDbSeeder
    {
        public void Seed(RaisinsDB context)
        {
            AddActivity(context, "Payment.Edit", "Administrator", "User");
            AddActivity(context, "Payment.Lock", "Administrator", "Accountant");
            AddActivity(context, "Home.Dashboard", "Administrator");
        }

        private static void AddActivity(RaisinsDB context, string activityName, params string[] roleNames)
        {
            if (!context.Activities.Any(a => a.Name == activityName))
            {
                Activity newActivity = new Activity() { Name = activityName };
                List<Role> roles = new List<Role>();
                
                foreach (string roleName in roleNames)
                {
                    roles.Add(Role.Find(roleName));
                }

                newActivity.Roles = roles;

                Activity.Add(newActivity);
            }
        }
    }
}