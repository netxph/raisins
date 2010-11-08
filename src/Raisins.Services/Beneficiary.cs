using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

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

    }
}
