using Raisins.Client.Web.Core.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {
        public Payment()
        {

        }

        public Payment(PaymentViewModel paymentViewModel)
        {
            ID = paymentViewModel.Id;
            Name = paymentViewModel.Name;
            Location = paymentViewModel.Location;
            Email = paymentViewModel.Email;
            Amount = paymentViewModel.Amount;
            SoldBy = paymentViewModel.SoldBy;
            Remarks = paymentViewModel.Remarks;
            BeneficiaryID = paymentViewModel.BeneficiaryId;
            CurrencyID = paymentViewModel.CurrencyId;
            ClassID = paymentViewModel.PaymentClassId;
        }

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

        public string GenerateMessageBody()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Ticket ticket in Tickets)
            {
                builder.Append(ticket.TicketCode);
                builder.AppendLine("<br />");
            }

            return string.Format(Templates.EMAIL, Beneficiary.Name, Tickets[0].Name, builder.ToString());
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
