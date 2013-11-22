using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Account
    {
        
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int AccountProfileID { get; set; }

        public List<Role> Roles { get; set; }
        public virtual AccountProfile Profile { get; set; }
        
        public static bool Login(string userName, string password)
        {
            using (var db = ObjectProvider.CreateDB())
            {

                var account = db.Accounts.FirstOrDefault(a => a.UserName == userName);

                if (account != null)
                {
                    return account.Password == GetHash(password, account.Salt);
                }

                return false;
            }
        }

        public static bool Exists(string userName)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Accounts.FirstOrDefault(a => a.UserName == userName) != null;
            }
        }

        public static Account CreateUser(string userName, string password, List<Role> roles, AccountProfile profile)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var salt = Helper.CreateSalt();

                Account account = new Account() { UserName = userName, Salt = salt, Password = GetHash(password, salt), Roles = roles, Profile = profile };

                roles.SetState(db, EntityState.Modified);
                profile.Beneficiaries.SetState(db, EntityState.Modified);
                profile.Currencies.SetState(db, EntityState.Modified);
                
                db.Accounts.Add(account);
                db.SaveChanges();

                return db.Accounts.FirstOrDefault(a => a.UserName == userName);
            }
        }

        public static Account GetCurrentUser()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var http = ObjectProvider.CreateHttpHelper();
                var userName = http.GetCurrentUserName();

                return db.Accounts.Include("Roles").Include("Profile").Include("Profile.Beneficiaries").Include("Profile.Currencies").FirstOrDefault(a => a.UserName == userName);
            }
        }

        protected static string GetHash(string password, string salt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            return Encoding.UTF8.GetString(hashedBytes);
        }


        public static Account CreateUser(string userName, string password)
        {
            return CreateUser(userName, password, new List<Role> { Role.Find("User") }, new AccountProfile());
        }


        public static void ChangePassword(string newPassword)
        {
            ChangePassword(Account.GetCurrentUser().ID, newPassword);
        }

        public static void ChangePassword(int userId, string newPassword)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var account = db.Accounts.First(a => a.ID == userId);
                var salt = Helper.CreateSalt();
                var password = GetHash(newPassword, salt);

                account.Salt = salt;
                account.Password = password;

                db.Entry(account).State = EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}