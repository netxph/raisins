using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Data
{
    public class RaisinsInitializer : DropCreateDatabaseIfModelChanges<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {

            //role
            Role role = new Role()
            {
                RoleType = (int)RoleType.Administrator
            };

            base.Seed(context);

            string salt = Account.GetSalt();

            var adminAccount = new Account()
            {
                UserName = "admin",
                Salt = salt,
                Password = Account.GetHash("r@isin5", salt),
                Role = role
            };

            context.Roles.Add(role);
            context.Accounts.Add(adminAccount);

            doDevelopmentSeeds(context);
        }

        private void doDevelopmentSeeds(RaisinsDB context)
        {
            Beneficiary beneficiary = new Beneficiary()
            {
                Name = "Team 1",
                Description = "The First Team"
            };

            context.Beneficiaries.Add(beneficiary);

            Role userRole = new Role()
            {
                RoleType = (int)RoleType.User
            };

            context.Roles.Add(userRole);

            Currency currency = new Currency()
            {
                CurrencyCode = "USD",
                ExchangeRate = 1.0m,
                Ratio = 1.0m
            };

            context.Currencies.Add(currency);
            context.SaveChanges();

            string salt = Account.GetSalt();
            int beneficiaryId = context.Beneficiaries.First().BeneficiaryID;
            int currencyId = context.Currencies.First().CurrencyID;

            Account userAccount = new Account()
            {
                UserName = "vitalim",
                Salt = salt,
                Password = Account.GetHash("P@ssw0rd!1", salt),
                Role = userRole,
                Setting = new Setting() { BeneficiaryID = beneficiaryId, Class = (int)PaymentClass.Internal, CurrencyID = currencyId, Location = "PH" }
            };

            context.Accounts.Add(userAccount);
        }

    }
}