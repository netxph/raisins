using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Models
{
    public class Account
    {
        public Account(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Account:name");
            }
            UserName = userName;
        }
        public Account() { }
        public string UserName { get; private set; }
    }
}
