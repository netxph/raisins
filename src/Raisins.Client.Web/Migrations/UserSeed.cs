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
            #region Administrators

            AddAdmin(context, "mendozg", "mendozg!23", "Gen Mendoza");
            AddAdmin(context, "delacle", "delacle!23", "Lei dela Cruz");
            AddAdmin(context, "pascuaa", "pascuaa!23", "Arla Pascual");

            #endregion

            #region Accountants/Auditors

            AddAccountant(context, "linggay", "linggay!23", "Gayle Ling", 1, 1);
            AddAccountant(context, "jaraban", "jaraban!23", "Noreen Jaraba", 1, 1);
            AddAccountant(context, "pardoja", "delacle!23", "Jazel Pardo", 2, 1);
            AddAccountant(context, "logicam", "logicam!23", "Mia Logica", 3, 1);
            AddAccountant(context, "evangch", "evangch!23", "Charry Evangelista", 4, 1);
            AddAccountant(context, "macalim", "macalim!23", "Dianne Macalintal", 5, 1);
            AddAccountant(context, "candalm", "candalm!23", "Marlo Candaliza", 6, 1);

            #endregion

            #region Users
            AddUser(context, "cadizbl", "cadizbl!23", "Bless Cadiz", 4, 1);
            AddUser(context, "tagapag", "tagapag!23", "Grace Tagapan", 4, 1);
            AddUser(context, "gabriem", "gabriem!23", "Leslie Gabriel", 4, 1);
            AddUser(context, "corteza", "corteza!23", "Aldrin Cortrez", 3, 1);
            AddUser(context, "pascuac", "pascuac!23", "Carmelyn Pascual", 3, 1);
            AddUser(context, "perezan", "perezan!23", "Venerando Perez", 5, 1);
            AddUser(context, "padernj", "padernj!23", "John Carlos Padernal", 5, 1);
            AddUser(context, "quillas", "quillas!23", "Shane Quillan", 6, 1);
            AddUser(context, "bolalie", "bolalie!23", "Eliza Bolalin", 6, 1);
            AddUser(context, "decastt", "decastt!23", "Teodore De Castro", 6, 1);
            AddUser(context, "leonorw", "leonorw!23", "William Leonor", 1, 1);
            AddUser(context, "vidallu", "vidallu!23", "Lui Vidal", 1, 1);
            #endregion
        }

        private static void AddAdmin(
            RaisinsDB context,
            string userName,
            string password,
            string name)
        {
            if (!context.Accounts.Any(a => a.UserName == userName))
            {
                var role = Role.Find("Administrator");
                var beneficiaries = Beneficiary.GetAll();
                var currencies = Currency.GetAll();

                Account.CreateUser(userName, password, new List<Role>() { role }, new AccountProfile()
                {
                    Name = name,
                    Beneficiaries = beneficiaries,
                    Currencies = currencies
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
                    Currencies = new List<Currency>(){ currency }
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
