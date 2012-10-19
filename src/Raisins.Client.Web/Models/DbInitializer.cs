using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<RaisinsDB>
    {

        protected override void Seed(RaisinsDB context)
        {
            AccountService service = new AccountService(context);
            service.CreateUser("admin", "R@isin5");
        }

    }
}