using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Web.Models
{
    public class ChangePasswordModel
    {

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[Compare("NewPassword", ErrorMessage = "Password don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}