using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raisins.Client.Web.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Raisins.Client.Web.Migrations
{
    public class UserSeed : IDbSeeder
    {
        public void Seed(RaisinsDB context)
        {
            if (!context.Accounts.Any(a => a.UserName == "delacle"))
            {
                var admin = Role.Find("admin");
                var beneficiary = Beneficiary.Find(1);
                var currency = Currency.Find(1);

                Account.CreateUser("delacle", "delacle!23", new List<Role>() { admin }, new AccountProfile() 
                { 
                    Name = "Lei dela Cruz",
                    Beneficiaries = new List<Beneficiary>() { beneficiary },
                    Currencies = new List<Currency>() { currency }
                });
            }
        }
    }
}
