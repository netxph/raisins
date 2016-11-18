using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Raisins.Client.Web.Models
{
    public class MailQueue
    {

        public MailQueue()
        {

        }

        public MailQueue(Payment payment)
        {
            MailMessage message = new MailMessage(
                                        "no-reply@navitaire.com",
                                        payment.Email,
                                        "[TALENTS FOR HUNGRY MINDS 2016] Ticket Notification",
                                        payment.GenerateMessageBody())
            {
                IsBodyHtml = true
            };

            From = message.From.Address;
            To = message.To[0].Address;
            Subject = message.Subject;
            Content = message.Body;
        }

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
