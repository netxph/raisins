using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Raisins.Services.Models;

namespace Raisins.Services.Data
{
    public class RaisinsInitializer : DropCreateDatabaseIfModelChanges<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            DataSeed.Start(context);    
        }

    }
}