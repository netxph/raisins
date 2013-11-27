using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Migrations
{
    public class RoleSeed : IDbSeeder
    {
        public void Seed(Models.RaisinsDB context)
        {
            //Role
            AddRole(context, "Administrator");
            AddRole(context, "Accountant");
            AddRole(context, "Manager");
            AddRole(context, "User");
        }

        private void AddRole(Models.RaisinsDB context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                Role.Add(new Role() { Name = roleName });
            }
        }
    }
}