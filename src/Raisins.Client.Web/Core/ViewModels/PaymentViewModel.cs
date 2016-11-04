using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Raisins.Client.Web.Core.ViewModels
{
    public class PaymentViewModel
    {
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