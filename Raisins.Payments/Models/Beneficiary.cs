using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
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
            Description = description;
        }
        public Beneficiary() { }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
