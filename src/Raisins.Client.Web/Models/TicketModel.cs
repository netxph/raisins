using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class TicketModel
    {

        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Beneficiary { get; set; }
        public string EmailAddress { get; set; }

    }
}