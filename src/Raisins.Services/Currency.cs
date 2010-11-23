using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Currency : ActiveRecordBase<Currency>
    {

        [PrimaryKey(PrimaryKeyType.Identity)]
        public int ID { get; set; }

        [Property]
        public string CurrencyCode { get; set; }

        [Property]
        public decimal Ratio { get; set; }

    }
}
