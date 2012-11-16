using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
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