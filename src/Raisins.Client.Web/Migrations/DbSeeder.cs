using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Migrations
{
    public class DbSeeder
    {

        public DbSeeder()
        {
            //register seeders
            Seeders = new List<IDbSeeder>();
            Seeders.Add(new RoleSeed());
            Seeders.Add(new ActivitySeed());
            Seeders.Add(new CurrencySeed());
            Seeders.Add(new BeneficiarySeed());
            Seeders.Add(new UserSeed());
            Seeders.Add(new Group7Migration());
            
        }

        public List<IDbSeeder> Seeders { get; set; }

        public void Seed(RaisinsDB context)
        {
            foreach (var seeder in Seeders)
            {
                seeder.Seed(context);
            }


        }

    }
}