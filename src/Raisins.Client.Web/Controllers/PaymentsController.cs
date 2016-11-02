using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Raisins.Client.Web.Controllers
{

    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult LockAll()
        {
            Account currentAccount = _unitOfWork.Accounts.GetCurrentUserAccount();
            List<Payment> payments = _unitOfWork.Payments.GetPayment(currentAccount).ToList();

            foreach (var payment in payments)
            {
                if (!payment.Locked)
                {
                    _unitOfWork.Payments.Edit(payment);

                    payment.Locked = true;
                    payment.AuditedByID = currentAccount.ID;
                    payment.Tickets = payment.GenerateTickets();

                    string beneficiaryName = _unitOfWork.Beneficiaries.Find(payment.ID).Name;
                    payment.EmailTickets(beneficiaryName);
                }
            }

            _unitOfWork.Complete();
            

            return RedirectToAction("Index");
        }

        public ActionResult LockLocal()
        {

            Account currentAccount = _unitOfWork.Accounts.GetCurrentUserAccount();

            var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

            List<Payment> payments = _unitOfWork.Payments.GetPaymentByBeneficiary(beneficiaryIds)
                            .Where(p => p.ClassID == (int)PaymentClass.Local 
                                    || p.ClassID == (int)PaymentClass.External).ToList();

            foreach (var payment in payments)
            {
                if (!payment.Locked)
                {
                    _unitOfWork.Payments.Edit(payment);

                    payment.Locked = true;
                    payment.AuditedByID = currentAccount.ID;
                    payment.Tickets = payment.GenerateTickets();

                    string beneficiaryName = _unitOfWork.Beneficiaries.Find(payment.ID).Name;
                    payment.EmailTickets(beneficiaryName);
                }
            }

            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public ActionResult LockForeign()
        {

            Account currentAccount = _unitOfWork.Accounts.GetCurrentUserAccount();

            var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

            List<Payment> payments = _unitOfWork.Payments.GetPaymentByBeneficiary(beneficiaryIds)
                                        .Where(p => p.ClassID == (int)PaymentClass.Foreign).ToList();

            foreach (var payment in payments)
            {
                if (!payment.Locked)
                {
                    _unitOfWork.Payments.Edit(payment);

                    payment.Locked = true;
                    payment.AuditedByID = currentAccount.ID;
                    payment.Tickets = payment.GenerateTickets();

                    string beneficiaryName = _unitOfWork.Beneficiaries.Find(payment.ID).Name;
                    payment.EmailTickets(beneficiaryName);
                }
            }
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Resend()
        {
            IEnumerable<Payment> payments = _unitOfWork.Payments.GetLockedPayments();
            foreach(Payment payment in payments)
            {
                string beneficiaryName = _unitOfWork.Beneficiaries.Find(payment.ID).Name;
                payment.EmailTickets(beneficiaryName);
            }

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
            Account userAccount = _unitOfWork.Accounts.GetCurrentUserAccount();
            List<Payment> payments = _unitOfWork.Payments.GetPayment(userAccount).ToList();

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

            Activity paymentLockActivity = _unitOfWork.Activities.GetActivityByName("Payment.Lock");
            ViewBag.CanLock = paymentLockActivity.DoUserRolesExists(userAccount.Roles);
            Activity paymentEditActivity = _unitOfWork.Activities.GetActivityByName("Payment.Edit");
            ViewBag.CanEdit = paymentEditActivity.DoUserRolesExists(userAccount.Roles);

            return View(payments);
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            Payment payment = _unitOfWork.Payments.GetPayment(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        [HttpPost]
        public ActionResult Details(string email, int ID)
        {
            var payment = _unitOfWork.Payments.GetPayment(ID);
            payment.Email = email;
            _unitOfWork.Payments.Edit(payment);
            _unitOfWork.Complete();

            return RedirectToAction("Index");  
        }

        [HttpGet]
        public ActionResult Create()
        {
            var user = _unitOfWork.Accounts.GetCurrentUserAccount();

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = _unitOfWork.Executives.GetAll().ToList();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            if (user.Profile.IsLocal)
            {
                paymentClasses.Remove(paymentClasses.Single(p => p.Name == "Foreign"));
            }

            ViewBag.BeneficiaryID = new SelectList(user.Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(user.Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

            Activity paymentLockActivity = _unitOfWork.Activities.GetActivityByName("Payment.Lock");
            ViewBag.CanLock = paymentLockActivity.DoUserRolesExists(user.Roles);
            Activity paymentEditActivity = _unitOfWork.Activities.GetActivityByName("Payment.Edit");
            ViewBag.CanEdit = paymentEditActivity.DoUserRolesExists(user.Roles);

            return View();
        }

        [HttpGet]
        public ActionResult CreateLocal()
        {
            var user = _unitOfWork.Accounts.GetCurrentUserAccount();

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = _unitOfWork.Executives.GetAll().ToList();
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

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                if (payment.ExecutiveID == -1) payment.ExecutiveID = null;

                _unitOfWork.Payments.Add(payment);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = _unitOfWork.Executives.GetAll().ToList();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Beneficiaries, "ID", "Name", 0);
            ViewBag.CurrencyID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Currencies, "ID", "CurrencyCode", 0);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", 0);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

            return View(payment);
        }

        //
        // GET: /Payments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Payment payment = _unitOfWork.Payments.GetPayment(id);
            if (payment == null)
            {
                return HttpNotFound();
            }

            if (payment.ExecutiveID == null) payment.ExecutiveID = -1;

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = _unitOfWork.Executives.GetAll().ToList();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Beneficiaries, "ID", "Name", payment.BeneficiaryID);
            ViewBag.CurrencyID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Currencies, "ID", "CurrencyCode", payment.CurrencyID);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", payment.ClassID);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name", payment.ExecutiveID);

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

                _unitOfWork.Payments.Edit(payment);
                return RedirectToAction("Index");
            }

            if (payment.ExecutiveID == null) payment.ExecutiveID = -1;

            var paymentClasses = Enum.GetNames(typeof(PaymentClass)).Select(p => new { ID = (int)Enum.Parse(typeof(PaymentClass), p), Name = p }).ToList();
            var executives = _unitOfWork.Executives.GetAll().ToList();
            executives.Insert(0, new Executive() { ID = -1, Name = "[Select an executive...]" });

            ViewBag.BeneficiaryID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Beneficiaries, "ID", "Name", 1);
            ViewBag.CurrencyID = new SelectList(_unitOfWork.Accounts.GetCurrentUserAccount().Profile.Currencies, "ID", "CurrencyCode", 1);
            ViewBag.ClassID = new SelectList(paymentClasses, "ID", "Name", (int)payment.ClassID);
            ViewBag.ExecutiveID = new SelectList(executives, "ID", "Name");

            _unitOfWork.Complete();
            return View(payment);
        }

        //
        // GET: /Payments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Payment payment = _unitOfWork.Payments.GetPayment(id);
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
            Payment payment = _unitOfWork.Payments.GetPayment(id);
            _unitOfWork.Payments.Delete(payment);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public ActionResult Email(int id)
        {
            Payment payment = _unitOfWork.Payments.GetPayment(id);
            string beneficiaryName = _unitOfWork.Beneficiaries.Find(payment.ID).Name;
            payment.EmailTickets(beneficiaryName);

            return RedirectToAction("Index");
        }
    }
}