using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Beneficiaries.Models
{
    public class Beneficiaries : IEnumerable<Beneficiary>
    {
        private readonly List<Beneficiary> _beneficiaries;
        public Beneficiaries()
        {
            _beneficiaries = new List<Beneficiary>();
        }
        public void AddRange(IEnumerable<Beneficiary> beneficiaries)
        {
            _beneficiaries.AddRange(beneficiaries);
        }
        public void Add(Beneficiary payment)
        {
            _beneficiaries.Add(payment);
        }

        public IEnumerator<Beneficiary> GetEnumerator()
        {
            return _beneficiaries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _beneficiaries.GetEnumerator();
        }

    }
}
