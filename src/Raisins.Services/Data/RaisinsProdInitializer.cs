using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Services.Data
{
    public class RaisinsProdInitializer : BaseDatabaseInitializer<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            base.Seed(context);
            DataSeed.Start(context);
        }

    }
}