namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;
    
    public partial class newactivities : DbMigration
    {
        public override void Up()
        {

            using (var db = ObjectProvider.CreateDB())
            {
                var activity = db.Activities.SingleOrDefault(a => a.Name == "Home.Dashboard");

                if (activity == null)
                {
                    var dashboardActivity = new Activity() { Name = "Home.Dashboard", Roles = new List<Role>() { db.Roles.First(r => r.Name == "Administrator") } };

                    db.Activities.Add(dashboardActivity);
                    db.SaveChanges();
                }
            }

        }
        
        public override void Down()
        {
        }
    }
}
