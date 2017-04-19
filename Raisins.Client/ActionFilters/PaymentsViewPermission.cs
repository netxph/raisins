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
    public class PaymentsViewPermission : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["token"] != null)
            {
                string token = filterContext.HttpContext.Session["token"].ToString();
                var clientT = new RestClient("http://localhost:4000/api/accounts/Validate");
                var requestT = new RestRequest(Method.GET);
                requestT.AddParameter("encrypted", token);
                var responseT = clientT.Execute<Token>(requestT);
                JsonDeserializer deserialize = new JsonDeserializer();
                Token deserialized = deserialize.Deserialize<Token>(responseT);

                bool validate = false;
                bool viewAll = false;
                foreach (var permission in deserialized.Permissions)
                {
                    if (permission == "payments_view_list")
                    {
                        validate = true;
                        break;
                    }
                    if (permission == "payments_view_list_all")
                    {
                        viewAll = true;
                        validate = true;
                    }
                }
                if (viewAll)
                {
                    filterContext.Result = new RedirectToRouteResult(
                  new RouteValueDictionary
                  {
                    { "controller", "payments" },
                    { "action", "ViewPaymentListAll" }
                  });
                }
                else if (!validate)
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