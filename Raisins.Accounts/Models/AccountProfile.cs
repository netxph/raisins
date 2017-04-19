using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class AccountProfile
    {
        public AccountProfile(string name, IEnumerable<Beneficiary> beneficiaries)
        {
            Name = name;
            Beneficiaries = beneficiaries;
        }

        public AccountProfile(string name, IEnumerable<Beneficiary> beneficiaries, bool isLocal)
        {
            Name = name;
            Beneficiaries = beneficiaries;
            IsLocal = isLocal;
        }
        public AccountProfile() { }
        public string Name { get; private set; }
        public virtual IEnumerable<Beneficiary> Beneficiaries { get; private set; }
        public bool IsLocal { get; private set; }
    }
}
