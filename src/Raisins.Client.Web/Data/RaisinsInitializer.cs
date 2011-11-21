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
                RoleType = (int)RoleType.Administrator
            };

            context.Accounts.Add(adminAccount);

            doDevelopmentSeeds(context);
        }

        private void doDevelopmentSeeds(RaisinsDB context)
        {
            Beneficiary beneficiary = new Beneficiary()
            {
                Name = "pRESents",
                Description = @"Group 1- Reservations Dev"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "Puto Boom Boom",
                Description = @"Group 2 - NSQA, Ancillary QA, NS PM"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "Super Stars",
                Description = @"Group 3 - Revenue Accounting Dev/QA/PM (PRA, SkyLedger), TS"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "HAMazing",
                Description = @"Group 4 - SkyPrice DEV/QA, Ancillary Dev, Dinsey Dev, BPO, Shared Services (CM, Performance, Automation, NavIP), Imp PM"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "Edam and Eve",
                Description = @"Group 5 - Products, AOPS Dev/QA/PM"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "SUS.per Nova",
                Description = @"Group 6 - SUS Group1 (OC Team1, PRA/SkyLedger/DSS/ESC/Aops/Web, PerfMan/PMO, OC CI/OCMT)"
            };

            context.Beneficiaries.Add(beneficiary);

            beneficiary = new Beneficiary()
            {
                Name = "TreeSUS",
                Description = @"Group 7 - SUS Group 2 (PM Res, Installs, Non Nav, OC Team2)"
            };

            context.Beneficiaries.Add(beneficiary);

            Currency currency = new Currency()
            {
                CurrencyCode = "PHP",
                ExchangeRate = 1.0m,
                Ratio = 50.0m
            };

            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "USD",
                ExchangeRate = 43.0m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);
            
            context.SaveChanges();

            string salt = Account.GetSalt();
            int beneficiaryId = context.Beneficiaries.First().BeneficiaryID;
            int currencyId = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "PHP").CurrencyID;

            Account userAccount = new Account()
            {
                UserName = "vitalim",
                Salt = salt,
                Password = Account.GetHash("P@ssw0rd!1", salt),
                RoleType = (int)RoleType.User,
                Setting = new Setting() { BeneficiaryID = beneficiaryId, Class = (int)PaymentClass.Internal, CurrencyID = currencyId, Location = "PH" }
            };

            context.Accounts.Add(userAccount);

            userAccount = new Account()
            {
                UserName = "delosrd",
                Salt = salt,
                Password = Account.GetHash("delosrd", salt),
                RoleType = (int)RoleType.User,
                Setting = new Setting() { BeneficiaryID = beneficiaryId, Class = (int)PaymentClass.Internal, CurrencyID = currencyId, Location = "PH" }
            };

            context.Accounts.Add(userAccount);

            userAccount = new Account()
            {
                UserName = "auditor",
                Salt = salt,
                Password = Account.GetHash("P@ssw0rd!1", salt),
                RoleType = (int)RoleType.Auditor,
                Setting = new Setting() { BeneficiaryID = beneficiaryId, Class = (int)PaymentClass.Internal, CurrencyID = currencyId, Location = "PH" }
            };

            currencyId = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "USD").CurrencyID;

            userAccount = new Account()
            {
                UserName = "forex",
                Salt = salt,
                Password = Account.GetHash("P@ssw0rd!1", salt),
                RoleType = (int)RoleType.User,
                Setting = new Setting() { BeneficiaryID = 0, Class = (int)PaymentClass.Foreign, CurrencyID = currencyId, Location = "US" }
            };
                
            context.Accounts.Add(userAccount);
        }

    }
}