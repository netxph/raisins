using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Beneficiary
    {
        public long BeneficiaryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}