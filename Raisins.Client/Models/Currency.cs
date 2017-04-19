using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Currency
    {
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }
        public decimal Ratio { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}