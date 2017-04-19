using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Account
    {
        public Account(string userName, string password, string salt, int roleID, AccountProfile profile)
        {
            UserName = userName;
            Password = password;
            Salt = salt;
            RoleID = roleID;
            Profile = profile;
        }
        public Account(int accountID, string userName, string password, string salt, int roleID, AccountProfile profile)
        {
            AccountID = accountID;
            UserName = userName;
            Password = password;
            Salt = salt;
            RoleID = roleID;
            Profile = profile;
        }
        public Account(int accountID, string userName, string password, string salt, int roleID, Role role, int profileID, AccountProfile profile)
        {
            AccountID = accountID;
            UserName = userName;
            Password = password;
            Salt = salt;
            RoleID = roleID;
            Role = role;
            ProfileID = profileID;
            Profile = profile;   
        }

        public Account(int accountID, string userName, string password, string salt, int roleID,int profileID)
        {
            AccountID = accountID;
            UserName = userName;
            Password = password;
            Salt = salt;
            RoleID = roleID;
            ProfileID = profileID;
        }

        public Account()
        {
        }
        [Key]
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        public int ProfileID { get; set; }
        public virtual Role Role { get; set; }
        public virtual AccountProfile Profile { get; set; }
    }
}
