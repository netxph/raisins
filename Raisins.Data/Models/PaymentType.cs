using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeID { get; set; }
        public string Type { get; set; }
    }
}
