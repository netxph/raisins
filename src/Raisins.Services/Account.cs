using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using System.IO;
using System.Security.Cryptography;
using NHibernate.Criterion;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Account : ActiveRecordBase<Account>
    {

        [PrimaryKey(PrimaryKeyType.Identity)]
        public int ID { get; set; }

        [Property]
        public string UserName { get; set; }

        [Property]
        public string Password { get; set; }

        [Property]
        public string Salt { get; set; }

        [HasMany]
        public IList<Setting> Settings { get; set; }

        public static string GetSalt()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public static Account FindUser(string userName)
        {
            return FindFirst(Expression.Eq("UserName", userName));
        }

        public static string GetHash(string password, string salt)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            
            return Convert.ToBase64String(provider.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}.{1}", password, salt))));
        }
        
        public static bool Authenticate(string userName, string password)
        {
            bool result = false;

            Account account = FindOne(Expression.Eq("UserName", userName));

            if (account != null)
            {
                if (account.Password == GetHash(password, account.Salt))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
