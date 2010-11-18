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
            ViewData["Beneficiaries"] = BeneficiaryService.FindAll();

            return View(new PaymentModel());
        } 

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(PaymentModel model)
        {
            try
            {
                SettingModel setting = SettingService.GetSetting(User.Identity.Name.ToLowerInvariant());

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
            ViewData["Beneficiaries"] = BeneficiaryService.FindAll();
            return View(PaymentService.GetPayment(id));
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentModel model)
        {
            try
            {
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
            ViewData["Beneficiaries"] = BeneficiaryService.FindAll();
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
