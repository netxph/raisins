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
            AddAdmin(context, "delosrd", "delosrd!23", "Danica Delos Reyes");

            #endregion

            #region Accountants/Auditors
            
            //Group 1 QAiTS 
            AddAccountant(context, "adacrue", "adacrue!23", " Edhiko Adacruz", 1, 1);
            AddAccountant(context, "andagda", "andagda!23", "Danielito Andag", 1, 1);
            AddAccountant(context, "catigum", "catigum!23", " Michelle Rodrigo", 1, 1);


            //Group 2 QAiTS   Manileños
            AddAccountant(context, "linggay", "linggay!23", " Gayle Udelle Ling", 2, 1);
            AddAccountant(context, "jaraban", "jaraban!23", "Noreen Jaraba", 2, 1);
            AddAccountant(context, "g2auditor", "g2auditor!23", "Group 2 Auditor", 2, 1); 
           

            //Group 3 The TimeJumpers
              AddAccountant(context, "seejoan", "seejoan!23", "Joan See", 3, 1);
              AddAccountant(context, "paguiad", "paguiad!23", "Dominic John Paguia", 3, 1);
              AddAccountant(context, "g3auditor", "g3auditor!23", "Group 3 Auditor", 3, 1); //same type of user


            //Group 4  Funny is the New Pogi
              AddAccountant(context, "tanjoan", "tanjoan!23", "Joan Tan", 4, 1);
              AddAccountant(context, "espirip", "espirip!23", "Pam Espiritu", 4, 1);
              AddAccountant(context, "g4auditor", "g4auditor!23", "Group 4 Auditor", 4, 1);

            //Group 5  OCSDO Angels
              AddAccountant(context, "evangel", "evangel!23", "Lesley Evangelista", 5, 1);
              AddAccountant(context, "dizonro", "dizonro!23", "Roberto Dizon", 5, 1);
              AddAccountant(context, "g5auditor", "g5auditor!23", "Group 5 Auditor", 5, 1);

            //Group 6 The Chronicles of Naina
              AddAccountant(context, "cortesf", "cortesf!23", "Francis Pio Cortes", 6, 1);
              AddAccountant(context, "lomedag", "lomedag!23", "Geronimo Lomeda, Jr.", 6, 1);
              AddAccountant(context, "g6auditor", "g6auditor!23", "Group 6 Auditor", 6, 1);

            //Group 7 *TBA
              AddAccountant(context, "g7auditor1", "g7auditor1!23", "Group 7 Auditor 1", 7, 1);
              AddAccountant(context, "g7auditor2", "g7auditor2!23", "Group 7 Auditor 2", 7, 1);
              AddAccountant(context, "g7auditor3", "g7auditor3!23", "Group 7 Auditor 3", 7, 1);
            #endregion


            #region Users

            //Group 1 QAiTS
              AddUser(context, "trinidp", "trinidp!23", "Paolo Trinidad", 1, 1);
              AddUser(context, "lumawim", "lumawim!23", "Mark Lester Lumawig", 1, 1);
              AddUser(context, "soriajo", "soriajo!23", "Japs Soria", 1, 1);
              AddUser(context, "logicam", "logicam!23", "Mia Logica", 1, 1);
              AddUser(context, "jugosja", "jugosja!23", "Jason Jugos", 1, 1);
              AddUser(context, "mundoma", "mundoma!23", "Trina Mundo", 1, 1);

            //Group 2    Manileños
              AddUser(context, "galsiml", "galsiml!23", "Leah Galsim", 2, 1);
              AddUser(context, "domingb", "domingb!23", "Bon Domingo", 2, 1);
              AddUser(context, "solisd", "solisd!23", "Dian Solis", 2, 1);
              AddUser(context, "brutasb", "brutasb!23", "Boris Brutas", 2, 1);


            //Group 3  The TimeJumpers
            AddUser(context, "navarrs", "navarrs!23", "Mai Navarro", 3, 1); 
            AddUser(context, "micukas", "micukas!23", "Kass Micu", 3, 1);
            AddUser(context, "tobiasa", "tobiasa!23", "Angie Tobias", 3, 1);
            AddUser(context, "taagkar", "taagkar!23", "Karl Alex Taag", 3, 1);
            AddUser(context, "cadizbl", "cadizbl!23", "Blesilda Cadiz", 3, 1);
            AddUser(context, "soanjer", "soanjer!23", "Jeremiah Soan", 3, 1);
            

            //Group 4  Funny is the New Pogi
            AddUser(context, "macalim", "macalim!23", "Dianne Macalintal", 4, 1);
            AddUser(context, "miharam", "miharam!23", "Myra Mihara", 4, 1);
            AddUser(context, "chiujef", "chiujef!23", "Jeff Chiu", 4, 1);
            AddUser(context, "raymunk", "raymunk!23", "Kris Raymundo", 4, 1);
          
            //Group 5  OCSDO Angels
            AddUser(context, "macalim", "macalim!23", "Lorreyn Joy Orbeta", 5, 1);
            AddUser(context, "contrer", "contrer!23", "Rizelle Contreras", 5, 1);
            AddUser(context, "medrani", "medrani!23", "Izza Medrano", 5, 1);
            AddUser(context, "dalidka", "dalidka!23", "Maria Karla Dalid", 5, 1);
            AddUser(context, "gabriee", "gabriee!23", "Jelynne Gabriel", 5, 1);


            //Group 6  The Chronicles of Naina
            AddUser(context, "pardoja", "pardoja!23", "Jazel Pardo", 6, 1);
            AddUser(context, "fernanp", "fernanp!23", "Paula Bianca Fernandez", 6, 1);
            AddUser(context, "tarcenm", "tarcenm!23", "Michelle Tarcena", 6, 1);
            AddUser(context, "g6logger1", "g6logger1!23", "Group 6 Logger 1", 6, 1);
            
           //Group 7  *TBA
            AddUser(context, "g7logger1", "g7logger1!23", "Group 7 Logger 1", 7, 1);
            AddUser(context, "g7logger2", "g7logger2!23", "Group 7 Logger 2", 7, 1);
            AddUser(context, "g7logger3", "g7logger3!23", "Group 7 Logger 3", 7, 1);
            AddUser(context, "g7logger4", "g7logger4!23", "Group 7 Logger 4", 7, 1);
            // TODO add 4 loggers

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
