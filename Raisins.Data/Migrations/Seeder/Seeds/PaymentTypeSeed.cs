using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class PaymentTypeSeed : IDbSeeder
    {
        public void Seed(RaisinsContext context)
        {
            AddType(context, "Cash");
            AddType(context, "Bank Deposit");
            AddType(context, "PayPal");
        }

        private void AddType(
            RaisinsContext context,
            string type)
        {
            if (!context.Types.Any(c => c.Type == type))
            {
                context.Types.Add(new PaymentType() { Type = type});
                context.SaveChanges();
            }
        }
    }
}