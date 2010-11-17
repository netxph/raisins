using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class PaymentModel
    {
        [Required]
        [RegularExpression(@"^(-)?\d+(\.\d\d)?$", ErrorMessage="Please enter a valid amount.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", NullDisplayText = "")]
        public decimal Amount { get; set; }

        public long BeneficiaryID { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public long ID { get; set; }
        public string Location { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Name { get; set; }

    }
}