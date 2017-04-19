using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Models
{
    public class Beneficiary
    {
        public Beneficiary(int beneficiaryID, string name)
        {
            if (beneficiaryID < 0)
            {
                throw new ArgumentNullException("Beneficiary:beneficiaryID");
            }
            BeneficiaryID = beneficiaryID;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Beneficiary:name");
            }
            Name = name;
        }
        [Key]
        public int BeneficiaryID { get; private set; }
        public string Name { get; private set; }
    }
}
