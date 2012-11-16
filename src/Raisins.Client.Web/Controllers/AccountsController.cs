using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Raisins.Client.Web.Controllers
{
    public class AccountsController : Controller
    {

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Account.Login(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
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

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            if (ModelState.IsValid)
            {
                Account.ChangePassword(changePassword.NewPassword);

                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            return View(changePassword);
        }

    }
}
