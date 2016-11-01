using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class AccountProfile
    {

        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<Beneficiary> Beneficiaries { get; set; }
        public List<Currency> Currencies { get; set; }

        public bool IsLocal { get; set; }

    }
}