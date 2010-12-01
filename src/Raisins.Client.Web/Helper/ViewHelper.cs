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

        public static PaymentClassOption[] ClassOptions
        {
            get
            {
                Setting setting = Account.FindUser(HttpContext.Current.User.Identity.Name).Settings.FirstOrDefault();

                if (setting != null)
                {
                    if (setting.Class == PaymentClass.Internal)
                    {
                        return new List<PaymentClassOption>()
                        {
                            new PaymentClassOption() { ID = (int)PaymentClass.Internal, Name = PaymentClass.Internal.ToString() },
                            new PaymentClassOption() { ID = (int)PaymentClass.External, Name = PaymentClass.External.ToString() }
                        }.ToArray();
                    }
                    else
                    {
                        return new List<PaymentClassOption>()
                        {
                            new PaymentClassOption() { ID = (int)setting.Class, Name = setting.Class.ToString() }
                        }.ToArray();
                    }
                }

                return null;
            }
        }

        public static RoleType RoleType
        {
            get { return Account.FindUser(HttpContext.Current.User.Identity.Name).Role.RoleType; }
        }

        public static string GetItemClass(PaymentModel payment)
        {
            return payment.Locked ? "lockitem" : "item";
        }

    }

    public class PaymentClassOption
    {

        public int ID { get; set; }
        public string Name { get; set; }

    }
}