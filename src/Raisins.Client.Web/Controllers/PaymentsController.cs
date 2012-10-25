using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{

    [Authorize]
    public class PaymentsController : Controller
    {
        //
        // GET: /Payments/

        public ActionResult Index()
        {
            var payments = Payment.GetAll();

            return View(payments);
        }

        //
        // GET: /Payments/Details/5

        public ActionResult Details(int id = 0)
        {
            Payment payment = Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        //
        // GET: /Payments/Create

        public ActionResult Create()
        {
            ViewBag.BeneficiaryID = new SelectList(Beneficiary.GetAll(), "ID", "Name");
            ViewBag.CurrencyID = new SelectList(Currency.GetAll(), "ID", "CurrencyCode");
            
            return View();
        }

        //
        // POST: /Payments/Create

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                Payment.Add(payment);
                return RedirectToAction("Index");
            }

            ViewBag.BeneficiaryID = new SelectList(Beneficiary.GetAll(), "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(Currency.GetAll(), "ID", "CurrencyCode", payment.CurrencyID);
            
            return View(payment);
        }

        //
        // GET: /Payments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Payment payment = Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryID = new SelectList(Beneficiary.GetAll(), "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(Currency.GetAll(), "ID", "CurrencyCode", payment.CurrencyID);

            return View(payment);
        }

        //
        // POST: /Payments/Edit/5

        [HttpPost]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                Payment.Edit(payment);
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryID = new SelectList(Beneficiary.GetAll(), "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(Beneficiary.GetAll(), "ID", "CurrencyCode", payment.CurrencyID);
            
            return View(payment);
        }

        //
        // GET: /Payments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Payment payment = Payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        //
        // POST: /Payments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment.Delete(id);
            return RedirectToAction("Index");
        }

    }
}