using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class SettingService
    {

        public static SettingModel GetSetting(string userName)
        {
            return ToModel(Account.FindUser(userName).Settings[0]);
        }

        public static SettingModel ToModel(Setting data)
        {

            SettingModel model = new SettingModel();
            model.ID = data.ID;
            model.Currency = data.Currency.CurrencyCode;
            model.Location = data.Location;
            model.UserName = data.Account.UserName;
            model.BeneficiaryID = data.Beneficiary.ID;

            return model;

        }

    }
}