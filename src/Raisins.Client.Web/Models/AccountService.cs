using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class AccountService
    {

        #region Operations

        public static AccountModel[] FindAll()
        {
            Account[] accounts = Account.FindAll();
            List<AccountModel> models = new List<AccountModel>();

            foreach (Account account in accounts)
            {
                models.Add(ToModel(account));
            }

            return models.ToArray();
        }

        public static void Save(AccountModel model)
        {
            model.BeneficiaryID = 1;

            Account data = ToData(model);

            data.Save();
        }

        #endregion

        #region Helper methods

        protected static AccountModel ToModel(Account data)
        {
            AccountModel model = new AccountModel();
            model.Amount = data.Amount;
            model.Currency = data.Currency;
            model.Email = data.Email;
            model.ID = data.ID;
            model.Location = data.Location;
            model.Name = data.Name;

            return model;
        }

        protected static Account ToData(AccountModel model)
        {
            Account data = new Account();
            data.Amount = model.Amount;
            data.Currency = model.Currency;
            data.Email = model.Email;
            data.ID = model.ID;
            data.Location = model.Location;
            data.Name = model.Name;

            data.Beneficiary = Beneficiary.Find(model.BeneficiaryID);

            return data;
        }

        #endregion

    }
}