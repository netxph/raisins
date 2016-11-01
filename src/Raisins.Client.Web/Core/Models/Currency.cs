using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Currency
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Ratio { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }

    }
}