using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using Raisins.Services;

namespace Raisins.Client.Web.Controllers
{
    public class TicketController : Controller
    {
        public ActionResult Show(long id)
        {
            ViewData["PaymentID"] = id;

            return View(TicketService.FindByPayment(id));
        }

        public ActionResult Print(long id)
        {
            return View(TicketService.FindByPayment(id));
        }

        public ActionResult Email(long id)
        {
            PaymentModel payment = PaymentService.GetPayment(id);
            TicketModel[] ticketModelList = TicketService.FindByPayment(id);

            try
            {
                if (payment != null && !string.IsNullOrEmpty(payment.Email) && !string.IsNullOrWhiteSpace(payment.Email))
                {
                    ViewData["toEmailAddress"] = payment.Email;
                    EmailService.SendEmail(ticketModelList, payment.Email, payment);
                    
                    MailLog mailLog = new MailLog();
                    mailLog.IsSuccessful = true;
                    mailLog.LastSent = DateTime.UtcNow;
                    mailLog.Payment = PaymentService.ToData(payment);

                    mailLog.Save();
                }
            }
            catch (Exception ex)
            {
                ViewData["toEmailAddress"] = null;

                MailLog mailLog = new MailLog();
                mailLog.IsSuccessful = false;
                mailLog.LastSent = DateTime.UtcNow;
                mailLog.Payment = PaymentService.ToData(payment);
                mailLog.Save();
            }

            return View(ticketModelList);
        }
    }
}
