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

            Account.CreateUser("admin", "P@ssw0rd!1", new List<Role>()  { Role.Find("Administrator") });
            Account.CreateUser("netxph", "P@ssw0rd!1", new List<Role>() { Role.Find("User") });

            Activity.Add(new Activity() { Name = "Beneficiary", Roles = new List<Role>() { Role.Find("Administrator") } });

            Beneficiary.Add(new Beneficiary() { Description = "Reservations Team", ID = 1, Name = "Team Res"});
            Currency.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, ID = 1, Ratio = 1M});
            
        }

    }
}