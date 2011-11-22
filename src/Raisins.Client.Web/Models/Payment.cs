using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Raisins.Client.Web.Data;
using System.Net.Mail;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Payment
    {

        #region Properties

        public int PaymentID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0.0D, double.MaxValue, ErrorMessage="Amount must be greater than zero")]
        public decimal Amount { get; set; }

        public string Location { get; set; }
        public Currency Currency { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Locked { get; set; }

        [Required]
        public int Class { get; set; }
        public string Remarks { get; set; }
        public Account AuditedBy { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Account CreatedBy { get; set; }
        public Beneficiary Beneficiary { get; set; }
 
        #endregion

        #region Methods

        public static Payment[] GetAll()
        {
            var db = new RaisinsDB();

            if (Account.CurrentUser.RoleType == (int)RoleType.User)
            {
                return db.Payments.Include("Beneficiary").Include("Currency").Where(payment => payment.CreatedBy.AccountID == Account.CurrentUser.AccountID).OrderBy(payment => payment.Currency.CurrencyCode).ToArray();
            }
            else
            {
                return db.Payments.Include("Beneficiary").Include("Currency").Where(payment => payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID).OrderBy(payment => payment.Currency.CurrencyCode).ToArray();
            }
        }

        public static decimal GetCashOnHand()
        {
            decimal result = 0.0m;

            var db = new RaisinsDB();

            if (((RoleType)Account.CurrentUser.RoleType) == RoleType.User)
            {
                result = db.Payments
                    .Where((payment) => payment.CreatedBy.UserName == Account.CurrentUser.UserName && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }
            else
            {
                result = db.Payments
                    .Where((payment) => payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID && !payment.Locked)
                    .Select(payment => payment.Amount * payment.Currency.ExchangeRate)
                    .DefaultIfEmpty()
                    .Sum();
            }

            return result;

        }

        protected static Payment SetDefaults(Payment payment, RaisinsDB db)
        {
            if (Account.CurrentUser != null)
            {
                if (Account.CurrentUser.Setting != null)
                {
                    payment.Location = Account.CurrentUser.Setting.Location;
                }
            }

            payment.Beneficiary = db.Beneficiaries.Find(payment.Beneficiary.BeneficiaryID);
            payment.Currency = db.Currencies.Find(payment.Currency.CurrencyID);

            return payment;
        }

        public static bool CreateNew(Payment item)
        {
            RaisinsDB db = new RaisinsDB();
            SetDefaults(item, db);

            item.CreatedBy = db.Accounts.FirstOrDefault(account => account.AccountID == Account.CurrentUser.AccountID);

            db.Payments.Add(item);

            db.SaveChanges();

            return true;
        }

        public static bool Update(Payment item)
        {
            RaisinsDB db = new RaisinsDB();

            var payment = db.Payments.Where(p => p.PaymentID == item.PaymentID).FirstOrDefault();
            SetDefaults(item, db);

            payment.Beneficiary = item.Beneficiary;
            payment.Amount = item.Amount;
            payment.Currency = item.Currency;
            payment.Class = item.Class;
            payment.Name = item.Name;
            payment.Email = item.Email;
            payment.Remarks = item.Remarks;

            db.SaveChanges();

            return true;
        }

        #endregion


        public static bool Delete(int id)
        {
            RaisinsDB db = new RaisinsDB();

            db.Payments.Remove(db.Payments.Where(payment => payment.PaymentID == id).FirstOrDefault());
            db.SaveChanges();

            return true;
        }

        public static Payment Get(int id)
        {
            RaisinsDB db = new RaisinsDB();

            return db.Payments.Include("Beneficiary").Include("Currency").Where(payment => payment.PaymentID == id).FirstOrDefault();
        }

        public static bool LockLocal()
        {
            if (Account.CurrentUser.RoleType == (int)RoleType.Auditor)
            { 
                RaisinsDB db = new RaisinsDB();

                var payments = db.Payments.Include("Beneficiary").Include("Currency").Include("Tickets").Where(payment => !payment.Locked && payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID && payment.Class != (int)PaymentClass.Foreign).ToList();

                var currentUser = db.Accounts.FirstOrDefault(a => a.AccountID == Account.CurrentUser.AccountID);
                Lock(payments, currentUser);

                db.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool LockForeign()
        {
            if (Account.CurrentUser.RoleType == (int)RoleType.Auditor)
            {
                RaisinsDB db = new RaisinsDB();

                var payments = db.Payments.Include("Beneficiary").Include("Currency").Include("Tickets").Where(payment => !payment.Locked && payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID && payment.Class == (int)PaymentClass.Foreign).ToList();

                var currentUser = db.Accounts.FirstOrDefault(a => a.AccountID == Account.CurrentUser.AccountID);
                Lock(payments, currentUser);

                db.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool LockAll()
        {
            if (Account.CurrentUser.RoleType == (int)RoleType.Auditor)
            {
                RaisinsDB db = new RaisinsDB();

                var payments = db.Payments.Include("Beneficiary").Include("Currency").Include("Tickets").Where(payment => !payment.Locked && payment.Beneficiary.BeneficiaryID == Account.CurrentUser.Setting.BeneficiaryID).ToList();

                var currentUser = db.Accounts.FirstOrDefault(a => a.AccountID == Account.CurrentUser.AccountID);
                Lock(payments, currentUser);

                db.SaveChanges();

                //email
                foreach (var payment in payments)
                {
                    EmailTickets(payment);
                }

                return true;
            }

            return false;
        }

        public static bool EmailTickets(Payment payment)
        {
            bool isSuccessful;
            try
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Hello {0},\r\n\r\n", payment.Name);
                messageBuilder.AppendLine("Thank you for supporting Pasko 2011.");
                messageBuilder.AppendLine("These are the ticket number generated for you:");
                messageBuilder.AppendLine();
                foreach (var ticket in payment.Tickets)
                {
                    messageBuilder.AppendLine(ticket.TicketCode);
                }

                messageBuilder.AppendLine();
                messageBuilder.AppendLine("Navitaire Pasko 2011 Committee");

                MailMessage mail = new MailMessage("marc.vitalis@navitaire.com", payment.Email);
                mail.Subject = "Pasko 2011 Tickets";
                mail.Body = messageBuilder.ToString();

                SmtpClient client = new SmtpClient("mailhost.navitaire.com", 25);
                client.Send(mail);
                isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
            }

            var mailLog = new MailLog()
            {
                PaymentID = payment.PaymentID,
                EmailAddress = payment.Email,
                IsSuccessful = isSuccessful,
                TimeStamp = DateTime.UtcNow
            };

            RaisinsDB db = new RaisinsDB();
            db.MailLogs.Add(mailLog);
            db.SaveChanges();

            return isSuccessful;
        }

        protected static bool Lock(IEnumerable<Payment> payments, Account user)
        {
            foreach (var payment in payments)
            {
                payment.Locked = true;
                payment.AuditedBy = user;

                //generate ticket
                generateTicket(payment);
            }

            return true;
        }

        private static void generateTicket(Payment payment)
        {
            var numOfTickets = (int)Math.Truncate(payment.Amount / payment.Currency.Ratio);

            List<Ticket> tickets = new List<Ticket>();

            for (int i = 0; i < numOfTickets; i++)
            {
                //generate code
                string code = string.Format("{0}{1}-{2}{3}", 
                    payment.Class.ToString(), 
                    payment.Beneficiary.BeneficiaryID.ToString("D2"), 
                    payment.PaymentID.ToString("X4"),
                    i.ToString("X3"));

                payment.Tickets.Add(new Ticket() { TicketCode = code, Name = payment.Name });
            }
        }
    }

    public enum PaymentClass
    {
        NotSpecified,
        Internal,
        Foreign,
        External
    }

}