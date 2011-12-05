using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Services.Models
{
    public class WinnerLog
    {
        public long WinnerLogID { get; set; }

        public Ticket Ticket { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}