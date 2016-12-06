using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using Raisins.Client.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Raisins.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var beneficiaries = _unitOfWork.Beneficiaries.GetAll();

            Dictionary<string, decimal> totals = new Dictionary<string, decimal>();
            const decimal TARGET = 700000;
            totals.Add("Target", TARGET);

            List<Payment> payments = _unitOfWork.Payments.GetPaymentWithCurrency().ToList();

            var posted = payments.Where(p => p.Locked).Sum(p => p.Amount * p.Currency.ExchangeRate);

            totals.Add("Posted", posted);

            var total = payments.Sum(p => p.Amount * p.Currency.ExchangeRate);

            totals.Add("Total", total);
            
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
