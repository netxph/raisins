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
            Seeders = new List<IDbSeeder>();
            Seeders.Add(new RoleSeed());
            Seeders.Add(new AccountSeed());
            Seeders.Add(new BeneficiarySeed());
            Seeders.Add(new CurrencySeed());
            //Seeders.Add(new PaymentSeed());
            Seeders.Add(new PaymentSourceSeed());
            Seeders.Add(new PaymentTypeSeed());
            
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
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
