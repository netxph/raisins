using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Services
{
    public class HttpHelper : IHttpHelper
    {
        #region IHttpHelper Members

        public string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        #endregion
    }
}