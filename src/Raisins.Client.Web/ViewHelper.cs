using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Controllers;

namespace Raisins.Client.Web
{
    public class ViewHelper
    {

        public static void GetPaymentReferences(PaymentController controller)
        {
            controller.ViewBag.Beneficiaries = Beneficiary.GetAll().ToList();
            controller.ViewBag.Currencies = Currency.GetAll().ToList();

            if (Account.CurrentUser.RoleType == (int)RoleType.User)
            {
                controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                             where e != PaymentClass.Foreign && e != PaymentClass.NotSpecified
                                             select new { ID = (int)e, Name = e.ToString() };
            }
            else
            {
                controller.ViewBag.Classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                                             select new { ID = (int)e, Name = e.ToString() };
            }
        }

    }
}