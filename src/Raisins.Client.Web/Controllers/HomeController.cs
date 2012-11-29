using Raisins.Client.Web.Models;
using Raisins.Client.Web.Services;
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
            ViewBag.Percentage = Convert.ToInt32((ViewBag.Total / ViewBag.Target) * 100);

            return View(beneficiaries);
        }

        [AuthorizeActivity("Home.Dashboard")]
        public ActionResult Dashboard()
        {
            ViewBag.Votes = VoteSummary.Get();
            ViewBag.ExecVotes = ExecutiveSummary.Get();
            ViewBag.Overall = OverallSummary.Get();

            return View();
        }

        public ActionResult Mechanics()
        {
            return View();
        }
    }
}
