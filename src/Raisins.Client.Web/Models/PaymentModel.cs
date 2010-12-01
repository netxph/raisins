using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Raisins.Client.Web.Validators;

namespace Raisins.Client.Web.Models
{
    public class PaymentModel : IValidatableObject
    {

        public long ID { get; set; }

        [Required]
        [RegularExpression(@"^(-)?\d+(\.\d\d)?$", ErrorMessage="Please enter a valid amount.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", NullDisplayText = "")]
        [HasDecimalValue]
        public decimal Amount { get; set; }

        public string Beneficiary { get; set; }

        public string Currency { get; set; }

        public string Email { get; set; }
        
        public string Location { get; set; }

        public string Remarks { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Name { get; set; }
        
        public PaymentClass Class { get; set; }

        public bool Locked { get; set; }

        public bool TryValidate(out IEnumerable<ValidationResult> validationResults)
        {
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, new ValidationContext(this, null, null), results, true);

            validationResults = results;

            return isValid;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Class == PaymentClass.Internal && !string.IsNullOrEmpty(Email))
            {
                if (!Email.ToLower().EndsWith("@navitaire.com"))
                {
                    yield return new ValidationResult("Please use voter's Navitaire email");
                }
            }
        }
    }
}