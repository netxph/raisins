using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Services;
using System.ComponentModel.DataAnnotations;

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

        public ActionResult LockLocal()
        {
            Payment.LockLocal();

            return RedirectToAction("Index");
        }

        public ActionResult LockForeign()
        {
            Payment.LockForeign();

            return RedirectToAction("Index");
        }

        public ActionResult Resend()
        {
            Payment.ResendEmail();

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
            var payments = Payment.FindAllForUser();

            string sortBy = Request.QueryString["SortBy"];

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "Beneficiary":
                        payments.Sort((p1, p2) => p1.Beneficiary.Name.CompareTo(p2.Beneficiary.Name));
                        break;

                    case "Class":
                        payments.Sort((p1, p2) => p1.ClassID.CompareTo(p2.ClassID));
                        break;

                    case "Name":
                        payments.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
                        break;

                    case "Currency":
                        payments.Sort((p1, p2) => p1.Currency.CurrencyCode.CompareTo(p2.Currency.CurrencyCode));
                        break;
                }
            }

            ViewBag.CanLock = Activity.IsInRole("Payment.Lock");
            ViewBag.CanEdit = Activity.IsInRole("Payment.Edit");

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

        [HttpPost]
        public ActionResult Details(string email, int ID)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                //TODO: place in payments model
                var payment = db.Payments.First(p => p.ID == ID);
                payment.Email = email;

                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Payments/Create

        public ActionResult Create()
        {
            var user = Account.GetCurrentUser();

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = Executive.GetAll();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            if (user.Profile.IsLocal)
            {
                paymentClasses.Remove(paymentClasses.Single(p => p.Name == "Foreign"));
            }

            ViewBag.BeneficiaryID = new SelectList(user.Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(user.Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

            ViewBag.CanLock = Activity.IsInRole("Payment.Lock");
            ViewBag.CanEdit = Activity.IsInRole("Payment.Edit");
            return View();
        }

        public ActionResult CreateLocal()
        {
            var user = Account.GetCurrentUser();

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = Executive.GetAll();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            if (user.Profile.IsLocal)
            {
                paymentClasses.Remove(paymentClasses.Single(p => p.Name == "Foreign"));
            }

            ViewBag.BeneficiaryID = new SelectList(user.Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(user.Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

            return View();
        }

        //
        // POST: /Payments/Create

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                if (payment.ExecutiveID == -1) payment.ExecutiveID = null;

                Payment.Add(payment);
                return RedirectToAction("Index");
            }

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = Executive.GetAll();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", 0);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", 0);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

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

            if (payment.ExecutiveID == null) payment.ExecutiveID = -1;

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = Executive.GetAll();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", payment.CurrencyID);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", payment.ClassID);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name", payment.ExecutiveID);

            ViewBag.CanLock = Activity.IsInRole("Payment.Lock");
            ViewBag.CanEdit = Activity.IsInRole("Payment.Edit");

            return View(payment);
        }

        //
        // POST: /Payments/Edit/5

        [HttpPost]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                if (payment.ExecutiveID == -1) payment.ExecutiveID = null;

                Payment.Edit(payment);
                return RedirectToAction("Index");
            }

            if (payment.ExecutiveID == null) payment.ExecutiveID = -1;

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = Executive.GetAll();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(Account.GetCurrentUser().Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(Account.GetCurrentUser().Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", (int)payment.ClassID);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

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

        protected bool IsInRole(string activityName)
        {
            return Activity.IsInRole(activityName);
        }

        public ActionResult Email(int id)
        {
            Payment.ResendEmail(id);

            return RedirectToAction("Index");
        }
    }
}