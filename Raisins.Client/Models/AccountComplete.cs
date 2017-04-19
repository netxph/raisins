using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class AccountComplete
    {
        public AccountComplete(Account account, AccountProfile profile)
        {
            Account = account;
            Profile = profile;
        }
        public Account Account { get; set; }
        public AccountProfile Profile { get; set; }
    }
}