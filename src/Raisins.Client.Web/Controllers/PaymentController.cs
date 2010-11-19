using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

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
            return View(new PaymentModel());
        } 

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(PaymentModel model)
        {
            try
            {
                if (model.Amount <= 0 || model.Amount % 50 != 0)
                {
                    return RedirectToAction("Create");
                }

                SettingModel setting = null;

                if (!User.Identity.IsAuthenticated)
                {
                    setting = SettingService.GetSetting(Request.ServerVariables["LOGON_USER"]);
                }
                else
                {
                    setting = SettingService.GetSetting(User.Identity.Name);
                } 

                model.Currency = setting.Currency;
                model.Location = setting.Location;
                model.BeneficiaryID = setting.BeneficiaryID;

                PaymentService.Save(model);

                return RedirectToAction("Index");
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
                if (model.Amount <= 0 || model.Amount % 50 != 0)
                {
                    return RedirectToAction("Edit");
                }

                PaymentModel original = PaymentService.GetPayment(model.ID);

                model.Currency = original.Currency;
                model.Location = original.Location;
                model.BeneficiaryID = original.BeneficiaryID;

                PaymentService.Update(model);
 
                return RedirectToAction("Index");
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
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
