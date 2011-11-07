using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using System.Web.Security;
using Raisins.Client.Web.Data;

namespace Raisins.Client.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            Account model = new Account();

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Account model, string returnUrl)
        {
            if (Account.Login(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //AccountService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
