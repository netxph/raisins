using D = Raisins.Beneficiaries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Beneficiaries.Interfaces
{
    public interface IBeneficiaryService
    {
        D.Beneficiaries GetAll();
        D.Beneficiary Get(int beneficiaryID);
        D.Beneficiary Get(string name);
        void Add(D.Beneficiary beneficiary);
        void Edit(D.Beneficiary beneficiary);
    }
}
