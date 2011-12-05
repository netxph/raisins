using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Raisins.Services.Security;

namespace Raisins.Services.Models
{
    public class Account
    {

        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleType { get; set; }
        public Setting Setting { get; set; }

        public static Account Login(string userName, string password)
        {
            var account = Account.FindUser(userName);

            if (account != null)
            {
                string encrypted = GetHash(password, account.Salt);

                if (encrypted == account.Password)
                {
                    return account;
                }
            }

            return null;
        }

        public static string GetHash(string password, string salt)
        {
            var crypto = new MD5CryptoServiceProvider();

            string saltedPassword = password + salt;

            var data = crypto.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string GetSalt()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public static Account FindUser(string userName)
        {
            var db = new RaisinsDB();

            return db.Accounts.Include("Setting").FirstOrDefault((account) => account.UserName == userName);
        }

        public static Account CurrentUser 
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as UserIdentity;

                if (identity != null)
                {
                    return identity.Account;
                }

                return null;
            }
        }

        public static bool ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {

            if (Login(Account.CurrentUser.UserName, oldPassword) != null && newPassword == confirmPassword)
            {
                RaisinsDB db = new RaisinsDB();
                var account = db.Accounts.FirstOrDefault(a => a.UserName == Account.CurrentUser.UserName);

                account.Salt = GetSalt();
                account.Password = GetHash(newPassword, account.Salt);

                db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}