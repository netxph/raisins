using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Security
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string name)
        {
            Name = name;

            Account = Account.FindUser(name);
        }

        public string AuthenticationType
        {
            get { return "Raisins Authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        public string Name { get; private set; }

        public Account Account { get; private set; }
    }
}