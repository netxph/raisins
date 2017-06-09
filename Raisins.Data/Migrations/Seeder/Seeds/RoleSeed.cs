using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class RoleSeed : IDbSeeder
    {
        private RaisinsContext _context;

        public void Seed(RaisinsContext context)
        {
            _context = context;

            AddRole("Super", "payments_lock;payments_unlock;payments_create_new;payments_view_summary;" +
                "payments_create_new," +
                "payments_view_list_all;" +
                "beneficiaries_view;beneficiaries_create;beneficiaries_update;" +
                "accounts_create;accounts_edit;accounts_view;" +
                "roles_view;roles_edit;roles_create");
            AddRole("Administrator", "payments_lock;payments_unlock;payments_create_new;payments_view_summary");
            AddRole("Accountant", "payments_view_summary");
            AddRole("Manager", "payments_lock;payments_unlock;payments_create_new;payments_view_summary");
            AddRole("User", "payments_create_new");
            AddRole("SuperAccountant", "payments_view_list_all");
            AddRole("SuperAdmininstrator", "beneficiaries_view;beneficiaries_create;beneficiaries_update");
            AddRole("SuperUser", "accounts_create;accounts_edit;accounts_view");
        }

        private void AddRole(string name, string permissions)
        {
            if (!_context.Roles.Any(r => r.Name == name))
            {
                _context.Roles.Add(new Role()
                {
                    Name = name,
                    Permissions = permissions
                });
            }
            _context.SaveChanges();
        }
    }
}
