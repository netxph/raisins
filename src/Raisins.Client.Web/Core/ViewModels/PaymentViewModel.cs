using Raisins.Client.Web.Controllers;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Raisins.Client.Web.Core.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        {

        }

        public PaymentViewModel(Payment payment)
        {
            Id = payment.ID;
            Name = payment.Name;
            Location = payment.Location;
            Email = payment.Email;
            Amount = payment.Amount;
            CurrencyId = payment.CurrencyID;
            BeneficiaryId = payment.BeneficiaryID;
            PaymentClassId = payment.ClassID;
            SoldBy = payment.SoldBy;
            Remarks = payment.Remarks;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string SoldBy { get; set; }
        public string Remarks { get; set; }
        public bool CanLock { get; set; }
        public bool CanEdit { get; set; }
        public int BeneficiaryId { get; set; }

        public IEnumerable<Beneficiary> Beneficiaries;

        public int CurrencyId { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }

        public int PaymentClassId { get; set; }
        public IEnumerable<SelectListItem> PaymentClasses { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<PaymentsController, ActionResult>> update =
                    (c => c.Edit(this));
                Expression<Func<PaymentsController, ActionResult>> create =
                    (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public void InitializePaymentFormResources(List<Beneficiary> beneficiaries, 
                                                List<Currency> currencies,
                                                List<SelectListItem> paymentClasses,
                                                bool canLock,
                                                bool canEdit)
        {
            Beneficiaries = beneficiaries;
            Currencies = currencies;
            PaymentClasses = paymentClasses;
            CanLock = canLock;
            CanEdit = canEdit;
        }

    }
}