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
            Account account = Account.FindUser(userName);

            Setting setting = null;
            if (account.Settings != null && account.Settings.Count > 0)
            {
                setting = account.Settings[0];
            }
            
            return ToModel(setting);
        }

        public static SettingModel ToModel(Setting data)
        {
            SettingModel model = null;
            if (data != null)
            {
                model = new SettingModel();
                model.ID = data.ID;
                model.Currency = data.Currency;
                model.Location = data.Location;
                model.UserName = data.Account.UserName;
                
                if (data.Beneficiary != null)
                {
                    model.BeneficiaryID = data.Beneficiary.ID;
                }

                model.Class = data.Class;
            }
            return model;

        }

    }
}