using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        public static string EmailTemplate { get; set; }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.0D, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        public string Location { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string SoldBy { get; set; }

        public string Remarks { get; set; }

        public List<Ticket> Tickets { get; set; }

        public int ClassID { get; set; }

        public bool Locked { get; set; }

        [Required]
        public int BeneficiaryID { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }

        public int? ExecutiveID { get; set; }
        public virtual Executive Executive { get; set; }

        [Required]
        public int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }

        [Required]
        public int CreatedByID { get; set; }
        public virtual Account CreatedBy { get; set; }

        public int? AuditedByID { get; set; }
        public virtual Account AuditedBy { get; set; }


        public void EmailTickets(string beneficiaryName)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Ticket ticket in Tickets)
            {
                builder.Append(ticket.TicketCode);
                builder.AppendLine("<br />");
            }


            string content = string.Format(Templates.EMAIL, beneficiaryName, Tickets[0].Name, builder.ToString());

            try
            {
                MailMessage message = new MailMessage("no-reply@navitaire.com", Email);
                message.Body = content;
                message.Subject = "[TALENTS FOR HUNGRY MINDS 2014] Ticket Notification";
                message.IsBodyHtml = true;

                //change this to DefaultMailer if you wish to send directly to smtp
                IMailer smtp = new DelegatedMailer();

                smtp.SendMessage(message);
            }
            catch { }

        }

        public List<Ticket> GenerateTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            int count = (((((int)((Amount) * this.Currency.ExchangeRate)) / 2000) * 55) +
                                                    ((((int)(Amount * Currency.ExchangeRate) % 2000) / 1000) * 25) +
                                                    (((((int)(Amount * Currency.ExchangeRate) % 2000) % 1000) / 500) * 12) +
                                                    ((((((int)(Amount * Currency.ExchangeRate) % 2000) % 1000) % 500) / 50) * 1)
                                                    );

            for (int i = 0; i < count; i++)
            {
                tickets.Add(new Ticket() { Name = Name, TicketCode = string.Format("{0}{1}{2}{3}", ClassID.ToString("00"), BeneficiaryID.ToString("00"), ID.ToString("X").PadLeft(5, '0'), i.ToString("00000")) });
            }

            return tickets;
        }
        
    }
}
