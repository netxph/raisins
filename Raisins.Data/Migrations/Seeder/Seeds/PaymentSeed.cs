using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class PaymentSeed : IDbSeeder
    {
        public void Seed(RaisinsContext context)
        {
            AddPayment(context, "payment1", 1000.5M, 1, 1, 1, "International", "Cash");  //1
            AddPayment(context, "payment2", 2000.25M, 2, 1, 1, "Local", "PayPal"); //2
            AddPayment(context, "payment3", 1500, 2, 1, 1, "International", "Bank Deposit");     //3
            context.SaveChanges();
        }

        private void AddPayment(RaisinsContext context,
            string name,
            decimal amount,
            int beneficiaryID,
            int currencyID,
            int createdByID,
            string sourceName,
            string typeName)
        {
            if (!context.Payments.Any(c => c.Name == name))
            {
                //context.Payments.Add(new Payment()
                //{
                //    Name = name,
                //    Amount = amount,
                //    BeneficiaryID = beneficiaryID,
                //    CurrencyID = currencyID,
                //    CreatedByID = createdByID
                //});
                var source = context.Sources.FirstOrDefault(x => x.Source.ToLower() == sourceName.ToLower());
                var type = context.Types.FirstOrDefault(x => x.Type.ToLower() == typeName.ToLower());

                if (source == null)
                {
                    throw new InvalidDataSeedException("Source not found!" + source );
                }
                if (type == null)
                {
                    throw new InvalidDataSeedException("Type not found!" + type);
                }

                context.Payments.Add(new Payment()
                {
                    Name = name,
                    Amount = amount,
                    BeneficiaryID = beneficiaryID,
                    CurrencyID = currencyID,
                    CreatedByID = createdByID,
                    CreatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    PaymentSourceID = source.PaymentSourceID,
                    PaymentTypeID = type.PaymentTypeID
                });


                //Payment payment = new Payment
                //{
                //    Name = name,
                //    Amount = amount,
                //    BeneficiaryID = beneficiaryID,
                //    CurrencyID = currencyID,
                //    CreatedByID = createdByID,
                //    CreatedDate = DateTime.Now,
                //    PaymentDate = DateTime.Now
                //};
                //context.Payments.Add(payment);
                //context.SaveChanges();
            }
        }
    }

}
