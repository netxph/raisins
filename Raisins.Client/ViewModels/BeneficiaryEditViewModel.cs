using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Models;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.ViewModels
{
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
}