using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Interfaces
{
    public interface IBeneficiaryForPaymentRepository
    {
        IEnumerable<Beneficiary> GetAll();
        Beneficiary Find(int id = 0);
        Beneficiary GetBeneficiary(string Name);
        void Add(Beneficiary beneficiary);
        void Edit(Beneficiary beneficiary);
        void Delete(Beneficiary beneficiary);
    }
}
