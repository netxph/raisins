﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        const decimal TARGET = 600000;

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

        public static List<Payment> GetAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Include(p => p.Executive).ToList();
            }
        }

        public static List<Payment> FindAllForUser()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Account currentAccount = Account.GetCurrentUser();

                var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

                IEnumerable<Payment> payments = null;

                //convert to activity
                if (currentAccount.Roles.Exists(r => r.Name == "User"))
                {
                    payments = db.Payments
                        .Include(p => p.Beneficiary)
                        .Include(p => p.Currency)
                        .Include(p => p.CreatedBy)
                        .Include(p => p.AuditedBy)
                        .Where(p => beneficiaryIds.Contains(p.BeneficiaryID) && p.CreatedByID == currentAccount.ID);
                }
                else if (currentAccount.Roles.Exists(r => r.Name == "Accountant"))
                {
                    payments = db.Payments
                        .Include(p => p.Beneficiary)
                        .Include(p => p.Currency)
                        .Include(p => p.CreatedBy)
                        .Include(p => p.AuditedBy)
                        .Where(p => beneficiaryIds.Contains(p.BeneficiaryID));
                }
                else
                {
                    payments = db.Payments
                        .Include(p => p.Beneficiary)
                        .Include(p => p.Currency)
                        .Include(p => p.CreatedBy)
                        .Include(p => p.AuditedBy);
                }

                return payments.ToList();
            }
        }

        public static Payment Find(int id = 0)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Payments
                    .Include(p => p.Tickets)
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Single(p => p.ID == id);
            }
        }

        public static Payment Add(Payment payment)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                payment.CreatedByID = Account.GetCurrentUser().ID;

                db.Payments.Add(payment);
                db.SaveChanges();

                return payment;
            }
        }

        public static Payment Edit(Payment payment)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                payment.CreatedByID = Account.GetCurrentUser().ID;

                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();

                return payment;
            }
        }

        public static void Delete(int id)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Payment payment = db.Payments.Find(id);
                db.Payments.Remove(payment);
                db.SaveChanges();
            }
        }

        public static void LockAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Account currentAccount = Account.GetCurrentUser();

                var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

                var payments = db.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Where(p => beneficiaryIds.Contains(p.BeneficiaryID));

                foreach (var payment in payments)
                {
                    if (!payment.Locked)
                    {
                        db.Entry(payment).State = EntityState.Modified;

                        payment.Locked = true;
                        payment.AuditedByID = Account.GetCurrentUser().ID;
                        payment.Tickets = generateTickets(payment);

                        emailTickets(payment.Email, payment.Tickets, payment.BeneficiaryID);
                    }
                }

                db.SaveChanges();
            }
        }

        public static void LockLocal()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Account currentAccount = Account.GetCurrentUser();

                var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

                var payments = db.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Where(p => beneficiaryIds.Contains(p.BeneficiaryID) && (p.ClassID == (int)PaymentClass.Local || p.ClassID == (int)PaymentClass.External));

                foreach (var payment in payments)
                {
                    if (!payment.Locked)
                    {
                        db.Entry(payment).State = EntityState.Modified;

                        payment.Locked = true;
                        payment.AuditedByID = Account.GetCurrentUser().ID;
                        payment.Tickets = generateTickets(payment);

                        emailTickets(payment.Email, payment.Tickets, payment.BeneficiaryID);
                 }
                }

                db.SaveChanges();
            }
        }

        public static void LockForeign()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                Account currentAccount = Account.GetCurrentUser();

                var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

                var payments = db.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Where(p => beneficiaryIds.Contains(p.BeneficiaryID) && p.ClassID == (int)PaymentClass.Foreign);

                foreach (var payment in payments)
                {
                    if (!payment.Locked)
                    {
                        db.Entry(payment).State = EntityState.Modified;

                        payment.Locked = true;
                        payment.AuditedByID = Account.GetCurrentUser().ID;
                        payment.Tickets = generateTickets(payment);

                        emailTickets(payment.Email, payment.Tickets, payment.BeneficiaryID);
                                           }
                }

                db.SaveChanges();
            }
        }

    
                //MailMessage message = new MailMessage("no-reply@navitaire.com", email);
                //message.Body = content;
                //message.Subject = "[TALENTS FOR HUNGRY MINDS 2013] Ticket Notification";
                //message.IsBodyHtml = true;

                //SmtpClient smtp = new SmtpClient("mailhost.navitaire.com",25);
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.Send(message);

        private static void emailTickets(string email, List<Ticket> tickets, int id)
        {
            StringBuilder builder = new StringBuilder();
            string groupName = string.Empty;
            foreach (var ticket in tickets)
            {
                builder.Append(ticket.TicketCode);
                builder.AppendLine("<br />");
            }

            try
            {
                groupName = Beneficiary.Find(id).Name;
            }
            catch { }

            string content = string.Format(Templates.EMAIL, groupName,  tickets[0].Name, builder.ToString());

            try
            {
                MailMessage message = new MailMessage("no-reply@navitaire.com", email);
                message.Body = content;
                message.Subject = "[TALENTS FOR HUNGRY MINDS 2014] Ticket Notification";
                message.IsBodyHtml = true;

                //change this to DefaultMailer if you wish to send directly to smtp
                IMailer smtp = new DelegatedMailer();

                smtp.SendMessage(message);
            }
            catch { }

        }

        private static List<Ticket> generateTickets(Payment payment)
        {
            List<Ticket> tickets = new List<Ticket>();
            int count = (((((int)((payment.Amount) * payment.Currency.ExchangeRate)) / 2000) * 55) +
                                                    ((((int)(payment.Amount * payment.Currency.ExchangeRate) % 2000) / 1000) * 25) +
                                                    (((((int)(payment.Amount * payment.Currency.ExchangeRate) % 2000) % 1000) / 500) * 12) +
                                                    ((((((int)(payment.Amount * payment.Currency.ExchangeRate) % 2000) % 1000) % 500) / 50) * 1)
                                                    );

            for (int i = 0; i < count; i++)
            {
                tickets.Add(new Ticket() { Name = payment.Name, TicketCode = string.Format("{0}{1}{2}{3}", payment.ClassID.ToString("00"), payment.BeneficiaryID.ToString("00"), payment.ID.ToString("X").PadLeft(5, '0'), i.ToString("00000")) });
            }

            return tickets;
        }

        public static void ResendEmail()
        {
            var payments = FindLockedPayments();

            foreach (var payment in payments)
            {
                ResendEmail(payment);
            }
        }

        public static List<Payment> FindLockedPayments()
        {
            List<Payment> payments = null;

            using (var db = ObjectProvider.CreateDB())
            {
                payments = db.Payments.Where(p => p.Locked == true).Include(p => p.Tickets).ToList();
            }

            return payments;
        }

        public static void ResendEmail(Payment payment)
        {
            //TODO: secure
            emailTickets(payment.Email, payment.Tickets, payment.BeneficiaryID);
        }

        public static void ResendEmail(int id)
        {
            var payment = Find(id);

            ResendEmail(payment);
        }

        public static Dictionary<string, decimal> GetTotalSummary()
        {
            Dictionary<string, decimal> totals = new Dictionary<string, decimal>();

            //TODO: put this in configuration
            totals.Add("Target", TARGET);

            using (var db = ObjectProvider.CreateDB())
            {
                var payments = db.Payments.Include("Currency").ToList();

                var posted = payments.Where(p => p.Locked).Sum(p => p.Amount * p.Currency.ExchangeRate);

                totals.Add("Posted", posted);

                var total = payments.Sum(p => p.Amount * p.Currency.ExchangeRate);

                totals.Add("Total", total);
            }

            return totals;
        }
        
    }
}
