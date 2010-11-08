using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using NHibernate.Criterion;

namespace Raisins.Client.Web.Models
{
	public class BeneficiaryModel
	{

		#region Constructors

		public BeneficiaryModel()
		{

		}

		#endregion

		#region Properties

		public long ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Votes { get; set; }
		public float TotalAmount { get; set; }
	
		#endregion

        #region Operations

        public static BeneficiaryModel[] GetStatistics()
        {
            Beneficiary[] beneficiaries = Beneficiary.FindAll();

            List<BeneficiaryModel> models = new List<BeneficiaryModel>();

            foreach (Beneficiary beneficiary in beneficiaries)
            {
                models.Add(ToModel(beneficiary));
            }

            //sort
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
            model.TotalAmount = data.Accounts.Sum(account => account.Amount);
            model.Votes = 0;

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