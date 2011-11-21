using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Controllers;
using System.Web.Mvc;

namespace Raisins.Client.Web
{
    public class ViewHelper
    {

        public static void GetPaymentReferences(PaymentController controller)
        {
            var beneficiaries = Beneficiary.GetAllForPayment().ToList();
            var currencies = Currency.GetAllForPayment().ToList();

            controller.ViewBag.Beneficiaries = new SelectList(beneficiaries, "BeneficiaryID", "Name", Account.CurrentUser.Setting.BeneficiaryID);
            controller.ViewBag.Currencies = new SelectList(currencies, "CurrencyID", "CurrencyCode", Account.CurrentUser.Setting.CurrencyID);

            if (Account.CurrentUser.RoleType == (int)RoleType.User)
            {
                var classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                              where e != PaymentClass.Foreign && e != PaymentClass.NotSpecified
                              select new { ID = (int)e, Name = e.ToString() };

                controller.ViewBag.Classes = new SelectList(classes, "ID", "Name", Account.CurrentUser.Setting.Class);
            }
            else
            {
                var classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                              select new { ID = (int)e, Name = e.ToString() };
                controller.ViewBag.Classes = new SelectList(classes, "ID", "Name", Account.CurrentUser.Setting.Class);
            }
        }

        public static string FormatName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }

    }
}