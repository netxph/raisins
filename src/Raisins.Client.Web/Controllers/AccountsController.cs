using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Raisins.Client.Web.Controllers
{
    public class AccountsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                Account account = _unitOfWork.Accounts.GetUserAccount(model.UserName);

                if(account.IsValidAccount(model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
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

                var account = _unitOfWork.Accounts.GetCurrentUserAccount();
                var salt = Helper.CreateSalt();
                account.SetSalt(salt);
                account.GenerateNewPassword(changePassword.NewPassword, salt);
                _unitOfWork.Accounts.Edit(account);
                _unitOfWork.Complete();

                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            return View(changePassword);
        }

        //TODO
        //public ActionResult CreateUSer(string userName, string password)
        //{
        //    var salt = Helper.CreateSalt();
        //    var roles = new List<Role> { Role.Find("User") };
        //    Account account = new Account() { UserName = userName, Salt = salt, Password = GetHash(password, salt), Roles = roles, Profile = new AccountProfile() };

        //    roles.SetState(db, EntityState.Modified);
        //    profile.Beneficiaries.SetState(db, EntityState.Modified);
        //    profile.Currencies.SetState(db, EntityState.Modified);

        //    db.Accounts.Add(account);
        //    db.SaveChanges();

        //    return db.Accounts.FirstOrDefault(a => a.UserName == userName);
            
        //}

    }
}
