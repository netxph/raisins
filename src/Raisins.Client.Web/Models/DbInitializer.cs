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

                db.Currencies.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, ID = 1, Ratio = 50M });

                db.SaveChanges();

                //TODO: make this db based.
                Account.CreateUser("admin", "P@ssw0rd!1",
                    new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                    new AccountProfile() { Name = "Administrator", Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
            }
            
        }

    }
}