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
            Seeder.Seed(context);
        }

    }
}