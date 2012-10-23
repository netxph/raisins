using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Ledger
    {
        [Key]
        public int LedgerID { get; set; }
        //public int BeneficiaryID { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}