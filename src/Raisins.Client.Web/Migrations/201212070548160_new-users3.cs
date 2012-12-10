namespace Raisins.Client.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity;
    using Raisins.Client.Web.Models;
    using System.Collections.Generic;

    public partial class newusers3 : DbMigration
    {
        public override void Up()
        {

            using (var db = ObjectProvider.CreateDB())
            {
                if (!Account.Exists("ruetase"))
                {
                    Account.CreateUser("ruetase", "ruetase!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Ruetas, Elizabeth", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                }
            }

        }
        
        public override void Down()
        {
        }
    }
}
