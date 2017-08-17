using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.ViewModels
{
    public class BeneficiaryListViewModel
    {
        public List<Beneficiary> Beneficiaries { get; set; }
        public BeneficiaryListViewModel()
        {
        }
        public BeneficiaryListViewModel(List<Beneficiary> beneficiaries)
        {
            Beneficiaries = beneficiaries.Where(b => b.Name.ToLower() != "none").ToList();
        }
    }
    public class BeneficiaryEditViewModel
    {       
        public int BeneficiaryID { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public BeneficiaryEditViewModel(Beneficiary beneficiary)
        {
            BeneficiaryID = beneficiary.BeneficiaryID;
            Name = beneficiary.Name;
            Description = beneficiary.Description;
        }
        public BeneficiaryEditViewModel()
        {
        }
    }
    public class HomeViewModel
    {
        public List<Beneficiary> Beneficiaries { get; set; }
        public decimal Total { get; set; }
        public decimal Needed { get; set; }
        public decimal Percent { get; set; }

        public HomeViewModel()
        {
        }
        public HomeViewModel(List<Beneficiary> beneficiaries, decimal total)
        {

            Beneficiaries = beneficiaries.Where(b => b.Name.ToLower() != "none").ToList();
            Total = total;
            Needed = 2000000;
            Percent = Math.Round((total/Needed*100),2);

        }
    }
}