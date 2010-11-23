using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class AccountService
    {

        public static bool Logon(string userId, string password)
        {
            bool result = Account.Authenticate(userId, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(userId, false);
            }

            return result;
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}