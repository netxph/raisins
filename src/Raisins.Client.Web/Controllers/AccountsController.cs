using Raisins.Client.Web.Core;
using Raisins.Client.Web.Core.ViewModels;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            Account account = _unitOfWork.Accounts.GetCurrentUserAccount();
            var accountViewModel = new AccountViewModel
            {
                Name = account.Profile.Name,
                Username = account.UserName,
                RoleObject = account.Roles.FirstOrDefault(),
                Beneficiaries = account.Profile.Beneficiaries,
                Currencies = account.Profile.Currencies
            };
            return View(accountViewModel);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        
        public ActionResult Login(LoginModel model)
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

        [HttpGet]
        public ActionResult Create()
        {
            var accountViewModel = new AccountViewModel
            {
                Beneficiaries = _unitOfWork.Beneficiaries.GetAll(),
                Currencies = _unitOfWork.Currencies.GetAll(),
                Roles = _unitOfWork.Roles.GetAll()
            };
            return View(accountViewModel);
        }

        [HttpPost]
        public ActionResult Create(AccountViewModel viewModel)
        {
            if(!ModelState.IsValid || _unitOfWork.Accounts.Any(viewModel.Username))
            {
                viewModel.Beneficiaries = _unitOfWork.Beneficiaries.GetAll();
                viewModel.Currencies = _unitOfWork.Currencies.GetAll();
                viewModel.Roles = _unitOfWork.Roles.GetAll();

                return View(viewModel);
            }

            Role role = _unitOfWork.Roles.Find(viewModel.Role);

            List<Beneficiary> beneficiaries = role.IsAdmin() ? _unitOfWork.Beneficiaries.GetAll().ToList() :
                                                             new List<Beneficiary>() { _unitOfWork
                                                                                        .Beneficiaries
                                                                                        .Find(viewModel.Beneficiary) };
            List<Currency> currencies = role.IsAdmin() ? _unitOfWork.Currencies.GetAll().ToList() : 
                                                        new List<Currency> { _unitOfWork
                                                                                .Currencies
                                                                                .Find(viewModel.Currency) };
            Account account = new Account
            {
                UserName = viewModel.Username,
                Password = viewModel.Password,
                Roles = new List<Role>() { _unitOfWork.Roles.Find(viewModel.Role) },
                Profile = new AccountProfile
                {
                    Name = viewModel.Name,
                    Beneficiaries = beneficiaries,
                    Currencies = currencies
                }
            };
            var salt = Helper.CreateSalt();
            account.SetSalt(salt);
            account.GenerateNewPassword(viewModel.Password, salt);

            _unitOfWork.Accounts.Add(account);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Home");
        }

    }
}
