using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Data
{
    public class RaisinsInitializer : DropCreateDatabaseIfModelChanges<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            DataSeed.Start(context);    
        }

    }
}