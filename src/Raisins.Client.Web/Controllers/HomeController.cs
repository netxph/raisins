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

            ViewBag.TotalPosted = 100000;
            ViewBag.Total = 110000;
            ViewBag.Target = 600000;

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
