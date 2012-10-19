using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class RaisinsDB : DbContext
    {

        public RaisinsDB()
            : base("Default")
        {

        }

        public DbSet<Account> Accounts { get; set; }

    }
}