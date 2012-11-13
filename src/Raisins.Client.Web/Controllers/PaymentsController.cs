using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Services;

namespace Raisins.Client.Web.Controllers
{

    [Authorize]
    public class PaymentsController : Controller
    {

        public ActionResult LockAll()
        {
            Payment.LockAll();

            return RedirectToAction("Index");
        }

        //
        // GET: /Payments/Manage
        [AllowAnonymous]
        public ActionResult Manage()
        {
            return View();
        }

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
            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            
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

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", 0);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", 0);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            
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
            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", payment.CurrencyID);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name",payment.ClassID);

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
            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", (int)payment.ClassID);
            
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