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
        public long ID { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public float Amount { get; set; }

        [Property]
        public string Location { get; set; }

        [Property]
        public string Currency { get; set; }

        [Property]
        public string Email { get; set; }

        [Property]
        public long BeneficiaryID { get; set; }

        [BelongsTo("BeneficiaryID")]
        public Beneficiary Beneficiary { get; set; }

        [HasMany]
        public IList<Ticket> Tickets { get; set; }

    }
}
