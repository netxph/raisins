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
    public class BasicPermissions : ActionFilterAttribute, IActionFilter
    {
        private string _permission { get; set; }
        public BasicPermissions(string permission)
        {
            _permission = permission;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["token"] != null)
            {
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
                        //if (permission == "accounts_edit" && deserialized.User == "Super" || deserialized.User == "SuperUser")
                        //{
                        //    if(clickedRole == "Super")
                        //    {
                        //        validate = false;
                        //        break;
                        //    }
                        //}
                        validate = true;
                        break;
                    }
                }

                //var hasPermission = ((Controllers.AccountsController)filterContext.Controller).CheckPermission(deserialized.Permissions.ToString(), deserialized.User.ToString());
                
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