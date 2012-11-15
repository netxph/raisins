namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    public partial class newusers : DbMigration
    {
        public override void Up()
        {
            using (var db = ObjectProvider.CreateDB())
            {

                if (!Account.Exists("reyesce"))
                {
                    Account.CreateUser("reyesce", "reyesce!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Reyes, Aubrey", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("delacle"))
                {
                    Account.CreateUser("delacle", "delacle!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Dela Cruz, Lei", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("pasquij"))
                {
                    Account.CreateUser("pasquij", "pasquij!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Pasquin, John", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("bolalie"))
                {
                    Account.CreateUser("bolalie", "bolalie!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Bolalin, Lian", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("angkiko"))
                {
                    Account.CreateUser("angkiko", "angkiko!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Angkiko, Youmi", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Saboteurs").ToList() });
                }

                if (!Account.Exists("bisaron"))
                {
                    Account.CreateUser("bisaron", "bisaron!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Bisa, Rona", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                }

                if (!Account.Exists("santiaa"))
                {
                    Account.CreateUser("santiaa", "santiaa!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Santiago, Roanne", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                }

                if (!Account.Exists("limcari"))
                {
                    Account.CreateUser("limcari", "limcari!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Lim, Terry", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                }

                if (!Account.Exists("lucayar"))
                {
                    Account.CreateUser("lucayar", "lucayar!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Lucaya, Rodney", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                }

                if (!Account.Exists("pascuaa"))
                {
                    Account.CreateUser("pascuaa", "pascuaa!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Pascual, Arla", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Saboteurs").ToList() });
                }

                if (!Account.Exists("fajarde"))
                {
                    Account.CreateUser("fajarde", "fajarde!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Fajardo, Ever", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                }

                if (!Account.Exists("evangel"))
                {
                    Account.CreateUser("evangel", "evangel!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Evangelista, Lesley", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                }

                if (!Account.Exists("quiazoj"))
                {
                    Account.CreateUser("quiazoj", "quiazoj!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Quiazon, Judee", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                }

                if (!Account.Exists("navarrj"))
                {
                    Account.CreateUser("navarrj", "navarrj!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Navarro, Jan", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                }
            }

        }
        
        public override void Down()
        {
        }
    }
}
