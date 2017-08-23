using Raisins.Client.Models;
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
    public class PaymentPublishPermission : ActionFilterAttribute, IActionFilter
    {
        private string _permission { get; set; }
        public PaymentPublishPermission(string permission)
        {
            _permission = permission;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters["paymentID"] != null && filterContext.HttpContext.Session["token"] != null)
            {
                int paymentID = (int)filterContext.ActionParameters["paymentID"];
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
                    filterContext.Result = new HttpNotFoundResult();
                }
                else
                {
                    //get payment
                    var client = new RestClient("http://localhost:4000/api/payments");
                    var request = new RestRequest(Method.GET);
                    request.AddParameter("paymentID", paymentID);
                    var response = client.Execute<List<Payment>>(request);
                    Payment payment = deserialize.Deserialize<Payment>(response);

                    //get profile
                    var clientP = new RestClient("http://localhost:4000/api/profile");
                    var requestP = new RestRequest(Method.GET);
                    requestP.AddParameter("userName", deserialized.User);
                    var responseP = clientP.Execute(requestP);
                    AccountProfile profile = deserialize.Deserialize<AccountProfile>(responseP);

                    validate = true;
                    if (profile.Beneficiaries != null)
                    {
                        foreach (var ben in profile.Beneficiaries)
                        {
                            if (ben.Name == payment.Beneficiary.Name)
                            {
                                validate = true;
                            }
                        }
                    }
                    if (payment.Beneficiary.Name.ToLower() == "none")
                    {
                        validate = true;
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