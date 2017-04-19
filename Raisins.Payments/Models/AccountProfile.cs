using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class AccountProfile
    {
        public AccountProfile(string name, IEnumerable<Beneficiary> beneficiaries)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("AccountProfile:name");
            }
            Name = name;
            Beneficiaries = beneficiaries;
        }

        public string Name { get; set; }
        public virtual IEnumerable<Beneficiary> Beneficiaries { get; set; }
    }
}
