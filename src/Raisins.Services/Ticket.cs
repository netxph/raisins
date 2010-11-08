using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Ticket : ActiveRecordBase<Ticket>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public long ID { get; set; }

        [Property]
        public string TicketCode { get; set; }

        [Property]
        public string Name { get; set; }

        [BelongsTo("AccountID")]
        public Account Account { get; set; }
    }
}
