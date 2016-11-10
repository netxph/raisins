using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

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

        public bool IsValidAccount(string password)
        {
            return Password == GetHash(password, Salt);
        }

        public void GenerateNewPassword(string newPassword, string newSalt)
        {
            Password = GetHash(newPassword, newSalt);
        }

        
        public void SetSalt(string salt)
        {
            Salt = salt;
        }

        public string GetHash(string password, string salt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            return Encoding.UTF8.GetString(hashedBytes);
        }


    }
}