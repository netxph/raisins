using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Account : ActiveRecordBase<Account>
    {

        [PrimaryKey(PrimaryKeyType.Identity)]
        public int ID { get; set; }

        [Property]
        public string UserName { get; set; }

        [Property]
        public string Password { get; set; }

        [OneToOne]
        public Setting Setting { get; set; }

    }
}
