using Raisins.Client.Models;
using System;
using System.Collections.Generic;
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
}