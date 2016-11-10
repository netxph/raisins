using System;
using System.Data.Entity;

namespace Raisins.Client.Web.Persistence
{
    public class DbInitializer : DropCreateDatabaseAlways<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            throw new NotSupportedException("PLEASE!!!! Check first your connection string before uncommenting this code, after you're done, but this back in.");

            //Seeder.Seed(context);
        }

    }
}