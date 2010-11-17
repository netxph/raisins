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
        public IList<Payment> Payments { get; set; }

        [HasMany]
        public IList<Setting> Settings { get; set; }

        public decimal GetTotalAmount()
        {
            ScalarQuery<decimal> query = new ScalarQuery<decimal>(typeof(Payment), "select sum(payment.Amount) from Payment payment where payment.Beneficiary = ?", this);

            return query.Execute();
        }

        public long GetTotalVotes()
        {
            ScalarQuery<long> query = new ScalarQuery<long>(typeof(Ticket), "select count(ticket) from Ticket ticket where ticket.Payment.Beneficiary = ?", this);

            return query.Execute();
        }

        public static Beneficiary FindSetting(string userName)
        {
            var beneficiaries = Beneficiary.FindAll();

            foreach (var beneficiary in beneficiaries)
            {
                foreach (var setting in beneficiary.Settings)
                {
                    if (setting.UserName == userName)
                    {
                        return beneficiary;
                    }
                }
            }

            return null;
        }

    }
}
