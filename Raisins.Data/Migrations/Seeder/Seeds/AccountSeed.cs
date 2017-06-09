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
        private RaisinsContext _context;

        public void Seed(RaisinsContext context)
        {
            _context = context;

            AddAccount("marielle", "1234", "Marielle Lapidario", "Administrator");
            AddAccount("Super", "1234", "Super User", "Super");
            AddAccount("natraj", "1234", "Natraj Rajput", "Accountant");
            AddAccount("john", "1234", "Clarisse Cheng", "Manager");
            AddAccount("danica", "1234", "Danica Sevilla", "User");
            AddAccount("geraldine", "1234", "Geraldine Atayan", "SuperAccountant");
            AddAccount("jm", "1234", "Edward Cullen", "SuperAdmininstrator");
            AddAccount("jessica", "1234", "Jessica Sanches", "SuperUser");

            AddAccountWithProfile(context, "super", "1234", "super user", "Super", "QaiTS");
            AddAccountWithProfile(context, "patricia", "1234", "patricia Honrado", "Accountant", "QaiTS");
            AddAccountWithProfile(context, "gina", "1234", "Gina Co", "Accountant", "MANILEÑOS");
            AddAccountWithProfile(context, "josiah", "1234", "Josiah Barretto", "Accountant", "The Chronicles of Naina");
        }

        private void AddAccount(string userName, string password, string name, string title)
        {
            if (!_context.Accounts.Any(a => a.UserName == userName))
            {
                var role = _context.Roles.Where(r => r.Name == title).FirstOrDefault();

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
                //var role = _context.Roles.FirstOrDefault(r => r.Name == title);
                var role = _context.Roles.Where(x => x.Name == title).FirstOrDefault();
                var salt = GetSalt();
                var assigned = _context.Beneficiaries.FirstOrDefault(b => b.Name == beneficiary);
                List<Beneficiary> list = new List<Beneficiary>();
                list.Add(assigned);

                //_context.Roles.Where(x => x.Name == title).FirstOrDefault();
                //list.Where(x => x.Name == title).FirstOrDefault();

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
