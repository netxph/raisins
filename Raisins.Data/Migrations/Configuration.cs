namespace Raisins.Data.Migrations
{
    using Seeder;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Raisins.Data.RaisinsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Raisins.Data.RaisinsContext context)
        {
            var seeder = new DBSeeder();
            seeder.Seed(context);
        }
    }
}
