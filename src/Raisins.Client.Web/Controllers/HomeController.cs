using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pages()
        {
            return View();
        }

        public ActionResult Reports()
        {
            var summaries = Summary.GetSummaryReport();
            ViewBag.TotalRemittedInLocal = summaries.Sum(s => s.TotalRemittedInLocal);
            ViewBag.TotalRemittedInGlobal = summaries.Sum(s => s.TotalRemittedInGlobal);
            ViewBag.TotalCashInLocal = summaries.Sum(s => s.TotalCashInLocal);
            ViewBag.TotalCashInGlobal = summaries.Sum(s => s.TotalCashInGlobal);

            return View(summaries);
        }

    }
}
