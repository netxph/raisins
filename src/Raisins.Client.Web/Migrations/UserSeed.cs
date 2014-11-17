﻿using System;
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

            AddAdmin(context, "admin", "@dmin!23", "Developer Accounts");
            AddAdmin(context, "mendozg", "mendozg!23", "Gen Mendoza");
            AddAdmin(context, "delacle", "delacle!23", "Lei dela Cruz");
            AddAdmin(context, "pascuaa", "pascuaa!23", "Arla Pascual");
            AddAdmin(context, "delosrd", "delosrd!23", "Danica Delos Reyes");

            #endregion

            int groupCount = 7;

            KeyValuePair<int, int>[] userTypeCountPair = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(2,3),
                new KeyValuePair<int, int>(3,4)
 
            };

            string accountantFormat = "G{0}Auditor{1}";
            string accountantPasswordFormat = "G{0}Auditor{1}!23";
            string accountantTempNameFormat = "Group {0} Auditor {1}";

            string userFormat = "G{0}User{1}";
            string userPasswordFormat = "G{0}User{1}!23";
            string userTempNameFormat = "Group {0} User {1}";

            for (int x = 1; x <= groupCount; x++)
            {
                foreach (KeyValuePair<int, int> pair in userTypeCountPair)
                {
                    if (pair.Key == 2)
                    {
                        for( int y = 1; y <= pair.Value; y++)
                        {
                            AddAccountant(
                                context,
                                string.Format(accountantFormat, x, y),
                                string.Format(accountantPasswordFormat, x, y),
                                string.Format(accountantTempNameFormat, x, y),
                                x,
                                1);
                        }
                    }
                    if (pair.Key == 3)
                    {
                        for (int y = 1; y <= pair.Value; y++)
                        {
                            AddUser(
                                context,
                                string.Format(userFormat, x, y),
                                string.Format(userPasswordFormat, x, y),
                                string.Format(userTempNameFormat, x, y),
                                x,
                                1);
                        } 
                    }

                }
            }

            #region add old users
            /*
             * 
            #region Accountants/Auditors
            
            AddAccountant(context, "linggay", "linggay!23", "Gayle Ling", 1, 1);
            AddAccountant(context, "jaraban", "jaraban!23", "Noreen Jaraba", 1, 1);
            AddAccountant(context, "pardoja", "pardoja!23", "Jazel Pardo", 2, 1);
            AddAccountant(context, "logicam", "logicam!23", "Mia Logica", 3, 1);
            AddAccountant(context, "macalim", "macalim!23", "Dianne Macalintal", 5, 1);
            AddAccountant(context, "candalm", "candalm!23", "Marlo Candaliza", 6, 1);
            AddAccountant(context, "evangec", "evangec!23", "Charry Evangelista", 4, 1);
            AddAccountant(context, "soriajo", "soriajo!23", "Jan Alvin Soria", 4, 1);
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
            AddUser(context, "evangel", "evangel!23", "Lesley Evangelista", 2, 1);
            #endregion
             * 
             */
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
