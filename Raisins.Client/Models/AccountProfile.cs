using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class AccountProfile
    {
        public AccountProfile(string name, List<Beneficiary> beneficiaries)
        {
            Name = name;
            Beneficiaries = beneficiaries;
        }
        public AccountProfile() { }
        public string Name { get; set; }
        public virtual List<Beneficiary> Beneficiaries { get; set; }
    }
}