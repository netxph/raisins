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

            return View(beneficiaries);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
