using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class Currency
    {
        public Currency()
        {
        }
        public Currency(string currencyCode, decimal ratio, decimal exchangeRate)
        {
            CurrencyCode = currencyCode;
            ratio = Ratio;
            ExchangeRate = exchangeRate;
        }
        [Key]
        public int CurrencyID { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Ratio { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }
    }
}
