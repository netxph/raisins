using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Raisins.Client.Web.Models
{
    public class ExecutiveSummary
    {

        public string Name { get; set; }
        public int TotalVotes { get; set; }
        public int LockedVotes { get; set; }

        public static IEnumerable<ExecutiveSummary> Get()
        {
            var votes = new List<ExecutiveSummary>();

            using (var db = ObjectProvider.CreateDB())
            {
                var totalQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Executive)
                                  group payment by payment.Executive.Name into g
                                  select new { Name = g.Key, TotalVotes = g.Sum(p => p.Amount / p.Currency.Ratio) }).ToList();

                var lockedQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Executive)
                                   where payment.Locked
                                   group payment by payment.Executive.Name into g
                                   select new { Name = g.Key, TotalVotes = g.Sum(p => p.Amount / p.Currency.Ratio) }).ToList();

                var executives = db.Executives.ToList();

                votes = (from executive in executives
                         join total in totalQuery on executive.Name equals total.Name into teamJoin
                         from subTotal in teamJoin.DefaultIfEmpty()
                         join locked in lockedQuery on executive.Name equals locked.Name into lockedJoin
                         from subLocked in lockedJoin.DefaultIfEmpty()
                         select new ExecutiveSummary() { Name = executive.Name, TotalVotes = (subTotal == null ? 0 : Convert.ToInt32(subTotal.TotalVotes)), LockedVotes = (subLocked == null ? 0 : Convert.ToInt32(subLocked.TotalVotes)) }).ToList();

            }

            return votes;
        }

    }
}