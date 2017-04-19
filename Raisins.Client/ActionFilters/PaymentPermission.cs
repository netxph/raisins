using Raisins.Client.Models;
using Raisins.Client.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Raisins.Client.ActionFilters
{
    public class PaymentPermission : ActionFilterAttribute, IActionFilter
    {
        private string _permission { get; set; }
        public PaymentPermission(string permission)
        {
            _permission = permission;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters["model"] != null && filterContext.HttpContext.Session["token"] != null)
            {
                var model = filterContext.ActionParameters["model"] as PaymentViewModel;
                string token = filterContext.HttpContext.Session["token"].ToString();

                var clientT = new RestClient("http://localhost:4000/api/accounts/Validate");
                var requestT = new RestRequest(Method.GET);
                requestT.AddParameter("encrypted", token);
                var responseT = clientT.Execute<Token>(requestT);
                JsonDeserializer deserialize = new JsonDeserializer();
                Token deserialized = deserialize.Deserialize<Token>(responseT);

                bool validate = false;
                foreach (var permission in deserialized.Permissions)
                {
                    if (permission == _permission)
                    {
                        validate = true;
                    }
                }
                if (!validate)
                {
                    filterContext.Controller.TempData.Add("message", "Sorry, but you do not have permission to do this action.");
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                                { "controller", "home" },
                                { "action", "index" }
                        });
                }
                else
                {
                    var client = new RestClient("http://localhost:4000/api/profile");
                    var request = new RestRequest(Method.GET);
                    request.AddParameter("userName", deserialized.User);
                    var response = client.Execute(request);
                    AccountProfile profile = deserialize.Deserialize<AccountProfile>(response);
                    validate = false;
                    if (profile.Beneficiaries != null)
                    {
                        foreach (var ben in profile.Beneficiaries)
                        {
                            if (ben.Name == model.Beneficiary)
                            {
                                validate = true;
                            }
                        }
                    }
                    if (model.Beneficiary.ToLower() == "none")
                    {
                        validate = true;
                    }
                    if (!validate)
                    {
                        filterContext.Controller.TempData.Add("message", "Sorry, but you cannot create a payment for a beneficiary not assigned to you.");
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                            {
                                { "controller", "home" },
                                { "action", "index" }
                            });
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary
              {
                    { "controller", "accounts" },
                    { "action", "Login" }
              });
            }
        }
    }
}