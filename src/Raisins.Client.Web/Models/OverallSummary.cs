using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class OverallSummary
    {

        public int TotalVotes { get; set; }
        public int LockedVotes { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal LockedPayments { get; set; }

        public static OverallSummary Get() 
        {
            var summary = VoteSummary.Get();

            var overall = new OverallSummary() 
            { 
                LockedVotes = summary.Sum(v => v.LockedVotes), 
                TotalVotes = summary.Sum(v => v.TotalVotes), 
                LockedPayments = summary.Sum(v => v.LockedPayments), 
                TotalPayments = summary.Sum(v => v.TotalPayments) 
            };

            return overall;
        }

    }
}