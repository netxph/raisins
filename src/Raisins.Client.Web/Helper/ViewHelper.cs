using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Raisins.Client.Web.Models;
using Raisins.Services;

namespace Raisins.Client.Web.Helper
{
    public class ViewHelper
    {

        public static string Title
        { 
            get { return ConfigurationManager.AppSettings["app.title"]; } 
        }
 
        public static string Description 
        {
            get { return ConfigurationManager.AppSettings["app.description"]; }
        }

        public static string Version
        {
            get { return ConfigurationManager.AppSettings["app.version"]; }
        }

        //resources specific models
        public static BeneficiaryModel[] Beneficiaries
        {
            get { return BeneficiaryService.FindByUser(HttpContext.Current.User.Identity.Name); }
        }

        public static RoleType RoleType
        {
            get { return Account.FindUser(HttpContext.Current.User.Identity.Name).Role.RoleType; }
        }

    }
}