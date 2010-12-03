using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

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
    }
}
