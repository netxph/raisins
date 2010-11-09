using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Beneficiary : ActiveRecordBase<Beneficiary>
    {

        [PrimaryKey(PrimaryKeyType.Identity)]
        public long ID { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public string Description { get; set; }

        [HasMany]
        public IList<Account> Accounts { get; set; }

        public decimal GetTotalAmount()
        {
            ScalarQuery<decimal> query = new ScalarQuery<decimal>(typeof(Account), "select sum(account.Amount) from Account account where account.Beneficiary = ?", this);

            return query.Execute();
        }

        public long GetTotalVotes()
        {
            ScalarQuery<long> query = new ScalarQuery<long>(typeof(Ticket), "select count(ticket) from Ticket ticket where ticket.Account.Beneficiary = ?", this);

            return query.Execute();
        }

    }
}
