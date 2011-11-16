using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Controllers;
using System.Collections;

namespace Raisins.Client.Web
{
    public class ViewHelper
    {

        public static void GetPaymentReferences(PaymentController controller)
        {
            controller.ViewBag.Beneficiaries = Beneficiary.GetAllForUser().ToList();
            controller.ViewBag.Currencies = Currency.GetAll().ToList();

            if (Account.CurrentUser.RoleType == (int)RoleType.User)
            {
                switch ((PaymentClass)Account.CurrentUser.Setting.Class)
                {
                    case PaymentClass.Internal:
                        controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                                     where e != PaymentClass.Foreign && e != PaymentClass.NotSpecified
                                                     select new { ID = (int)e, Name = e.ToString() };
                        break;
                    case PaymentClass.Foreign:
                        controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                                     where e != PaymentClass.Internal && e != PaymentClass.NotSpecified
                                                     select new { ID = (int)e, Name = e.ToString() };
                        break;
                    case PaymentClass.External:
                    case PaymentClass.NotSpecified:
                        controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                                     where e != PaymentClass.NotSpecified
                                                     select new { ID = (int)e, Name = e.ToString() };
                        break;
                }

            }
            else
            {
                controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                             select new { ID = (int)e, Name = e.ToString() };
            }

            controller.ViewBag.SelectedClass = ((PaymentClass)Account.CurrentUser.Setting.Class).ToString();
            controller.ViewBag.SelectedCurrency = Currency.Get(Account.CurrentUser.Setting.CurrencyID).CurrencyCode;

        }

        public static string FormatName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }

    }
}