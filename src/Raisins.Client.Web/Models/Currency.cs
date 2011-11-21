using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Data;

namespace Raisins.Client.Web.Models
{
    public class Currency
    {
        public int CurrencyID { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Ratio { get; set; }

        public decimal ExchangeRate { get; set; }

        public static Currency Get(int id)
        {
            return RaisinsDB.Instance.Currencies.Where(currency => currency.CurrencyID == id).FirstOrDefault();
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
                    return RaisinsDB.Instance.Currencies.DefaultIfEmpty().ToArray();
                }
            }
            else
            {
                return null;
            }
        }
    }
}