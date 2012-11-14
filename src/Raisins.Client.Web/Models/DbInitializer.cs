using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Roles.Add(new Role() { Name = "Administrator" });
                db.Roles.Add(new Role() { Name = "Accountant" });
                db.Roles.Add(new Role() { Name = "Manager" });
                db.Roles.Add(new Role() { Name = "User" });

                db.SaveChanges();

                db.Activities.Add(new Activity()
                {
                    Name = "Payment",
                    Roles = new List<Role>() 
                { 
                    db.Roles.First(r => r.Name == "Administrator"),
                    db.Roles.First(r => r.Name == "Accountant"),
                    db.Roles.First(r => r.Name == "User")
                }
                });

                db.SaveChanges();

                db.Beneficiaries.Add(new Beneficiary() { Description = "The Res Band", Name = "The Res Band" });
                db.Beneficiaries.Add(new Beneficiary() { Description = "Forever Wassaque", Name = "Forever Wassaque" });
                db.Beneficiaries.Add(new Beneficiary() { Description = "JNG Project", Name = "JNG Project" });
                db.Beneficiaries.Add(new Beneficiary() { Description = "The Saboteurs", Name = "The Saboteurs" });
                db.Beneficiaries.Add(new Beneficiary() { Description = "The Remedy", Name = "The Remedy" });

                db.SaveChanges();

                db.Executives.Add(new Executive() { Name = "Dave Evans" });
                db.Executives.Add(new Executive() { Name = "Chuck Maahs" });
                db.Executives.Add(new Executive() { Name = "Kurt Blumberg" });

                db.SaveChanges();

                db.Currencies.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, Ratio = 50M });
                db.Currencies.Add(new Currency() { CurrencyCode = "USD", ExchangeRate = 42, Ratio = 1M });
                db.Currencies.Add(new Currency() { CurrencyCode = "AUD", ExchangeRate = 43, Ratio = 1M });
                db.Currencies.Add(new Currency() { CurrencyCode = "SGD", ExchangeRate = 34, Ratio = 2M });
                db.Currencies.Add(new Currency() { CurrencyCode = "HKD", ExchangeRate = 5, Ratio = 9M });
                db.Currencies.Add(new Currency() { CurrencyCode = "EUR", ExchangeRate = 53, Ratio = 1M });
                db.Currencies.Add(new Currency() { CurrencyCode = "GBP", ExchangeRate = 66, Ratio = 1M });
                db.Currencies.Add(new Currency() { CurrencyCode = "NZD", ExchangeRate = 34, Ratio = 2M });
                db.Currencies.Add(new Currency() { CurrencyCode = "MYR", ExchangeRate = 13, Ratio = 4M });

                db.SaveChanges();

                //TODO: make this db based.
                Account.CreateUser("admin", "P@ssw0rd!1",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                    new AccountProfile() { Name = "Administrator", Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });

                Account.CreateUser("netxph", "P@ssw0rd!1",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "P@ssw0rd!1", Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList() , Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });

                Account.CreateUser("abayona", "P@ssw0rd!1",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                    new AccountProfile() { Name = "P@ssw0rd!1", Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
            }
            
        }

    }
}