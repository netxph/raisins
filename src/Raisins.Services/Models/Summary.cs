using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Services.Models
{
    public class Summary
    {

        public Beneficiary Beneficiary { get; set; }
        public decimal TotalCashInLocal { get; set; }
        public decimal TotalCashInGlobal { get; set; }
        public decimal TotalRemittedInLocal { get; set; }
        public decimal TotalRemittedInGlobal { get; set; }
        public int TotalVotes { get; set; }

        public static List<Summary> GetSummaryReport()
        {
            var beneficiaries = Beneficiary.GetAllForReport();
            var summaries = new List<Summary>();

            var globalCurrency = Currency.GetGlobalCurrency();

            foreach (var beneficiary in beneficiaries)
            {
                var summary = new Summary();

                summary.Beneficiary = beneficiary;

                summary.TotalCashInLocal = beneficiary.Payments
                    .Sum(p => p.Amount * p.Currency.ExchangeRate);

                summary.TotalCashInGlobal = beneficiary.Payments
                    .Sum(p => (p.Amount * p.Currency.ExchangeRate) / globalCurrency.ExchangeRate);

                summary.TotalRemittedInLocal = beneficiary.Payments
                    .Where(p => p.Locked)
                    .Sum(p => p.Amount * p.Currency.ExchangeRate);

                summary.TotalRemittedInGlobal = beneficiary.Payments
                    .Where(p => p.Locked)
                    .Sum(p => (p.Amount * p.Currency.ExchangeRate) / globalCurrency.ExchangeRate);

                summary.TotalVotes = (from payment in beneficiary.Payments
                                     select new { TicketCount = payment.Tickets.Count }).Sum(p => p.TicketCount);

                summaries.Add(summary);
            }

            return summaries.OrderByDescending(s => s.TotalVotes).ToList();
        }

    }
}