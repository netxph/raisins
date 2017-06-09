using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts.Models
{
    public class Accounts : IEnumerable<Account>
    {
        private readonly List<Account> _accounts;
        public Accounts()
        {
            _accounts = new List<Account>();
        }
        public Accounts(IEnumerable<Account> accounts)
            : this()
        {
            AddRange(accounts);
        }
        public void AddRange(IEnumerable<Account> accounts)
        {
            _accounts.AddRange(accounts);
        }
        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public IEnumerator<Account> GetEnumerator()
        {
            return _accounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _accounts.GetEnumerator();
        }

    }
}
