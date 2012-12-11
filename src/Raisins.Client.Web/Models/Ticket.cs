using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Ticket
    {

        [Key]
        public long ID { get; set; }

        public string TicketCode { get; set; }

        public string Name { get; set; }


        public static List<Ticket> GetAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Tickets.ToList();
            }
        }
    }
}
