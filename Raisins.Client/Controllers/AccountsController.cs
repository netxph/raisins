using Raisins.Client.ActionFilters;
using Raisins.Client.Models;
using Raisins.Client.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Raisins.Client.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            LoginViewModel model = (LoginViewModel)TempData["model"];
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            var client = new RestClient("http://localhost:4000/api/accounts/login");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(model);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["token"] = response.Headers.ToList().Find(x => x.Name == "X-Session-Token").Value.ToString();
                Session["user"] = model.Username;
                FormsAuthentication.SetAuthCookie(model.Username, true);
                ViewBag.Username = model.Username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["model"] = model;
                return RedirectToAction("Login", "Accounts");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }

        [BasicPermissions("accounts_create")]
        [HttpGet]
        public ActionResult NewAccount()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            RegisterViewModel model = new RegisterViewModel();
            var clientR = new RestClient("http://localhost:4000/api/roleslist");
            var requestR = new RestRequest(Method.GET);
            var responseR = clientR.Execute<List<Role>>(requestR);
            List<Role> roles = deserialize.Deserialize<List<Role>>(responseR);

            var clientB = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            List<CheckModel> checkmodels = new List<CheckModel>();
            var item = beneficiaries.Single(x => x.Name.ToLower() == "none");
            beneficiaries.Remove(item);
            foreach (var beneficiary in beneficiaries)
            {
                checkmodels.Add(new CheckModel(beneficiary.BeneficiaryID, beneficiary.Name));
            }
            model.InitResources(roles, checkmodels);

            return View(model);
        }

        [BasicPermissions("accounts_create")]
        [HttpPost]
        public ActionResult NewAccount(RegisterViewModel model)
        {
            Role role = new Role(model.Role);
            Account account = new Account(model.Username, model.Password, role);
            List<Beneficiary> list = new List<Beneficiary>();
            foreach(var selected in model.Checkboxes)
            {
                if (selected.Checked)
                {
                    list.Add(new Beneficiary(selected.Id, selected.Name));
                }
            }
            AccountProfile profile = new AccountProfile(model.Name, list);
            AccountComplete complete = new AccountComplete(account, profile);

            var client = new RestClient("http://localhost:4000/api/AccountsCreate");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(complete);
            var response = client.Execute(request);

            return RedirectToAction("ViewAccountList", "Accounts");
        }

        [BasicPermissions("accounts_view")]
        [HttpGet]
        public ActionResult ViewAccountList()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/accountsAll");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Role>>(request);
            List<Account> accounts = deserialize.Deserialize<List<Account>>(response);
            AccountsListViewModel model = new AccountsListViewModel(accounts);
            return View(model);
        }

        [BasicPermissions("accounts_edit")]
        [HttpGet]
        public ActionResult EditAccount(string userName)
        {
            RegisterViewModel model = new RegisterViewModel();
            JsonDeserializer deserialize = new JsonDeserializer();

            var client = new RestClient("http://localhost:4000/api/AccountsCreate");
            var request = new RestRequest(Method.GET);
            request.AddParameter("userName", userName);
            var response = client.Execute<Account>(request);
            Account account = deserialize.Deserialize<Account>(response);
            
            var clientR = new RestClient("http://localhost:4000/api/roleslist");
            var requestR = new RestRequest(Method.GET);
            var responseR = clientR.Execute<List<Role>>(requestR);
            List<Role> roles = deserialize.Deserialize<List<Role>>(responseR);

            
            var clientB = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            List<CheckModel> checkmodels = new List<CheckModel>();
            var item = beneficiaries.Single(x => x.Name.ToLower() == "none");
            beneficiaries.Remove(item);
            foreach (var beneficiary in beneficiaries)
            {
                checkmodels.Add(new CheckModel(beneficiary.BeneficiaryID, beneficiary.Name));
            }
            foreach(var checkbox in checkmodels)
            {
                foreach (var beneficiary in account.Profile.Beneficiaries)
                {
                    if (checkbox.Name == beneficiary.Name)
                    {
                        checkbox.SetChecked(true);
                    }
                }
            }
            
            model.InitResources(account, roles, checkmodels);

            return View(model);
        }
        public ActionResult EditAccount(RegisterViewModel model)
        {
            Role role = new Role(model.Role);
            Account account = new Account(model.Username, model.Password, role);
            List<Beneficiary> list = new List<Beneficiary>();
            foreach (var selected in model.Checkboxes)
            {
                if (selected.Checked)
                {
                    list.Add(new Beneficiary(selected.Id, selected.Name));
                }
            }
            AccountProfile profile = new AccountProfile(model.Name, list);
            AccountComplete complete = new AccountComplete(account, profile);

            var client = new RestClient("http://localhost:4000/api/AccountsCreate");
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(complete);
            var response = client.Execute(request);

            return RedirectToAction("ViewAccountList", "Accounts");
        }
    }
}