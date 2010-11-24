using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class BeneficiaryService
    {

        #region Operations

        public static BeneficiaryModel[] GetStatistics(string userName)
        {
            List<BeneficiaryModel> models = new List<BeneficiaryModel>();

            var setting = Account.FindUser(userName).Settings.FirstOrDefault();

            if (setting != null && setting.Beneficiary != null)
            {
                var model = ToModel(setting.Beneficiary);
                model.Detail = getDetails(setting.Beneficiary);

                models.Add(model);
            }
            else
            { 
                //load all
                var beneficiaries = Beneficiary.FindAll();

                foreach (var beneficiary in beneficiaries)
                {
                    var model = ToModel(beneficiary);
                    model.Detail = getDetails(beneficiary);

                    models.Add(model);
                }
            }

            return models.ToArray();
        }

        private static BeneficiaryDetailModel getDetails(Beneficiary beneficiary)
        {
            BeneficiaryDetailModel detail = new BeneficiaryDetailModel();
            detail.TotalAmount = beneficiary.GetTotalAmount();
            detail.Votes = beneficiary.GetTotalVotes();

            return detail;
        }

        public static BeneficiaryModel[] FindByUser(string userName)
        {
            List<BeneficiaryModel> models = new List<BeneficiaryModel>();

            var setting = Account.FindUser(userName).Settings.FirstOrDefault();

            if (setting != null && setting.Beneficiary != null)
            {
                var model = ToModel(setting.Beneficiary);

                models.Add(model);
            }
            else
            {
                //load all
                var beneficiaries = Beneficiary.FindAll();

                foreach (var beneficiary in beneficiaries)
                {
                    var model = ToModel(beneficiary);

                    models.Add(model);
                }
            }

            return models.ToArray();
        }

        public static BeneficiaryModel[] FindAll()
        {
            Beneficiary[] beneficiaries = Beneficiary.FindAll();

            List<BeneficiaryModel> models = new List<BeneficiaryModel>();

            foreach (Beneficiary beneficiary in beneficiaries)
            {
                models.Add(ToModel(beneficiary));
            }

            return models.ToArray();
        }

        #endregion

        #region Helper methods

        protected static BeneficiaryModel ToModel(Beneficiary data)
        {
            BeneficiaryModel model = new BeneficiaryModel();
            model.Description = data.Description;
            model.ID = data.ID;
            model.Name = data.Name;

            return model;
        }

        protected static Beneficiary ToData(BeneficiaryModel model)
        {
            Beneficiary data = new Beneficiary();
            data.Description = model.Description;
            data.ID = model.ID;
            data.Name = model.Name;

            return data;
        }

        #endregion

    }
}