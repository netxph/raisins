using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Web.Migrations
{
    public class UserSeed : IDbSeeder
    {
        private UnitOfWork _unitOfWork;
        public void Seed(RaisinsDB context)
        {
            _unitOfWork = new UnitOfWork(context);
            #region Administrators

            AddAdmin("admin", "@dmin!23", "Developer Accounts");
            AddAdmin("mendozg", "mendozg!23", "Gen Mendoza");
            AddAdmin("delacle", "delacle!23", "Lei dela Cruz");
            AddAdmin("pascuaa", "pascuaa!23", "Arla Pascual");
            AddAdmin("delosrd", "delosrd!23", "Danica Delos Reyes");

            #endregion

            #region Accountants/Auditors
            
            //Group 1 QAiTS 
            AddAccountant("adacrue", "adacrue!23", " Edhiko Adacruz", 1, 1);
            AddAccountant("andagda", "andagda!23", "Danielito Andag", 1, 1);
            AddAccountant("catigum", "catigum!23", " Michelle Rodrigo", 1, 1);


            //Group 2 QAiTS   Manileños
            AddAccountant("linggay", "linggay!23", " Gayle Udelle Ling", 2, 1);
            AddAccountant("jaraban", "jaraban!23", "Noreen Jaraba", 2, 1);
           


            //Group 3 The TimeJumpers
              AddAccountant("seejoan", "seejoan!23", "Joan See", 3, 1);
              AddAccountant("paguiad", "paguiad!23", "Dominic John Paguia", 3, 1);
              // AddAccountant(context, "Navarro", "Navarro!23", "Mai Navarro", 3, 1); //same type of user



            //Group 4  Funny is the New Pogi
              AddAccountant("tanjoan", "tanjoan", "Joan Tan", 4, 1);
              AddAccountant("espirip", "espirip!23", "Pam Espiritu", 4, 1);

            //Group 5  OCSDO Angels
              AddAccountant("evangel", "evangel!23", "Lesley Evangelista", 5, 1);
              AddAccountant("dizonro", "dizonro!23", "Roberto Dizon", 5, 1);

            //Group 6 The Chronicles of Naina
              AddAccountant("cortesf", "cortesf!23", "Francis Pio Cortes", 6, 1);
              AddAccountant("lomedag", "lomedag!23", "Geronimo Lomeda, Jr.", 6, 1);

            //Group 7 *TBA

            #endregion


            #region Users

            //Group 1 QAiTS
              AddUser("trinidp", "trinidp!23", "Paolo Trinidad", 1, 1);
              AddUser("lumawim", "lumawim!23", "Mark Lester Lumawig", 1, 1);
              AddUser("soriajo", "soriajo!23", "Japs Soria", 1, 1);
              AddUser("logicam", "logicam!23", "Mia Logica", 1, 1);
              AddUser("jugosja", "jugosja!23", "Jason Jugos", 1, 1);
              AddUser("mundoma", "mundoma!23", "Trina Mundo", 1, 1);

            //Group 2    Manileños
              AddUser("galsiml", "galsiml!23", "Leah Galsim", 2, 1);
              AddUser("domingb", "domingb!23", "Bon Domingo", 2, 1);
            


            //Group 3  The TimeJumpers
            AddUser("navarrs", "navarrs!23", "Mai Navarro", 3, 1); 
            AddUser("micukas", "micukas!23", "Kass Micu", 3, 1);
            AddUser("tobiasa", "tobiasa!23", "Angie Tobias", 3, 1);
            AddUser("taagkar", "taagkar!23", "Karl Alex Taag", 3, 1);
            AddUser("cadizbl", "cadizbl!23", "Blesilda Cadiz", 3, 1);
            AddUser("soanjer", "soanjer!23", "Jeremiah Soan", 3, 1);
            

            //Group 4  Funny is the New Pogi
            AddUser("macalim", "macalim!23", "Dianne Macalintal", 4, 1);
            AddUser("miharam", "miharam!23", "Myra Mihara", 4, 1);
            AddUser("chiujef", "chiujef!23", "Jeff Chiu", 4, 1);
            AddUser("raymunk", "raymunk!23", "Kris Raymundo", 4, 1);
          
            //Group 5  OCSDO Angels
            AddUser("macalim", "macalim!23", "Lorreyn Joy Orbeta", 5, 1);
            AddUser("contrer", "contrer!23", "Rizelle Contreras", 5, 1);
            AddUser("medrani", "medrani!23", "Izza Medrano", 5, 1);
            AddUser("dalidka", "dalidka!23", "Maria Karla Dalid", 5, 1);
            AddUser("gabriee", "gabriee!23", "Jelynne Gabriel", 5, 1);


            //Group 6  The Chronicles of Naina
            AddUser("pardoja ", "pardoja !23", "Jazel Pardo", 6, 1);
            AddUser("fernanp", "fernanp!23", "Paula Bianca Fernandez", 6, 1);
            AddUser("tarcenm ", "tarcenm ", "Michelle Tarcena", 6, 1);

            //Group 7  *TBA
            


            #endregion
        }

        private void AddAdmin(
            string userName,
            string password,
            string name)
        {
            if (!_unitOfWork.Accounts.Any(userName))
            {
                var role = _unitOfWork.Roles.Get("Administrator");
                var beneficiaries = _unitOfWork.Beneficiaries.GetAll();
                var currencies = _unitOfWork.Currencies.GetAll();

                Account account = new Account
                {
                    UserName = userName,
                    Password = password,
                    Roles = new List<Role>() { role },
                    Profile = new AccountProfile
                    {
                        Name = name,
                        Beneficiaries = beneficiaries.ToList(),
                        Currencies = currencies.ToList()
                    }

                };

                var salt = Helper.CreateSalt();
                account.SetSalt(salt);
                account.GenerateNewPassword(password, salt);
                _unitOfWork.Accounts.Add(account);

                _unitOfWork.Complete();
            }
        }

        private void AddUser(
           string userName,
           string password,
           string name,
           int beneficiaryId,
           int currencyId)
        {
            if (!_unitOfWork.Accounts.Any(userName))
            {
                var role = _unitOfWork.Roles.Get("User");
                var beneficiary = _unitOfWork.Beneficiaries.Find(beneficiaryId);
                var currency = _unitOfWork.Currencies.Find(currencyId);

                Account account = new Account
                {
                    UserName = userName,
                    Password = password,
                    Roles = new List<Role>() { role },
                    Profile = new AccountProfile
                    {
                        Name = name,
                        Beneficiaries = new List<Beneficiary>() { beneficiary },
                        Currencies = new List<Currency>() { currency }
                    }

                };

                var salt = Helper.CreateSalt();
                account.SetSalt(salt);
                account.GenerateNewPassword(password, salt);
                _unitOfWork.Accounts.Add(account);


                _unitOfWork.Complete();
            }
        }

        private void AddAccountant(
           string userName,
           string password,
           string name,
           int beneficiaryId,
           int currencyId)
        {
            if (!_unitOfWork.Accounts.Any(userName))
            {
                var role = _unitOfWork.Roles.Get("Accountant");
                var beneficiary = _unitOfWork.Beneficiaries.Find(beneficiaryId);
                var currency = _unitOfWork.Currencies.Find(currencyId);

                Account account = new Account
                {
                    UserName = userName,
                    Password = password,
                    Roles = new List<Role>() { role },
                    Profile = new AccountProfile
                    {
                        Name = name,
                        Beneficiaries = new List<Beneficiary>() { beneficiary },
                        Currencies = new List<Currency>() { currency }
                    }

                };



                var salt = Helper.CreateSalt();
                account.SetSalt(salt);
                account.GenerateNewPassword(password, salt);
                _unitOfWork.Accounts.Add(account);

                _unitOfWork.Complete();
            }
        }
    }
}
