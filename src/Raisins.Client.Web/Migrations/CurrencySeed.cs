using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Migrations
{
    public class CurrencySeed : IDbSeeder
    {
        public void Seed(Models.RaisinsDB context)
        {
            if (!context.Currencies.Any(c => c.CurrencyCode == "USD"))
            {
                Currency.Add(new Currency()
                {
                    CurrencyCode = "USD",
                    ExchangeRate = 41.0M,
                    Ratio = 1.0M
                });
            }
        }
    }
}