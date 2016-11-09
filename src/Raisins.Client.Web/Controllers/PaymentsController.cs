using Raisins.Client.Web.Core;
using Raisins.Client.Web.Core.ViewModels;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Services;
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

                    MailQueue mailQueue = new MailQueue(payment);
                    _unitOfWork.MailQueues.Add(mailQueue);
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
                            .Where(p => p.ClassID == (int)PaymentClass.Local).ToList();

            foreach (var payment in payments)
            {
                if (!payment.Locked)
                {
                    payment.Locked = true;
                    payment.AuditedByID = currentAccount.ID;
                    payment.Tickets = payment.GenerateTickets();
                    MailQueue mailQueue = new MailQueue(payment);
                    _unitOfWork.MailQueues.Add(mailQueue);
                    _unitOfWork.Payments.Edit(payment);
                    _unitOfWork.Complete();
                }
            }
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

                    MailQueue mailQueue = new MailQueue(payment);
                    _unitOfWork.MailQueues.Add(mailQueue);
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
                MailQueue mailQueue = new MailQueue(payment);
                _unitOfWork.MailQueues.Add(mailQueue);
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
            ViewBag.CanLock = paymentLockActivity.IsUserAllowed(userAccount.Roles);
            Activity paymentEditActivity = _unitOfWork.Activities.GetActivityByName("Payment.Edit");
            ViewBag.CanEdit = paymentEditActivity.IsUserAllowed(userAccount.Roles);

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
            PaymentViewModel viewModel = new PaymentViewModel();
            viewModel.InitializePaymentFormResources(user.Profile.Beneficiaries,
                                                     user.Profile.Currencies,
                                                     EnumHelper.GetEnumSelectList<PaymentClass>(),
                                                     _unitOfWork.Activities.GetActivityByName("Payment.Lock").IsUserAllowed(user.Roles),
                                                     _unitOfWork.Activities.GetActivityByName("Payment.Edit").IsUserAllowed(user.Roles));

            return View("PaymentForm", viewModel);
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
        public ActionResult Create(PaymentViewModel paymentViewModel)
        {
            if (!ModelState.IsValid)
            {
                var user = _unitOfWork.Accounts.GetCurrentUserAccount();
                paymentViewModel.InitializePaymentFormResources(user.Profile.Beneficiaries,
                                                         user.Profile.Currencies,
                                                         EnumHelper.GetEnumSelectList<PaymentClass>(),
                                                         _unitOfWork.Activities.GetActivityByName("Payment.Lock").IsUserAllowed(user.Roles),
                                                         _unitOfWork.Activities.GetActivityByName("Payment.Edit").IsUserAllowed(user.Roles));

                return View("PaymentForm", paymentViewModel);
            }

            Payment payment = new Payment(paymentViewModel);
            payment.CreatedByID = _unitOfWork.Accounts.GetCurrentUserAccount().ID;
            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
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

            PaymentViewModel viewModel = new PaymentViewModel(payment);
            var user = _unitOfWork.Accounts.GetCurrentUserAccount();
            viewModel.InitializePaymentFormResources(user.Profile.Beneficiaries,
                                                     user.Profile.Currencies,
                                                     EnumHelper.GetEnumSelectList<PaymentClass>(),
                                                     _unitOfWork.Activities.GetActivityByName("Payment.Lock").IsUserAllowed(user.Roles),
                                                     _unitOfWork.Activities.GetActivityByName("Payment.Edit").IsUserAllowed(user.Roles));
            return View("PaymentForm", viewModel);
        }

        //
        // POST: /Payments/Edit/5

        [HttpPost]
        public ActionResult Edit(PaymentViewModel paymentViewModel)
        {
            if (!ModelState.IsValid)
            {
                var user = _unitOfWork.Accounts.GetCurrentUserAccount();
                paymentViewModel.InitializePaymentFormResources(user.Profile.Beneficiaries,
                                                         user.Profile.Currencies,
                                                         EnumHelper.GetEnumSelectList<PaymentClass>(),
                                                         _unitOfWork.Activities.GetActivityByName("Payment.Lock").IsUserAllowed(user.Roles),
                                                         _unitOfWork.Activities.GetActivityByName("Payment.Edit").IsUserAllowed(user.Roles));

                return View("PaymentForm", paymentViewModel);
            }
            Payment payment = new Payment(paymentViewModel);
            payment.CreatedByID = _unitOfWork.Accounts.GetCurrentUserAccount().ID;
            _unitOfWork.Payments.Edit(payment);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
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
            MailQueue mailQueue = new MailQueue(payment);
            _unitOfWork.MailQueues.Add(mailQueue);

            return RedirectToAction("Index");
        }
    }
}