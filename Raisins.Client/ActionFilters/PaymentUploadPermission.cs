using Raisins.Client.Models;
using Raisins.Client.Services;
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
    public class PaymentUploadPermission : ActionFilterAttribute, IActionFilter
    {
        private string _permission { get; set; }
        public PaymentUploadPermission(string permission)
        {
            _permission = permission;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters["upload"] != null && filterContext.HttpContext.Session["token"] != null)
            {
                var model = filterContext.ActionParameters["upload"] as HttpPostedFileBase;
                string token = filterContext.HttpContext.Session["token"].ToString();

                var clientT = new RestClient(AppConfig.GetUrl("accounts/Validate"));
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
                    FileUploader uploader = new FileUploader();
                    IEnumerable<Payment> payments = uploader.ExcelUpload(model);
                    var client = new RestClient(AppConfig.GetUrl("profile"));
                    var request = new RestRequest(Method.GET);
                    request.AddParameter("userName", deserialized.User);
                    var response = client.Execute(request);
                    AccountProfile profile = deserialize.Deserialize<AccountProfile>(response);
                    int count = 0;
                    if (profile.Beneficiaries != null)
                    {
                        foreach (var ben in profile.Beneficiaries)
                        {
                            foreach (var payment in payments)
                            {
                                if (ben.Name == payment.Beneficiary.Name)
                                {
                                    count++;
                                    validate = false;
                                }
                            }
                        }
                    }
                    if (count != payments.Count())
                    {
                        filterContext.Controller.TempData.Add("message", "Sorry, but you cannot add payments for beneficiaries not assigned to you.");
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