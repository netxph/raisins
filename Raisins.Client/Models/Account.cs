using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Account
    {
        public Account(string userName, string password, Role role)
        {
            UserName = userName;
            Password = password;
            Role = role;
        }
        public Account() { }
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public int ProfileID { get; set; }
        public virtual Role Role { get; set; }
        public virtual AccountProfile Profile { get; set; }
    }
}