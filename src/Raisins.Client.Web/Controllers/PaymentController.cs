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
            PaymentModel model = PaymentService.CreateNew();
            
            return View(model);
        } 

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(PaymentModel model)
        {
            try
            {
                PaymentService.Save(model);
                return RedirectToAction("Create");
            }
            catch
            {
                return View(model);
            }
        }
        
        //
        // GET: /Account/Edit/5
 
        public ActionResult Edit(long id)
        {
            PaymentModel model = PaymentService.GetPayment(id);

            return View(model);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentModel model)
        {

            try
            {
                PaymentService.Update(model);
                return RedirectToAction("Create");
            }
            catch
            {
                return View(model);
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                PaymentService.Delete(id);
            }
            catch
            {
            }

            return RedirectToAction("Index");
        }

        //
        // POST: /Account/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

        public ActionResult LockAll()
        {
            PaymentService.LockAll();

            return RedirectToAction("Index");
        }
    }
}
