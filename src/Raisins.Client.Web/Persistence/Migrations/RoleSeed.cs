using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Linq;

namespace Raisins.Client.Web.Migrations
{
    public class RoleSeed : IDbSeeder
    {
        public void Seed(RaisinsDB context)
        {
            //Role
            AddRole(context, "Administrator");
            AddRole(context, "Accountant");
            AddRole(context, "Manager");
            AddRole(context, "User");
        }

        private void AddRole(RaisinsDB context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                context.Roles.Add(new Role() { Name = roleName });
            }
        }
    }
}