using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            AccountModel model = new AccountModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountModel model, string returnUrl)
        {
            if (AccountService.Logon(model.UserName, model.Password))
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            AccountService.Logout();

            return RedirectToAction("Index", "Home");
        }

    }
}
