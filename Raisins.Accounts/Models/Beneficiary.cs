using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class Beneficiary
    {
        public Beneficiary(string name, int id, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Beneficiary:name");
            }
            Name = name;

            Id = id;

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Beneficiary:description");
            }
            Description = description;
        }

        public Beneficiary() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
