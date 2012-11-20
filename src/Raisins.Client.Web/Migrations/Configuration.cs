namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RaisinsDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RaisinsDB context)
        {
            //Seeder.Seed(context);
        }
    }
}
