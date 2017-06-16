using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class PaymentSourceSeed : IDbSeeder
    {
        public void Seed(RaisinsContext context)
        {
            Addsource(context, "Local");
            Addsource(context, "International");
            Addsource(context, "External");
        }

        private void Addsource(
            RaisinsContext context,
            string source)
        {
            if (!context.Sources.Any(c => c.Source == source))
            {
                context.Sources.Add(new PaymentSource() { Source = source });
                context.SaveChanges();
            }
        }
    }
}