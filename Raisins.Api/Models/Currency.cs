using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Api.Models
{
    public class Currency
    {
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