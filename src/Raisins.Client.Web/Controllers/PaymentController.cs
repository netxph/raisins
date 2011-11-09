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
        // GET: /Payment/

        public ActionResult Index()
        {
            Payment[] models = Payment.GetAll();
            
            ViewBag.CashOnHand = Payment.GetCashOnHand();
            ViewBag.RoleTypeIsUser = Account.CurrentUser.RoleType == (int)RoleType.User;

            if (Request.QueryString["Locked"] == "True")
            {
                models = models.Where(payment => payment.Locked).ToArray();
            }
            else if (Request.QueryString["Locked"] == "False")
            {
                models = models.Where(payment => !payment.Locked).ToArray();
            }

            return View(models);
        }

        //
        // GET: /Payment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Payment/Create

        public ActionResult Create()
        {
            ViewHelper.GetPaymentReferences(this);

            return View();
        } 

        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            try
            {
                

                Payment.CreateNew(payment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Payment/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = Payment.Get(id);

            ViewHelper.GetPaymentReferences(this);

            return View(model);
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Payment payment)
        {
            try
            {
                payment.PaymentID = id;

                Payment.Update(payment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Payment/Delete/5
 
        public ActionResult Delete(int id)
        {
            Payment.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
