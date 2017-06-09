using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donor's Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Beneficiary")]
        public string Beneficiary { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }
        public IEnumerable<Beneficiary> Beneficiaries { get; set; }
        public int CreatedByID { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public bool Locked { get; set; }

        //added
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        [Display(Name = "Payment Source")]
        public string Source { get; set; }
        public IEnumerable<PaymentSource> Sources { get; set; }
        [Display(Name = "Payment Type")]
        public string Type { get; set; }
        public IEnumerable<PaymentType> Types { get; set; }
        [Display(Name = "Opt Out from Raffle")]
        public bool OptOut { get; set; }

        public void InitResources(List<Beneficiary> beneficiaries,
            List<Currency> currencies, List<PaymentSource> sources, List<PaymentType> types, DateTime paymentDate)
        {
            //List<String> list = new List<String>();
            //for (int i = 0; i < beneficiaries.Count; i++)
            //{
            //    list.Add(beneficiaries[i].Name);

            //}
            //beneficiaries.Select(c => c.Name).ToList();

            List<Beneficiary> benList = new List<Beneficiary>();
            benList.Add(beneficiaries.FirstOrDefault(b => b.Name.ToLower() == "none"));
            benList.AddRange(beneficiaries.Where(b => b.Name.ToLower() != "none"));
            Beneficiaries = benList;
            Currencies = currencies;
            Beneficiary = beneficiaries[0].Name;
            Currency = currencies[0].CurrencyCode;

            
            Sources = sources;
            Source = sources[0].Source;
            Types = types;
            Type = types[0].Type;
            PaymentDate = paymentDate;
        }

        public void InitResources(List<Beneficiary> beneficiaries,
            List<Currency> currencies, List<PaymentSource> sources, List<PaymentType> types, Payment payment)
        {
            List<Beneficiary> benList = new List<Beneficiary>();
            benList.Add(beneficiaries.FirstOrDefault(b => b.Name.ToLower() == "none"));
            benList.AddRange(beneficiaries.Where(b => b.Name.ToLower() != "none"));
            Beneficiaries = benList;
            Currencies = currencies;
            Beneficiary = payment.Beneficiary.Name;
            Currency = payment.Currency.CurrencyCode;
            Name = payment.Name;
            Amount = payment.Amount;
            PaymentID = payment.PaymentID;
            Locked = payment.Locked;

            Sources = sources;
            Source = payment.Source.Source;
            Types = types;
            Type = payment.Type.Type;
            PaymentDate = payment.PaymentDate;
            Email = payment.Email;
            CreatedDate = payment.CreatedDate;
            CreatedBy = payment.CreatedBy;
            ModifiedBy = payment.ModifiedBy;
            OptOut = payment.OptOut;
        }
    }

    public class PublishAllViewModel
    {
        public List<Payment> Payments { get; set; }
        public PublishAllViewModel()
        {
        }
        public PublishAllViewModel(List<Payment> payments)
        {
            Payments = payments;
        }
    }
    public class UploadPaymentViewModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}