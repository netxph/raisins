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
            Account.CreateUser("admin", "R@isin5");
            Beneficiary.Add(new Beneficiary() { Description = "Reservations Team", ID = 1, Name = "Team Res"});
            Currency.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, ID = 1, Ratio = 1M});
            
        }

    }
}