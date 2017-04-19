using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class Account
    {
        public Account(string userName, string passwordHash, string salt, Role role)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Account:userName");
            }

            UserName = userName;

            if (role == null)
            {
                throw new ArgumentNullException("Account:role");
            }

            Role = role;

            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentNullException("Account:salt");
            }

            Salt = salt;

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentNullException("Account:passwordHash");
            }

            Password = passwordHash;
        }

        public Account(string userName, string passwordHash, string salt, Role role, AccountProfile profile)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Account:userName");
            }

            UserName = userName;

            if (role == null)
            {
                throw new ArgumentNullException("Account:role");
            }

            Role = role;
            if (profile == null)
            {
                throw new ArgumentNullException("Account:profile");
            }

            Profile = profile;

            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentNullException("Account:salt");
            }

            Salt = salt;

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentNullException("Account:passwordHash");
            }

            Password = passwordHash;
        }

        public Account(string userName, Role role, AccountProfile profile)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Account:userName");
            }

            UserName = userName;

            if (role == null)
            {
                throw new ArgumentNullException("Account:role");
            }

            Role = role;
            if (profile == null)
            {
                throw new ArgumentNullException("Account:profile");
            }

            Profile = profile;
        }
        public Account() { }
        public Role Role { get; private set; }
        public AccountProfile Profile { get; private set; }
        public string Salt { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
       

        public bool ValidatePassword(ICryptProvider cryptProvider, string password)
        {
            var compareHash = cryptProvider.Hash(password, Salt);

            return Password == compareHash;
        }
        public Token CreateToken()
        {
            return new Token(UserName, Role.Name ,Role.Permissions, 1);
        }
        public void AddSalt()
        {
            Salt = GetSalt();
        }
        private string GetSalt()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}
