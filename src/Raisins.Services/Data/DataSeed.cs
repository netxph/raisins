using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services.Models;

namespace Raisins.Services.Data
{
    public class DataSeed
    {

        public static void Start(RaisinsDB context)
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
                ExchangeRate = 43.44m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "AUD",
                ExchangeRate = 43.09m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "SGD",
                ExchangeRate = 33.40m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "NZD",
                ExchangeRate = 32.68m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "HKD",
                ExchangeRate = 5.58m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            currency = new Currency()
            {
                CurrencyCode = "GBP",
                ExchangeRate = 68.36m,
                Ratio = 1.0m
            };
            context.Currencies.Add(currency);

            context.SaveChanges();

            string salt = Account.GetSalt();
            int beneficiaryId = context.Beneficiaries.First().BeneficiaryID;
            int currencyId = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "PHP").CurrencyID;

            //Administrator
            Account userAccount = new Account()
            {
                UserName = "admin",
                Salt = salt,
                Password = Account.GetHash("r@isins", salt),
                RoleType = (int)RoleType.Administrator
            };

            context.Accounts.Add(userAccount);

            //Local Users
            context.Accounts.Add(addUser("decastm", 1, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("bolanoa", 2, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("obadajo", 2, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("venturm", 3, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("angkiko", 3, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("lucayar", 4, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("manalil", 4, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("diazmar", 5, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("pangilc", 5, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("fernanc", 6, PaymentClass.Internal, 1, "PH", RoleType.User));
            context.Accounts.Add(addUser("gomezma", 7, PaymentClass.Internal, 1, "PH", RoleType.User));

            //auditors
            context.Accounts.Add(addUser("rullleo", 1, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("bonitam", 2, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("reyesce", 3, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("uyjaych", 4, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("mendozn", 5, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("gomezja", 6, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));
            context.Accounts.Add(addUser("santiac", 7, PaymentClass.NotSpecified, 1, "PH", RoleType.Auditor));

            //foreign
            context.Accounts.Add(addUser("santoja", 0, PaymentClass.Foreign, 2, "US", RoleType.User));
            context.Accounts.Add(addUser("rulllef", 0, PaymentClass.Foreign, 2, "US", RoleType.User));

            context.SaveChanges();
        }


        private static Account addUser(string userName, int beneficiaryId, PaymentClass paymentClass, int currencyId, string location, RoleType roleType)
        {
            var salt = Account.GetSalt();
            var userAccount = new Account()
            {
                UserName = userName,
                Salt = salt,
                Password = Account.GetHash(userName, salt),
                RoleType = (int)roleType,
                Setting = new Setting() { BeneficiaryID = beneficiaryId, Class = (int)paymentClass, CurrencyID = currencyId, Location = location }
            };

            return userAccount;
        }

    }
}