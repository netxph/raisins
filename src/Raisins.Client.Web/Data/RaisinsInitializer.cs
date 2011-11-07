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
            base.Seed(context);

            string salt = Account.GetSalt();

            var adminAccount = new Account()
            {
                UserName = "admin",
                Salt = salt,
                Password = Account.GetHash("r@isin5", salt)
            };

            context.Accounts.Add(adminAccount);
        }

    }
}