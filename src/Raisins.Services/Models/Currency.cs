using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services.Data;

namespace Raisins.Services.Models
{
    public class Currency
    {
        public int CurrencyID { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Ratio { get; set; }

        public decimal ExchangeRate { get; set; }

        public static Currency Get(int id)
        {
            RaisinsDB db = new RaisinsDB();

            return db.Currencies.Where(currency => currency.CurrencyID == id).FirstOrDefault();
        }

        public static Currency[] GetAll()
        {
            RaisinsDB db = new RaisinsDB();
            return db.Currencies.DefaultIfEmpty().ToArray();
        }

        public static Currency[] GetAllForPayment()
        {
            if (Account.CurrentUser.Setting != null)
            {
                if (Account.CurrentUser.Setting.Class != (int)PaymentClass.Foreign)
                {
                    return new Currency[] { Get(Account.CurrentUser.Setting.CurrencyID) };
                }
                else
                {
                    RaisinsDB db = new RaisinsDB();
                    return db.Currencies.DefaultIfEmpty().ToArray();
                }
            }
            else
            {
                return null;
            }
        }

        public static Currency GetGlobalCurrency()
        {
            RaisinsDB db = new RaisinsDB();
            return db.Currencies.FirstOrDefault(c => c.CurrencyCode == "USD");
        }

        public static Currency GetLocalCurrency()
        {
            RaisinsDB db = new RaisinsDB();
            return db.Currencies.FirstOrDefault(c => c.CurrencyCode == "PHP");
        }
    }
}