using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raisins.Client.Web.Migrations
{
    public interface IDbSeeder
    {

        void Seed(RaisinsDB context);

    }
}
