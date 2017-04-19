using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Beneficiaries.Models
{
    public class Beneficiary
    {
        public Beneficiary(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Beneficiary:name");
            }
            Name = name;
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Beneficiary:description");
            }
            Description = description;
        }
        public Beneficiary(int beneficiaryID, string name, string description)
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
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Beneficiary:description");
            }
            Description = description;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int BeneficiaryID { get; private set; }
    }
}
