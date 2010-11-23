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

        [BelongsTo("CurrencyID")]
        public Currency Currency { get; set; }

        [Property]
        public string Location { get; set; }

        [Property]
        public PaymentClass Class { get; set; }

        [BelongsTo("BeneficiaryID")]
        public Beneficiary Beneficiary { get; set; }

        [BelongsTo("AccountID")]
        public Account Account { get; set; }
        
    }
}
