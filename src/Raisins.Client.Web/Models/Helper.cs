using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Helper
    {

        public static string CreateSalt()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

    }
}