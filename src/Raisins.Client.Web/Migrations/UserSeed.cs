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


            //Group 1 QAiTS 
            AddAccountant(context, "adacrue", "adacrue!23", " Edhiko Adacruz", 1, 1);
            AddAccountant(context, "Andagda", "Andagda!23", "Danielito Andag", 1, 1);
            AddAccountant(context, "catigum", "catigum!23", " Michelle Rodrigo", 1, 1);


            //Group 2 QAiTS   Manileños
            AddAccountant(context, "Linggay", "Linggay!23", " Gayle Udelle Ling", 2, 1);
            AddAccountant(context, "jaraban", "jaraban!23", "Noreen Jaraba", 2, 1);
           


            //Group 3 The TimeJumpers
              AddAccountant(context, "seejoan", "seejoan!23", "Joan See", 3, 1);
              AddAccountant(context, "paguiad", "paguiad!23", "Dominic John Paguia", 3, 1);
              // AddAccountant(context, "Navarro", "Navarro!23", "Mai Navarro", 3, 1); //same type of user



            //Group 4  Funny is the New Pogi

            //Group 5  OCSDO Angels

            //Group 6 The Chronicles of Naina

            //Group 7 *TBA
            #endregion


            #region Users

            //Group 1 QAiTS
              AddUser(context, "trinidp", "trinidp!23", "Paolo Trinidad", 1, 1);
              AddUser(context, "Lumawim", "Lumawim!23", "Mark Lester Lumawig", 1, 1);
              AddUser(context, "soriajo", "soriajo!23", "Japs Soria", 1, 1);
              AddUser(context, "logicam", "logicam!23", "Mia Logica", 1, 1);
              AddUser(context, "jugosja", "jugosja!23", "Jason Jugos", 1, 1);
              AddUser(context, "mundoma", "mundoma!23", "Trina Mundo", 1, 1);

            //Group 2    Manileños
              AddUser(context, "galsiml", "galsiml!23", "Leah Galsim", 2, 1);
              AddUser(context, "domingb", "domingb!23", "Bon Domingo", 2, 1);
            


            //Group 3  The TimeJumpers
            AddUser(context, "navarrs", "navarrs!23", "Mai Navarro", 3, 1); 
            AddUser(context, "micukas", "micukas!23", "Kass Micu", 3, 1);
            AddUser(context, "tobiasa", "tobiasa!23", "Angie Tobias", 3, 1);
            AddUser(context, "taagkar", "taagkar!23", "Karl Alex Taag", 3, 1);
            AddUser(context, "cadizbl", "cadizbl!23", "Blesilda Cadiz", 3, 1);
            AddUser(context, "Soanjer", "Soanjer!23", "Jeremiah Soan", 3, 1);
            

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
