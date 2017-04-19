using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Beneficiary
    {
        public Beneficiary()
        {
        }
        public Beneficiary(int beneficiaryID, string name)
        {
            Name = name;
            BeneficiaryID = beneficiaryID;
        }
        public Beneficiary(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Beneficiary(int beneficiaryID, string name, string description)
        {
            BeneficiaryID = beneficiaryID;
            Name = name;
            Description = description;
        }

        [Display(Name = "Beneficiary")]
        public int BeneficiaryID { get; set; }
        [Display(Name = "Group Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}