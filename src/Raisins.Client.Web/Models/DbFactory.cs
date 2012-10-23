using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class DbFactory
    {

        static object _lockObject = new object();

        static RaisinsDB _db = null;
        protected static RaisinsDB DB { get; set; }

        public static void Store(RaisinsDB db)
        {
            lock (_lockObject)
            {
                DB = db;
            }
        }

        public static RaisinsDB Create()
        {
            if (DB == null)
            {
                return new RaisinsDB();
            }

            return DB;
        }

    }
}