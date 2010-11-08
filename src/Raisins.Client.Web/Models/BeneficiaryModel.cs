using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class BeneficiaryModel
    {

        public BeneficiaryModel()
        {

        }

        public long ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

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

    }
}