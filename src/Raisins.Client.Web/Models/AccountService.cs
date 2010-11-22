using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Raisins.Client.Web.Models
{
    public class AccountService
    {

        public static void Logon(string userId, string password)
        {
            FormsAuthentication.SetAuthCookie(userId, false);    
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}