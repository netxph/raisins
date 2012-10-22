using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class AccountService
    {

        public AccountService()
            : this(new RaisinsDB())
        {
            
        }

        public AccountService(RaisinsDB db)
        {
            DB = db;
        }

        protected RaisinsDB DB { get; set; }

        public bool Login(string userName, string password)
        {
            var account = DB.Accounts.FirstOrDefault(a => a.UserName == userName);
            
            if (account != null)
            {
                return account.Password == GetHash(password, account.Salt);
            }

            return false;
        }


        public Account CreateUser(string userName, string password)
        {
            var salt = Helper.CreateSalt();

            Account account = new Account() { UserName = userName, Salt = salt, Password = GetHash(password, salt) };

            DB.Accounts.Add(account);
            DB.SaveChanges();

            return DB.Accounts.FirstOrDefault(a => a.UserName == userName);
        }

        protected virtual string GetHash(string password, string salt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            return Encoding.UTF8.GetString(hashedBytes);
        }
    }
}