namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class newusers1 : DbMigration
    {
        public override void Up()
        {

            using (var db = ObjectProvider.CreateDB())
            {
                var account = db.Accounts.FirstOrDefault(a => a.UserName == "bolalie");

                if (account != null)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                }

                account = db.Accounts.FirstOrDefault(a => a.UserName == "bisaron");

                if (account != null)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                }

                account = db.Accounts.FirstOrDefault(a => a.UserName == "santiaa");

                if (account != null)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                }

                if (!Account.Exists("bolalie"))
                {
                    Account.CreateUser("bolalie", "bolalie!23",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "Bolalin, Lian", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                }

                if (!Account.Exists("santiaa"))
                {
                    Account.CreateUser("santiaa", "santiaa!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Santiago, Roanne", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                }

                if (!Account.Exists("sorianm"))
                {
                    Account.CreateUser("sorianm", "sorianm!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                            new AccountProfile() { Name = "Soriano, Michelle", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                }

                if (!Account.Exists("mendozg"))
                {
                    Account.CreateUser("mendozg", "mendozg!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                        new AccountProfile() { Name = "Mendoza, Genecel", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                }

                if (!Account.Exists("delosrd"))
                {
                    Account.CreateUser("delosrd", "delosrd!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                            new AccountProfile() { Name = "Delos Reyes, Danica", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                }

            }

        }

        public override void Down()
        {
        }
    }
}
