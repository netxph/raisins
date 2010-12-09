using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class MailLog : ActiveRecordBase<MailLog>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public long ID { get; set; }

        [BelongsTo("PaymentID")]
        public Payment Payment { get; set; }

        [Property]
        public DateTime LastSent { get; set; }

        [Property]
        public bool IsSuccessful { get; set; }

    }
}
