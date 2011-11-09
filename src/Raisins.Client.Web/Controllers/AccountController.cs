using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;
using System.Web.Security;
using Raisins.Client.Web.Data;
using System.Web.Profile;

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
            var account = Account.Login(model.UserName, model.Password);

            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
