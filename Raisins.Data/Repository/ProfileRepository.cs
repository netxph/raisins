using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Payments.Models;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private RaisinsContext _context;
        public ProfileRepository() : this(new RaisinsContext())
        {
        }

        public ProfileRepository(RaisinsContext context)
        {
            _context = context;
        }
        public IEnumerable<AccountProfile> GetAll()
        {
            List<EF.AccountProfile> efProfiles = _context.Profiles.ToList();
            List<AccountProfile> profile = new List<AccountProfile>();
            foreach (var efprofile in efProfiles)
            {
                profile.Add(ConverToDomain(efprofile));
            }

            return profile;
        }


        public AccountProfile GetProfile(string userName)
        {
            return ConverToDomain(_context.Accounts.FirstOrDefault(a => a.UserName == userName).Profile);
        }

        private AccountProfile ConverToDomain(EF.AccountProfile efProfile)
        {
            return new AccountProfile(efProfile.Name, ConvertBeneficiaryList(efProfile.Beneficiaries.ToList()));
        }

        private Beneficiary ConvertBeneficiaryToDomain(EF.Beneficiary efBeneficiary)
        {
            return new Beneficiary(efBeneficiary.Name, efBeneficiary.Description);
        }

        private IEnumerable<Beneficiary> ConvertBeneficiaryList(IEnumerable<EF.Beneficiary> efBeneficiaries)
        {
            List<Beneficiary> beneficiaries = new List<Beneficiary>();
            foreach (var efBeneficiary in efBeneficiaries)
            {
                beneficiaries.Add(ConvertBeneficiaryToDomain(efBeneficiary));
            }
            return beneficiaries;
        }
    }
}
