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
            Role.Add(new Role() { Name = "Administrator" });
            Role.Add(new Role() { Name = "Accountant" });
            Role.Add(new Role() { Name = "Manager" });
            Role.Add(new Role() { Name = "User" });

            Account.CreateUser("admin", "P@ssw0rd!1",
                new List<Role>()  
                { 
                    Role.Find("Administrator") 
                }, new AccountProfile() 
                {
                    Name = "Administrator",
                    Beneficiaries = Beneficiary.GetAll(),
                    Currencies = Currency.GetAll()
                });

            Activity.Add(new Activity() 
            { 
                Name = "Payment", 
                Roles = new List<Role>() 
                { 
                    Role.Find("Administrator"),
                    Role.Find("Accountant"),
                    Role.Find("User")
                } 
            });

            Beneficiary.Add(new Beneficiary() { Description = "The Res Band", ID = 1, Name = "The Res Band"});
            Beneficiary.Add(new Beneficiary() { Description = "Forever Wassaque", ID = 1, Name = "Forever Wassaque" });
            Beneficiary.Add(new Beneficiary() { Description = "JNG Project", ID = 1, Name = "JNG Project" });
            Beneficiary.Add(new Beneficiary() { Description = "The Saboteurs", ID = 1, Name = "The Saboteurs" });
            Beneficiary.Add(new Beneficiary() { Description = "The Remedy", ID = 1, Name = "The Remedy" });

            Currency.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, ID = 1, Ratio = 1M});
            
        }

    }
}