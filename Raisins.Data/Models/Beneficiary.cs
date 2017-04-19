using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Beneficiary
    {
        public Beneficiary()
        {
        }
        public Beneficiary(string name)
        {
            Name = name;
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
        [Key]
        public int BeneficiaryID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
