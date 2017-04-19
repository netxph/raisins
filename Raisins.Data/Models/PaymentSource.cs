using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class PaymentSource
    {
        [Key]
        public int PaymentSourceID { get; set; }
        public string Source { get; set; }
    }
}
