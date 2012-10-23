using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        
        public static bool Login(string userName, string password)
        {
            using (var db = DbFactory.Create())
            {

                var account = db.Accounts.FirstOrDefault(a => a.UserName == userName);

                if (account != null)
                {
                    return account.Password == GetHash(password, account.Salt);
                }

                return false;
            }
        }


        public static Account CreateUser(string userName, string password)
        {
            using (var db = DbFactory.Create())
            {
                var salt = Helper.CreateSalt();

                Account account = new Account() { UserName = userName, Salt = salt, Password = GetHash(password, salt) };

                db.Accounts.Add(account);
                db.SaveChanges();

                return db.Accounts.FirstOrDefault(a => a.UserName == userName);
            }
        }

        protected static string GetHash(string password, string salt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            return Encoding.UTF8.GetString(hashedBytes);
        }

    }
}