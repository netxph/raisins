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

        public ActionResult ChangePassword()
        {
            if (Account.CurrentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            if (Account.ChangePassword(form["OldPassword"], form["NewPassword"], form["ConfirmPassword"]))
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Create()
        {
            if (Account.CurrentUser.RoleType == (int)RoleType.Administrator)
            {
                var model = new Account() { Setting = new Setting() };

                ViewHelper.GetAccountReferences(this);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(Account account)
        {
            account.Salt = Account.GetSalt();
            account.Password = Account.GetHash(account.UserName, account.Salt);

            RaisinsDB db = new RaisinsDB();
            db.Accounts.Add(account);

            db.SaveChanges();    

            return RedirectToAction("Index", "Home");
        }
    }
}
