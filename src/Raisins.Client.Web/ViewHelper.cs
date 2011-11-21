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
            GetPaymentReferences(controller, null);
        }

        public static string FormatName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }


        public static void GetPaymentReferences(PaymentController controller, Payment model)
        {
            var beneficiaries = Beneficiary.GetAllForPayment().ToList();
            var currencies = Currency.GetAllForPayment().ToList();

            int beneficiaryID = model != null ? model.Beneficiary.BeneficiaryID : Account.CurrentUser.Setting.BeneficiaryID;
            int currencyID = model != null ? model.Currency.CurrencyID : Account.CurrentUser.Setting.CurrencyID;
            int paymentClass = model != null ? model.Class : Account.CurrentUser.Setting.Class;

            controller.ViewBag.Beneficiaries = new SelectList(beneficiaries, "BeneficiaryID", "Name", beneficiaryID);
            controller.ViewBag.Currencies = new SelectList(currencies, "CurrencyID", "CurrencyCode", currencyID);

            if (Account.CurrentUser.RoleType == (int)RoleType.User)
            {
                var classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                              where e != PaymentClass.Foreign && e != PaymentClass.NotSpecified
                              select new { ID = (int)e, Name = e.ToString() };

                controller.ViewBag.Classes = new SelectList(classes, "ID", "Name", paymentClass);
            }
            else
            {
                var classes = from PaymentClass e in Enum.GetValues(typeof(PaymentClass))
                              select new { ID = (int)e, Name = e.ToString() };
                controller.ViewBag.Classes = new SelectList(classes, "ID", "Name", paymentClass);
            }
        }
    }
}