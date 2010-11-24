using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Helper;

namespace Raisins.Client.Web.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View(PaymentService.FindAll());
        }

        //
        // GET: /Account/Details/5

        public ActionResult Details(long id)
        {
            return View(PaymentService.GetPayment(id));
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            ViewData["ExistingPayments"] = PaymentService.FindAllByUser(HttpContext.User.Identity.Name);
            ViewData["Beneficiaries"] = BeneficiaryService.FindByUser(HttpContext.User.Identity.Name);
            
            PaymentModel model = new PaymentModel();
            SettingModel setting = SettingService.GetSetting(HttpContext.User.Identity.Name);

            model.Currency = setting.Currency;
            model.Location = setting.Location;
            model.BeneficiaryID = setting.BeneficiaryID;
            model.Class = setting.Class;

            return View(model);
        } 

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(PaymentModel model)
        {
            try
            {
                SettingModel setting = SettingService.GetSetting(HttpContext.User.Identity.Name);

                List<string> errorList = new List<string>();
                if (!Validator.IsAmountValid(model.Amount))
                {
                    errorList.Add("Please enter an amount that's greater than 0.");
                }

                if (setting != null && setting.Currency != null && !Validator.IsAmountWithinRatio(setting.Currency.Ratio, model.Amount))
                {
                    errorList.Add("Please enter an amount that's divisible by "+setting.Currency.Ratio);
                }

                if (errorList.Count > 0)
                {
                    model.Currency = setting.Currency;
                    model.Location = setting.Location;
                    model.BeneficiaryID = setting.BeneficiaryID;
                    model.Class = setting.Class;

                    ViewData["ExistingPayments"] = PaymentService.FindAllByUser(HttpContext.User.Identity.Name);
                    ViewData["Beneficiaries"] = BeneficiaryService.FindByUser(HttpContext.User.Identity.Name);

                    ViewData["Exceptions"] = errorList;

                    return View(model);
                }

                PaymentService.Save(model);

                model = new PaymentModel();

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Account/Edit/5
 
        public ActionResult Edit(long id)
        {
            return View(PaymentService.GetPayment(id));
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentModel model)
        {
            try
            {
                SettingModel setting = SettingService.GetSetting(HttpContext.User.Identity.Name);

                if (setting == null)
                {
                    return RedirectToAction("Edit");
                }

                List<string> errorList = new List<string>();
                if (!Validator.IsAmountValid(model.Amount))
                {
                    errorList.Add("Please enter an amount that's greater than 0.");
                }

                if (setting != null && setting.Currency != null && !Validator.IsAmountWithinRatio(setting.Currency.Ratio, model.Amount))
                {
                    errorList.Add("Please enter an amount that's divisible by " + setting.Currency.Ratio);
                }

                if (errorList.Count > 0)
                {
                    ViewData["Exceptions"] = errorList;
                    return View(PaymentService.GetPayment(model.ID));
                }

                PaymentModel original = PaymentService.GetPayment(model.ID);

                model.Currency = original.Currency;
                model.Email = original.Email;
                model.Location = original.Location;
                model.BeneficiaryID = original.BeneficiaryID;

                PaymentService.Update(model);
 
                return RedirectToAction("Create");

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(PaymentService.GetPayment(id));
        }

        //
        // POST: /Account/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                PaymentService.Delete(id);
 
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }
    }
}
