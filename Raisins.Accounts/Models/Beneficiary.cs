using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class Beneficiary
    {
        public Beneficiary(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Beneficiary:name");
            }
            Name = name;
        }
        public Beneficiary() { }
        public string Name { get; private set; }
    }
}
