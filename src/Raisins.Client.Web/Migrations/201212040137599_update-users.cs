namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System.Data.Entity.Migrations;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public partial class updateusers : DbMigration
    {
        public override void Up()
        {

            using (var db = ObjectProvider.CreateDB())
            {
                var account = db.Accounts.FirstOrDefault(a => a.UserName == "reyesce");

                if (account != null)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                }

                Account.CreateUser("reyesce", "reyesce!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Reyes, Aubrey", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                
            }

        }
        
        public override void Down()
        {
        }
    }
}
