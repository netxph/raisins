using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Migrations
{
    public partial class Group7Migration : IDbSeeder
    {
        public void Seed(Models.RaisinsDB context)
        {
            AddBeneficiary(context, "TechnoSUS and Friends", "SUS IT/Installs/PMO/PerfMon/Security/BusOps"); //7

            //Group 7 *TBA
            AddAccountant(context, "quillas", "quillas!23", "Shane Quillan", 7, 1);
            AddAccountant(context, "azarcom", "azarcom!23", "Marc Azarcon", 7, 1);
            AddAccountant(context, "g7auditor1", "g7auditor1!23", "Group 7 Auditor 1", 7, 1);

            //Group 7  *TBA
            AddUser(context, "diazgab", "diazgab!23", "Gabriel Diaz", 7, 1);
            AddUser(context, "luikath", "luikath!23", "Kathleen Lui", 7, 1);
            AddUser(context, "vivarja", "vivarja!23", "Jamie Vivar", 7, 1);
            AddUser(context, "alvarec", "alvarec!23", "Cecile Alvarez", 7, 1);
            // TODO add 4 loggers
        }

        private static void AddBeneficiary(
            Models.RaisinsDB context,
            string name,
            string description)
        {
            if (!context.Beneficiaries.Any(b => b.Name == name))
            {
                Beneficiary.Add(new Beneficiary()
                {
                    Name = name,
                    Description = description
                });
            }
        }

        private static void AddUser(
          RaisinsDB context,
          string userName,
          string password,
          string name,
          int beneficiaryId,
          int currencyId)
        {
            if (!context.Accounts.Any(a => a.UserName == userName))
            {
                var role = Role.Find("User");
                var beneficiary = Beneficiary.Find(beneficiaryId);
                var currency = Currency.Find(currencyId);

                Account.CreateUser(userName, password, new List<Role>() { role }, new AccountProfile()
                {
                    Name = name,
                    Beneficiaries = new List<Beneficiary>() { beneficiary },
                    Currencies = new List<Currency>() { currency }
                });
            }
        }

        private static void AddAccountant(
           RaisinsDB context,
           string userName,
           string password,
           string name,
           int beneficiaryId,
           int currencyId)
        {
            if (!context.Accounts.Any(a => a.UserName == userName))
            {
                var role = Role.Find("Accountant");
                var beneficiary = Beneficiary.Find(beneficiaryId);
                var currency = Currency.Find(currencyId);

                Account.CreateUser(userName, password, new List<Role>() { role }, new AccountProfile()
                {
                    Name = name,
                    Beneficiaries = new List<Beneficiary>() { beneficiary },
                    Currencies = new List<Currency>() { currency }
                });
            }
        }
    }
}