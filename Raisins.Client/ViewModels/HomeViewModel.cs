using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Models;

namespace Raisins.Client.ViewModels
{
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
            Needed = 1000000;
            Percent = Math.Round((total / Needed * 100), 2);
        }
    }
}