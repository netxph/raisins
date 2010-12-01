using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Validators
{
    public class HasDecimalValueAttribute : ValidationAttribute
    {

        public HasDecimalValueAttribute()
        {
            ErrorMessage = "Value must be > 0.";
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;

            try
            {
                decimal validateObject = Convert.ToDecimal(value);

                if (validateObject > 0)
                {
                    isValid = true;
                }
            }
            catch
            { 
            }

            return isValid;
        }

    }
}