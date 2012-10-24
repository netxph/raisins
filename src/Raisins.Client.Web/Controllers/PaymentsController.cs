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
    public class PaymentsController : Controller
    {
        private RaisinsDB db = new RaisinsDB();

        //
        // GET: /Payments/

        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }

        //
        // GET: /Payments/Details/5

        public ActionResult Details(int id = 0)
        {
            Payment payment = db.Payments.Find(id);
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
            List<SelectListItem> currencyListItems = new List<SelectListItem>();
            List<Currency> currencies = Currency.GetAll();
            foreach (Currency currency in currencies)
            {
                currencyListItems.Add(new SelectListItem() { Value=currency.ID.ToString(), Text =currency.CurrencyCode} );
            }
            ViewBag.CurrencyIDList = new SelectList(currencyListItems, "Value", "Text");

            List<SelectListItem> beneficiaryListItems = new List<SelectListItem>();
            List<Beneficiary> beneficiaries = Beneficiary.GetAll();
            foreach (Beneficiary beneficiary in beneficiaries)
            {
                beneficiaryListItems.Add(new SelectListItem() { Value=beneficiary.ID.ToString(), Text=beneficiary.Name});
            }
            ViewBag.BeneficiaryIDList = new SelectList(beneficiaryListItems, "Value", "Text");
            return View();
        }

        //
        // POST: /Payments/Create

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            Beneficiary selectedBeneficiary = Beneficiary.Find(payment.Beneficiary.ID);
            payment.Beneficiary = selectedBeneficiary;

            Currency selectedCurrency = Currency.Find(payment.Currency.ID);
            payment.Currency = selectedCurrency;

            payment.Tickets = new List<Ticket>();

            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<Currency> currencies = Currency.GetAll();
            foreach (Currency currency in currencies)
            {
                selectListItems.Add(new SelectListItem() { Value = currency.ID.ToString(), Text = currency.CurrencyCode });
            }
            ViewBag.CurrencyIDList = new SelectList(selectListItems, "Value", "Text", payment.Currency);

            List<SelectListItem> beneficiaryListItems = new List<SelectListItem>();
            List<Beneficiary> beneficiaries = Beneficiary.GetAll();
            foreach (Beneficiary beneficiary in beneficiaries)
            {
                beneficiaryListItems.Add(new SelectListItem() { Value = beneficiary.ID.ToString(), Text = beneficiary.Name });
            }
            ViewBag.BeneficiaryIDList = new SelectList(beneficiaryListItems, "Value", "Text", payment.Beneficiary);
            return View(payment);
        }

        //
        // GET: /Payments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        //
        // POST: /Payments/Edit/5

        [HttpPost]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        //
        // GET: /Payments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Payment payment = db.Payments.Find(id);
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
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}