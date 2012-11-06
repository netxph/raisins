using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Web.Services
{
    public class AuthorizeActivityAttribute : AuthorizeAttribute
    {

        public string ActivityName { get; set; }

        public AuthorizeActivityAttribute(string activityName)
        {
            ActivityName = activityName;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var account = Account.GetCurrentUser();
            return Activity.IsInRole(ActivityName, account.Roles.ToList());
        }

    }
}