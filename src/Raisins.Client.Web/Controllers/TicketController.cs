using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Services.Models;

namespace Raisins.Client.Web.Controllers
{
    public class TicketController : Controller
    {

        public ActionResult Show(int id)
        {
            var tickets = Ticket.GetForPayment(id);

            return View(tickets);
        }
        
    }
}
