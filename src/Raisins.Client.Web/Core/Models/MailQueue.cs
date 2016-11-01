using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class MailQueue
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
