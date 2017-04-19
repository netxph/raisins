using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class Token
    {
        public Token(string user, string role, IEnumerable<string> permissions, int daysExpire)
        {
            //validation here
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException("Token:user");
            }
            User = user;
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("Token:role");
            }
            Role = role;
            if (permissions == null)
            {
                throw new ArgumentNullException("Token:permissions");
            }
            Permissions = permissions;
            if (daysExpire == 0)
            {
                throw new ArgumentNullException("Token:daysExpire");
            }
            DaysExpire = daysExpire;
        }

        public string Role { get; private set; }
        public int DaysExpire { get; private set; }
        public IEnumerable<string> Permissions { get; private set; }
        public string User { get; private set; }
        
        public string GenerateString(IDateProvider provider)
        {
            var created = provider.GetUtcNow();
            var expired = created.AddDays(DaysExpire);
            return string.Format("userName={0}|role={1}|permissions={2}|created-date={3}|expiration-date={4}", User, Role, string.Join(";", Permissions.ToArray()), created, expired);
        }

    }
}
