using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Data;

namespace Raisins.Client.Web.Models
{
    public class Ticket
    {
        public long TicketID { get; set; }

        public string TicketCode { get; set; }

        public string Name { get; set; }


        public static Ticket[] GetForPayment(int id)
        {
            RaisinsDB db = new RaisinsDB();
            var payment = db.Payments.Include("Tickets").First(p => p.PaymentID == id);

            return payment.Tickets.ToArray();
        }
    }
}