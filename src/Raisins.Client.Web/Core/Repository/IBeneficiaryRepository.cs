using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IBeneficiaryRepository
    {
        IEnumerable<Beneficiary> GetAll();
        Beneficiary Find(int id = 0);
        void Add(Beneficiary beneficiary);
        void Edit(Beneficiary beneficiary);
        void Delete(int id);
    }
}