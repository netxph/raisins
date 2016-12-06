using Raisins.Client.Web.Core.ViewModels;
using System;
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

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Location { get; set; }

        [Required]
        public string Email { get; set; }

        public string SoldBy { get; set; }

        public string Remarks { get; set; }

        public List<Ticket> Tickets { get; set; }

        public int ClassID { get; set; }

        public bool Locked { get; set; }

        public int BeneficiaryID { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }

        public int? ExecutiveID { get; set; }
        public virtual Executive Executive { get; set; }

        public int CurrencyID { get; set; }
        public virtual Currency Currency { get; set; }

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
            int count = ConvertPaymentAmountToVotes();

            for (int i = 0; i < count; i++)
            {
                tickets.Add(new Ticket() { Name = Name, TicketCode = string.Format("{0}{1}{2}{3}", ClassID.ToString("00"), BeneficiaryID.ToString("00"), ID.ToString("X").PadLeft(5, '0'), i.ToString("00000")) });
            }

            return tickets;
        }

        public int ConvertPaymentAmountToVotes()
        {
            int convertedAmount = (int)(Amount * Currency.ExchangeRate);
            string currencyCode = Currency.CurrencyCode;
            if(currencyCode == "PHP")
            {
                PaymentCategory paymentCategory = new PaymentCategory
                {
                    PlatinumPaymentAmount = 2000, PlatinumPaymentVotes = 55,
                    GoldPaymentAmount = 1000, GoldPaymentVotes = 25,
                    SilverPaymentAmount = 500, SilverPaymentVotes = 12,
                    BronzePaymentAmount = 50, BronzePaymentVotes = 1
                };

                return NumberOfVotes(convertedAmount, paymentCategory);
            } else if (currencyCode == "USD")
            {
                PaymentCategory paymentCategory = new PaymentCategory
                {
                    PlatinumPaymentAmount = 40, PlatinumPaymentVotes = 55,
                    GoldPaymentAmount = 20, GoldPaymentVotes = 25,
                    SilverPaymentAmount = 10, SilverPaymentVotes = 12,
                    BronzePaymentAmount = 1, BronzePaymentVotes = 1
                };
                return NumberOfVotes((int)Amount, paymentCategory);
            } else
            {
                return (int)(Amount / Currency.Ratio);
            }
        }

        public int NumberOfVotes(int paymentAmount, PaymentCategory paymentCategory)
        {
            if (paymentCategory == null) throw new ArgumentNullException("paymentCategory", "Payment Category is null");

            int platinumPayment = paymentAmount / paymentCategory.PlatinumPaymentAmount * paymentCategory.PlatinumPaymentVotes;
            int goldPortionOfPayment = paymentAmount % paymentCategory.PlatinumPaymentAmount;
            int goldPayment = goldPortionOfPayment / paymentCategory.GoldPaymentAmount * paymentCategory.GoldPaymentVotes;
            int silverPortionOfPayment = goldPortionOfPayment % paymentCategory.GoldPaymentAmount;
            int silverPayment = silverPortionOfPayment / paymentCategory.SilverPaymentAmount * paymentCategory.SilverPaymentVotes;
            int bronzePortionOfPayment = silverPortionOfPayment % paymentCategory.SilverPaymentAmount;
            int bronzePayment = bronzePortionOfPayment / paymentCategory.BronzePaymentAmount * paymentCategory.BronzePaymentVotes;

            return platinumPayment + goldPayment + silverPayment + bronzePayment;
        }


    }
}
