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
            AddPayment(context, "payment1", 1000.5M, 1, 1, 1); //1
            AddPayment(context, "payment2", 2000.25M, 2, 1, 1); //2
            AddPayment(context, "payment3", 1500, 2, 1, 1); //3
            AddPayment(context, "payment4", 1000, 2, 1, 1); //4
            AddPayment(context, "payment5", 1050, 3, 1, 1); //5
            AddPayment(context, "payment6", 1000.10M, 3, 1, 2); //6
            AddPayment(context, "payment7", 1010, 3, 1, 2); //7
            AddPayment(context, "payment8", 1000, 4, 1, 2); //8
            AddPayment(context, "payment9", 1000, 5, 1, 2); //9
            AddPayment(context, "payment10", 500, 6, 1, 3); //10
        }

        private void AddPayment(RaisinsContext context,
            string name,
            decimal amount,
            int beneficiaryID,
            int currencyID,
            int createdByID)
        {
            if (!context.Payments.Any(c => c.Name == name))
            {
                context.Payments.Add(new Payment()
                {
                    Name = name,
                    Amount = amount,
                    BeneficiaryID = beneficiaryID,
                    CurrencyID = currencyID,
                    CreatedByID = createdByID
                });
            }
        }
    }

}
