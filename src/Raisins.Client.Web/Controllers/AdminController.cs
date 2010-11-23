using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            BeneficiaryModel[] models = null;
            
            models = BeneficiaryService.GetStatistics(HttpContext.User.Identity.Name);

            return View(models);
        }

    }
}
