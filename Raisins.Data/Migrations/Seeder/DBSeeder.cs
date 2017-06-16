using Raisins.Data.Migrations.Seeder.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder
{
    public class DBSeeder
    {
        public DBSeeder()
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            Seeders = new List<IDbSeeder>();
            //Seeders.Add(new BeneficiarySeed()); // to remove to after
            Seeders.Add(new RoleSeed());
            Seeders.Add(new AccountSeed());
            Seeders.Add(new CurrencySeed());
            Seeders.Add(new PaymentSourceSeed());
            Seeders.Add(new PaymentTypeSeed());
            //Seeders.Add(new PaymentSeed());
        }
        public List<IDbSeeder> Seeders { get; set; }
        public void Seed(RaisinsContext context)
        {
            foreach (var seeder in Seeders)
            {
                seeder.Seed(context);
            }
        }
    }
}
