using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class PaymentExport
    {
        public string Name
        { get; set; }
        public string Email
        { get; set; }
        public string Amount
        { get; set; }
        public string Beneficiary
        { get; set; }
        public string Currency
        { get; set; }
        public string Type
        { get; set; }
        public string Source
        { get; set; }
        public string Date
        { get; set; }
        public string OptOut
        { get; set; }
    }
}