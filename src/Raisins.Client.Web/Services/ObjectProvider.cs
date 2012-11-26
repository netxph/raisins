using Raisins.Client.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class ObjectProvider
    {

        static object _lockObject = new object();

        protected static RaisinsDB DB { get; set; }

        public static RaisinsDB CreateDB()
        {
            return new RaisinsDB();
        }

        public static IHttpHelper CreateHttpHelper()
        {
            return new HttpHelper();
        }

    }
}