using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Raisins.Client.Web.Models;

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

        public static List<BeneficiaryModel> GetBeneficiaryAccordingToClass(SettingModel settingModel)
        {
            List<BeneficiaryModel> beneficiaries = new List<BeneficiaryModel>();
            return beneficiaries;
        }

    }
}