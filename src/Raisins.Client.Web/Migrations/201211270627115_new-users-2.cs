namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    public partial class newusers2 : DbMigration
    {
        public override void Up()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                if (!Account.Exists("remultj"))
                {
                    Account.CreateUser("remultj", "remultj!23",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "Remulta, Jared", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                }

                if (!Account.Exists("delacrl"))
                {
                    Account.CreateUser("delacrl", "delacrl!23",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "Dela Cruz, Lyndon", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                }

                if (!Account.Exists("perezan"))
                {
                    Account.CreateUser("perezan", "perezan!23",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "Perez, Andy", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                }
            }

        }
        
        public override void Down()
        {
        }
    }
}
