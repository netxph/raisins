using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var beneficiaries = Beneficiary.GetAll();

            var totals = Payment.GetTotalSummary();

            ViewBag.Posted = totals["Posted"];
            ViewBag.Total = totals["Total"];
            ViewBag.Target = totals["Target"];

            return View(beneficiaries);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Mechanics()
        {
            return View();
        }
    }
}
