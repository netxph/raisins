using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        public long PaymentID { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }

        public Currency Currency { get; set; }

        public string Email { get; set; }

        public bool Locked { get; set; }

        public int Class { get; set; }

        public string Remarks { get; set; }

        public Account AuditedBy { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public Account CreatedBy { get; set; }

    }

    public enum PaymentClass
    {
        NotSpecified,
        Internal,
        Foreign,
        External
    }

}