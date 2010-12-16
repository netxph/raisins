using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class WinnerLog : ActiveRecordBase<WinnerLog>
    {

        [PrimaryKey(PrimaryKeyType.Foreign)]
        public long ID { get; set; }

        [OneToOne]
        public Ticket Ticket { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public DateTime CreatedDate { get; set; }

    }
}
