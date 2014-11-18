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

            AddAdmin(context, "admin", "@dmin!23", "Developer Accounts");
            AddAdmin(context, "mendozg", "mendozg!23", "Gen Mendoza");
            AddAdmin(context, "delacle", "delacle!23", "Lei dela Cruz");
            AddAdmin(context, "pascuaa", "pascuaa!23", "Arla Pascual");

            #endregion

            #region Accountants/Auditors

            /*AddAccountant(context, "linggay", "linggay!23", "Gayle Ling", 1, 1);
            AddAccountant(context, "jaraban", "jaraban!23", "Noreen Jaraba", 1, 1);
            AddAccountant(context, "pardoja", "pardoja!23", "Jazel Pardo", 2, 1);
            AddAccountant(context, "logicam", "logicam!23", "Mia Logica", 3, 1);
            AddAccountant(context, "macalim", "macalim!23", "Dianne Macalintal", 5, 1);
            AddAccountant(context, "candalm", "candalm!23", "Marlo Candaliza", 6, 1);
            AddAccountant(context, "evangec", "evangec!23", "Charry Evangelista", 4, 1);
            AddAccountant(context, "soriajo", "soriajo!23", "Jan Alvin Soria", 4, 1);
             */

            //Group 1 QAiTS 
            AddAccountant(context, "adacruz", "adacruz!23", "Michelle Rodrigo Edhiko Adacruz", 1, 1);
            AddAccountant(context, "andag", "andag!23", "Danielito Andag", 1, 1);

            //Group 2 QAiTS   Manileños
              AddAccountant(context, "ling", "ling!23", " Gayle Udelle Ling", 2, 1);
              AddAccountant(context, "jaraba", "jaraba!23", "Noreen Jaraba", 2, 1);
           


            //Group 3 The TimeJumpers
              AddAccountant(context, "ling", "ling!23", "Joan See", 3, 1);
              AddAccountant(context, "jaraba", "jaraba!23", "Dominic John Paguia", 3, 1);
              AddAccountant(context, "Navarro", "Navarro!23", "Mai Navarro", 3, 1);



            //Group 4  Funny is the New Pogi

            //Group 5  OCSDO Angels

            //Group 6 The Chronicles of Naina

            //Group 7 *TBA
            #endregion

            #region Users
/*          AddUser(context, "cadizbl", "cadizbl!23", "Bless Cadiz", 4, 1);
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
 * */
            //Group 1 QAiTS
            AddUser(context, "trinidad", "trinidad!23", "Paolo Trinidad", 1, 1);
            AddUser(context, "lumawig", "lumawig!23", "Mark Lester Lumawig", 1, 1);
            AddUser(context, "soria", "soria!23", "Japs Soria", 1, 1);
            AddUser(context, "logica", "logica!23", "Mia Logica", 1, 1);

            //Group 2    Manileños
            AddUser(context, "galsim", "galsim!23", "Leah Galsim", 2, 1);
            AddUser(context, "domingo", "domingo!23", "Bon Domingo", 2, 1);
            


            //Group 3  The TimeJumpers
            AddUser(context, "navarro", "navarro!23", "Mai Navarro", 3, 1); //same type of user
            AddUser(context, "micu", "micu!23", "Kass Micu", 3, 1);
            AddUser(context, "tobias", "tobias!23", "Angie Tobias", 3, 1);
            AddUser(context, "taag", "taag!23", "Karl Alex Taag", 3, 1);
            AddUser(context, "cadiz", "cadiz!23", "Blesilda Cadiz", 3, 1);
            AddUser(context, "soan", "soan!23", "Jeremiah Soan", 3, 1);
            

            //Group 4  Funny is the New Pogi

            //Group 5  OCSDO Angels

            //Group 6  The Chronicles of Naina

            //Group 7  *TBA
            


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
