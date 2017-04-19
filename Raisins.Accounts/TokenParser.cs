using Raisins.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts
{
    public class TokenParser
    {
        private readonly string _token;

        public TokenParser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("TokenParser:token");
            }
            _token = token;
        }
        public Token GetToken()
        {
            string[] values = _token.Split('|');
            string user = "", role = "", created = "", expiration = "";
            IEnumerable<string> permissions = null;
            foreach (string value in values)
            {
                if (value.Contains("userName"))
                {
                    string[] temps = value.Split('=');
                    user = temps[1];
                }
                if (value.Contains("role"))
                {
                    string[] temps = value.Split('=');
                    role = temps[1];
                }
                if (value.Contains("permissions"))
                {
                    string[] temps = value.Split('=');
                    permissions = temps[1].Split(';');
                }
                if (value.Contains("created-date"))
                {
                    string[] temps = value.Split('=');
                    created = temps[1];
                }
                if (value.Contains("expiration-date"))
                {
                    string[] temps = value.Split('=');
                    expiration = temps[1];
                }
            }
            DateTime dateCreated = DateTime.Parse(created);
            DateTime dateExpired = DateTime.Parse(expiration);

            var date = (dateExpired - dateCreated).TotalDays;

            return new Token(user, role, permissions, Convert.ToInt32(date));
        }
    }
}
