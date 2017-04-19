using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class AccountSeed : IDbSeeder
    {
        public void Seed(RaisinsContext context)
        {
            AddAccount(context, "marielle", "1234", "Marielle Lapidario", "Administrator");
            AddAccount(context, "natraj", "1234", "Natraj Rajput", "Accountant");
            AddAccount(context, "clarisse", "1234", "Clarisse Cheng", "Manager");
            AddAccount(context, "danica", "1234", "Danica Sevilla", "User");
            AddAccountWithProfile(context, "patricia", "1234", "patricia Honrado", "Accountant", "QaiTS");
            AddAccountWithProfile(context, "gina", "1234", "Gina Co", "Accountant", "MANILEÑOS");
            AddAccountWithProfile(context, "josiah", "1234", "Josiah Barretto", "Accountant", "The Chronicles of Naina");
            AddAccount(context, "geraldine", "1234", "Geraldine Atayan", "SuperAccountant");
            AddAccount(context, "edward", "1234", "Edward Cullen", "SuperAdmininstrator");
            AddAccount(context, "jessica", "1234", "Jessica Sanches", "SuperUser");
        }
        private void AddAccount(RaisinsContext _context, string userName, string password, string name, string title)
        {
            if (!_context.Accounts.Any(a => a.UserName == userName))
            {
                var role = _context.Roles.FirstOrDefault(r => r.Name == title);
                var salt = GetSalt();
                Account account = new Account
                {
                    UserName = userName,
                    Salt = salt,
                    Password = GetHash(password, salt),
                    RoleID = role.RoleID,
                    Profile = new AccountProfile
                    {
                        Name = name
                    }
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
        }

        private void AddAccountWithProfile(RaisinsContext _context, string userName, string password, string name, string title, string beneficiary)
        {
            if (!_context.Accounts.Any(a => a.UserName == userName))
            {
                var role = _context.Roles.FirstOrDefault(r => r.Name == title);
                var salt = GetSalt();
                var assigned = _context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary);
                List<Beneficiary> list = new List<Beneficiary>();
                list.Add(assigned);


                Account account = new Account
                {
                    UserName = userName,
                    Salt = salt,
                    Password = GetHash(password, salt),
                    RoleID = role.RoleID,
                    Profile = new AccountProfile
                    {
                        Name = name,
                        Beneficiaries = list,
                        IsLocal = true
                    }
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
        }

        private string GetHash(string password, string salt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string result = BitConverter.ToString(bytes).Replace("-", string.Empty);
            return result.ToLower();
        }
        private string GetSalt()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}
