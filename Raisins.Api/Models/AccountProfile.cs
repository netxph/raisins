using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Api.Models
{
    public class AccountProfile
    {
        [Key]
        public int ProfileID { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }

        public bool IsLocal { get; set; }
    }
}