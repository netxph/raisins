using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Raisins.Client.Web.Models
{
    public class VoteSummary
    {

        public string Name { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal LockedPayments { get; set; }
        public int LockedVotes { get; set; }
        public int TotalVotes { get; set; }

        public static IEnumerable<VoteSummary> Get()
        {
            var votes = new List<VoteSummary>();

            using (var db = ObjectProvider.CreateDB())
            {
                var totalQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Beneficiary)
                                 group payment by payment.Beneficiary.Name into g
                                  select new
                                  {   Name = g.Key,
                                      TotalVotes = 
                                      g.Sum (p =>
                                          (p.Currency.CurrencyCode=="PHP")?
                                          (((((int)((p.Amount)*p.Currency.ExchangeRate)) / 2000) * 55) +
                                                    ((((int)(p.Amount*p.Currency.ExchangeRate) % 2000) / 1000) * 25) +
                                                    (((((int)(p.Amount*p.Currency.ExchangeRate) % 2000) % 1000) / 500) * 12) +
                                                    ((((((int)(p.Amount * p.Currency.ExchangeRate) % 2000) % 1000) % 500) / 50) * 1)) :
                                         (int)(p.Amount / p.Currency.Ratio)
                                          )
                                 
                                  }).ToList();             
                                 //select new { Name = g.Key, TotalVotes = g.Sum(p => p.Amount / p.Currency.Ratio) }).ToList();

                var lockedQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Beneficiary)
                                  where payment.Locked
                                 group payment by payment.Beneficiary.Name into g
                                 select new
                                   {
                                       Name = g.Key,
                                       TotalVotes =
                                       g.Sum(p =>
                                           (p.Currency.CurrencyCode == "PHP") ?
                                           (((((int)((p.Amount) * p.Currency.ExchangeRate)) / 2000) * 55) +
                                                     ((((int)(p.Amount * p.Currency.ExchangeRate) % 2000) / 1000) * 25) +
                                                     (((((int)(p.Amount * p.Currency.ExchangeRate) % 2000) % 1000) / 500) * 12) +
                                                     ((((((int)(p.Amount * p.Currency.ExchangeRate) % 2000) % 1000) % 500) / 50) * 1)) :
                                          (int)(p.Amount / p.Currency.Ratio)
                                           )

                                   }).ToList();
               //select new { Name = g.Key, TotalVotes = g.Sum(p => p.Amount / p.Currency.Ratio) }).ToList();

                var totalPayQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Beneficiary)
                                 group payment by payment.Beneficiary.Name into g
                                 select new { Name = g.Key, TotalPayment = g.Sum(p => p.Amount * p.Currency.ExchangeRate ) }).ToList();

                var lockedPayQuery = (from payment in db.Payments.Include(p => p.Currency).Include(p => p.Beneficiary)
                                      where payment.Locked
                                     group payment by payment.Beneficiary.Name into g
                                     select new { Name = g.Key, TotalPayment = g.Sum(p => p.Amount * p.Currency.ExchangeRate) }).ToList();

                var teams = db.Beneficiaries.ToList();

                votes = (from team in teams
                         join total in totalQuery on team.Name equals total.Name into teamJoin
                         from subTotal in teamJoin.DefaultIfEmpty()
                         join locked in lockedQuery on team.Name equals locked.Name into lockedJoin
                         from subLocked in lockedJoin.DefaultIfEmpty()
                         join totalPay in totalPayQuery on team.Name equals totalPay.Name into totalPayJoin
                         from subTotalPay in totalPayJoin.DefaultIfEmpty()
                         join lockedPay in lockedPayQuery on team.Name equals lockedPay.Name into lockedPayJoin
                         from subLockedPay in lockedPayJoin.DefaultIfEmpty()
                         select new VoteSummary() { 
                             Name = team.Name, 
                             TotalVotes = (subTotal == null ? 0 : Convert.ToInt32(subTotal.TotalVotes)), 
                             LockedVotes = (subLocked == null ? 0 : Convert.ToInt32(subLocked.TotalVotes)) ,
                             TotalPayments = (subTotalPay == null ? 0 : subTotalPay.TotalPayment),
                             LockedPayments = (subLockedPay == null ? 0 : subLockedPay.TotalPayment)
                         }).ToList();
                        
            }

            return votes;

        }

    }
}