using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class PaymentService
    {

        #region Operations

        public static PaymentModel[] FindAll()
        {
            Payment[] payments = Payment.FindAll();
            List<PaymentModel> models = new List<PaymentModel>();

            foreach (Payment payment in payments)
            {
                models.Add(ToModel(payment));
            }

            return models.ToArray();
        }

        public static void Save(PaymentModel model)
        {
            Payment data = ToData(model);

            data.Save();
        }

        public static void Update(PaymentModel modelToBeUpdated)
        {
            Save(modelToBeUpdated);
        }

        public static void Delete(long id)
        {
            IList<long> idsToDelete = new List<long>();
            idsToDelete.Add(id);
            Payment.DeleteAll(idsToDelete);
        }

        public static PaymentModel GetAccount(long id)
        {
            return ToModel(Payment.Find(id));
        }

        #endregion

        #region Helper methods

        protected static PaymentModel ToModel(Payment data)
        {
            PaymentModel model = new PaymentModel();
            model.Amount = data.Amount;
            model.Currency = data.Currency;
            model.Email = data.Email;
            model.ID = data.ID;
            model.BeneficiaryID = data.Beneficiary.ID;
            model.Location = data.Location;
            model.Name = data.Name;

            return model;
        }

        protected static Payment ToData(PaymentModel model)
        {
            Payment data = new Payment();
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