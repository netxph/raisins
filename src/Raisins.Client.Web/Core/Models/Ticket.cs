using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Ticket
    {
        [Key]
        public long ID { get; set; }

        public string TicketCode { get; set; }

        public string Name { get; set; }

    }
}
