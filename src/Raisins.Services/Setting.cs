using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Setting : ActiveRecordBase<Payment>
    {
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int ID { get; set; }

        [Property]
        public string UserName { get; set; }

        [Property]
        public string Currency { get; set; }

        [Property]
        public string Location { get; set; }

        [BelongsTo("BeneficiaryID")]
        public Beneficiary Beneficiary { get; set; }

    }
}
