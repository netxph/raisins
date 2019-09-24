using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.ViewModels
{
    public class PublishAllViewModel
    {

        private string _selected;

        public string SelectedBeneficiary
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public int paymentID { get; set; }

        public List<string> MultipleBeneficiaries { get; set; }

        public List<Payment> SelectedPayment()
        {
            return Payments;
        }

        [Display(Name = "Role")]
        public List<Payment> Payments { get; set; }

        [Display(Name = "Beneficiary")]
        public string Beneficiary { get; set; }

        private SelectList _beneficiaries;

        private List<Payment> allpayments;

        public SelectList UniqueBeneficiaries
        {
            get
            {
                if (_beneficiaries == null)
                {
                    _beneficiaries = new SelectList(allpayments.GroupBy(p => p.Beneficiary.Name)
                                                  .Select(y => y.FirstOrDefault()),
                                          "Beneficiary.Name",
                                          "Beneficiary.Name");
                }
                return _beneficiaries;
            }
            set
            {
                _beneficiaries = value;
            }
        }

        public SelectList Sources
        {
            get
            {
                return new SelectList(new List<string>()
                {
                    "Local",
                    "International",
                    "External"
                });
            }
        }


        public string Total { get { return SelectedPayment().Select(payment => payment.Amount * payment.Currency.ExchangeRate).Sum().ToString("#,##0.00"); } }

        public string TotalTickets { get { return SelectedPayment().Select(payment => payment.Tickets).Sum().ToString(); }  }

        public PublishAllViewModel()
        {
        }

        public PublishAllViewModel(List<Payment> payments)
        {
            Payments = payments;
            allpayments = Payments;
        }

        // JUST IN CASE
        //public PublishAllViewModel(/*List<Payment> payments,*/ string beneficiary, List<Payment> paymentsAll)
        //{
        //    //payments.FirstOrDefault(a => a.Beneficiary.Name == beneficiary);
        //    //Payments = payments;
        //    var s = paymentsAll.Where(b => b.Beneficiary.Name.Contains(beneficiary));
        //    Payments = s.ToList();
        //    allpayments = paymentsAll;
        //}

        public PublishAllViewModel(string beneficiary, List<Payment> paymentsAll)
        {
            var s = paymentsAll.Where(b => b.Beneficiary.Name.Contains(beneficiary));
            Payments = s.ToList();
            allpayments = paymentsAll;
        }
    }
}