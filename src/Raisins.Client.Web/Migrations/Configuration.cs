namespace Raisins.Client.Web.Migrations
{
    using Raisins.Client.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Diagnostics;
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

    //public sealed class Configuration : DbMigrationsConfiguration<RaisinsDB>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }

    //    protected override void Seed(RaisinsDB context)
    //    {
    //       // Seeder.Seed(context);
    //        Configuration configuration = new Configuration();
    //    configuration.ContextType = typeof(RaisinsDB);
    //    var migrator = new DbMigrator(configuration);

    //    //This will get the SQL script which will update the DB and will write it to debug
    //    var scriptor = new MigratorScriptingDecorator(migrator);
    //    string script = scriptor.ScriptUpdate(sourceMigration: null, targetMigration: null).ToString();
    //    Debug.Write(script);

    //    //This will run the migration update script and will run Seed() method
    //    migrator.Update();
    //    } 
        
       
    //}

        

   
}

