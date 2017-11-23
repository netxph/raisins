using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DATA = Raisins.Data.Models;
using ACCOUNTS = Raisins.Accounts.Models;
using System.Data.Entity;
using Raisins.Accounts.Interfaces;
using System.Security.Cryptography;

namespace Raisins.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private RaisinsContext _context;
        public AccountRepository() : this(RaisinsContext.Instance)
        {
        }

        public AccountRepository(RaisinsContext context)
        {
            _context = context;
        }

        public ACCOUNTS.Account Get(string userName)
        {
            return Convert(_context.Accounts
                    .FirstOrDefault(a => a.UserName == userName));
        }

        public ACCOUNTS.Accounts GetAll()
        {
            return ConvertToDomainList(_context.Accounts
                                        .Include(a => a.Profile)
                                        .Include(a => a.Profile.Beneficiaries)
                                        .Include(a => a.Role)
                                        );
        }

        public bool Exists(string userName)
        {
            if (Get(userName) == null) return false;
            else return true;
        }

        public void Edit(ACCOUNTS.Account account, ACCOUNTS.AccountProfile profile)
        {
            DATA.Account efAccount = ConvertToEFwithID(account, profile);

            //START
            var tempAccount = _context.Accounts.Single(a => a.AccountID == efAccount.AccountID);
            tempAccount.RoleID = efAccount.RoleID;

            var tempProfile = _context.Profiles.Single(p => p.ProfileID == efAccount.ProfileID);
            //remove beneficary
            foreach (var ben in tempProfile.Beneficiaries.ToArray())
            {
                if (!efAccount.Profile.Beneficiaries.Any(b => b.BeneficiaryID == ben.BeneficiaryID))
                {
                    tempProfile.Beneficiaries.Remove(ben);
                }
            }
            //add beneficary
            foreach (var ben in efAccount.Profile.Beneficiaries)
            {
                if (!tempProfile.Beneficiaries.Any(b => b.BeneficiaryID == ben.BeneficiaryID))
                {
                    tempProfile.Beneficiaries.Add(ben);
                }
            }           

            _context.SaveChanges();           

        }

        public bool Any(string userName)
        {
            return _context.Accounts.Any(a => a.UserName == userName);
        }

        public void Add(ACCOUNTS.Account account, ACCOUNTS.AccountProfile profile)
        {
            if (account.UserName != null || profile.Name != null)
            {
                account.AddSalt();

                _context.Accounts.Add(ConvertToEF(account, profile));
                _context.SaveChanges();
            }
        }

        protected DATA.Account ConvertToEF(ACCOUNTS.Account account, ACCOUNTS.AccountProfile profile)
        {
            int roleID = _context.Roles.DefaultIfEmpty().FirstOrDefault(r => r.Name == account.Role.Name).RoleID;

            return new DATA.Account(
                            account.UserName,
                            GetHash(account.Password,
                                    account.Salt),
                            account.Salt,
                            roleID,
                            BuildAccountProfile(profile));
        }

        protected virtual DATA.AccountProfile BuildAccountProfile(ACCOUNTS.AccountProfile profile)
        {
            List<DATA.Beneficiary> efBeneficiaries = new List<DATA.Beneficiary>();

            foreach (var beneficiary in profile.Beneficiaries)
            {
                efBeneficiaries.Add(_context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary.Name));
            }

            return new DATA.AccountProfile(profile.Name, efBeneficiaries);
        }

        private DATA.Account ConvertToEFwithID(ACCOUNTS.Account account, ACCOUNTS.AccountProfile profile)
        {
            DATA.Role role = _context.Roles.FirstOrDefault(r => r.Name == account.Role.Name);
            List<DATA.Beneficiary> efBeneficiaries = new List<DATA.Beneficiary>();
            foreach (var beneficiary in profile.Beneficiaries)
            {
                efBeneficiaries.Add(_context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary.Name));
            }
            DATA.Account current = _context.Accounts.FirstOrDefault(a => a.UserName == account.UserName);
            int profileID = _context.Profiles.FirstOrDefault(a => a.Name == profile.Name).ProfileID;

            DATA.AccountProfile efProfile = new DATA.AccountProfile(profileID, profile.Name, efBeneficiaries, profile.IsLocal);
            return new DATA.Account(current.AccountID, account.UserName, current.Password, current.Salt, role.RoleID, role, efProfile.ProfileID, efProfile);
        }

        private DATA.AccountProfile ConvertProfileToEF(ACCOUNTS.AccountProfile profile)
        {
            List<DATA.Beneficiary> efBeneficiaries = new List<DATA.Beneficiary>();
            foreach (var beneficiary in profile.Beneficiaries)
            {
                efBeneficiaries.Add(_context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary.Name));
            }
            int profileID = _context.Profiles.FirstOrDefault(a => a.Name == profile.Name).ProfileID;

            return new DATA.AccountProfile(profileID, profile.Name, efBeneficiaries, profile.IsLocal);
        }

        private ACCOUNTS.Account Convert(DATA.Account efAccount)
        {
            var beneficiaries = Convert(efAccount.Profile.Beneficiaries);

            var role = Convert(efAccount.Role);
            var profile = Convert(efAccount, beneficiaries);

            return new ACCOUNTS.Account(efAccount.UserName, efAccount.Password, efAccount.Salt, role, profile);
        }

        private IEnumerable<ACCOUNTS.Beneficiary> Convert(ICollection<DATA.Beneficiary> beneficiaries)
        {
            return beneficiaries.DefaultIfEmpty().Select(b => Convert(b));
        }

        private static ACCOUNTS.AccountProfile Convert(DATA.Account efAccount, IEnumerable<ACCOUNTS.Beneficiary> beneficiaries)
        {
            if (efAccount == null)
            {
                return null;
            }

            return new ACCOUNTS.AccountProfile(efAccount.Profile.Name, beneficiaries);
        }

        private static ACCOUNTS.Role Convert(DATA.Role role)
        {
            if (role == null)
            {
                return null;
            }

            return new ACCOUNTS.Role(role.Name, role.Permissions);
        }

        private static ACCOUNTS.Beneficiary Convert(DATA.Beneficiary beneficiary)
        {
            if (beneficiary == null)
            {
                return null;
            }

            return new ACCOUNTS.Beneficiary(beneficiary.Name, beneficiary.BeneficiaryID, beneficiary.Description);
        }

        private ACCOUNTS.Accounts ConvertToDomainList(IEnumerable<DATA.Account> efAccounts)
        {
            ACCOUNTS.Accounts accounts = new ACCOUNTS.Accounts();

            foreach (var efAccount in efAccounts)
            {
                accounts.Add(Convert(efAccount));
            }

            return accounts;
        }

        private string GetHash(string password, string salt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string result = BitConverter.ToString(bytes).Replace("-", string.Empty);
            return result.ToLower();
        }

        public void AddBeneficiary(ACCOUNTS.Account superAccount, ACCOUNTS.Beneficiary beneficiary)
        {
            var superAccountBeneficiary = superAccount.Profile.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary.Name);

            if (superAccountBeneficiary == null)
            {
                AddBeneficiaryToSuper(superAccount, beneficiary);
            }
        }

        protected virtual void AddBeneficiaryToSuper(ACCOUNTS.Account superAccount, ACCOUNTS.Beneficiary beneficiary)
        {
            var acc = _context.Accounts.FirstOrDefault(a => a.UserName == superAccount.UserName);

            //_context.Accounts.Remove(acc);
            //_context.SaveChanges();

            var bene = _context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary.Name);

            //_context.Accounts.Add(acc);
            //_context.SaveChanges();

            acc.Profile.Beneficiaries.Add(bene);
            _context.SaveChanges();
        }
    }
}