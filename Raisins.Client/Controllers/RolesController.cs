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

namespace Raisins.Client.Controllers
{
    public class RolesController : Controller
    {
        [BasicPermissions("roles_view")]
        [HttpGet]
        public ActionResult ViewRoleList()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/RolesList");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Role>>(request);
            List<Role> roles = deserialize.Deserialize<List<Role>>(response);
            RoleListViewModel model = new RoleListViewModel(roles);
            return View(model);
        }

        [BasicPermissions("roles_edit")]
        [HttpGet]
        public ActionResult EditRole(int RoleID)
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/roles");
            var request = new RestRequest(Method.GET);
            request.AddParameter("roleID", RoleID);
            var response = client.Execute<Role>(request);
            Role beneficiary = deserialize.Deserialize<Role>(response);
            RoleEditViewModel model = new RoleEditViewModel(beneficiary);
            return View(model);
        }

        [BasicPermissions("roles_edit")]
        [HttpPost]
        public ActionResult EditRole(RoleEditViewModel model)
        {
            Role role = new Role(model.RoleID, model.Name, model.Convert());
            var client = new RestClient("http://localhost:4000/api/roles");
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(role);
            var response = client.Execute(request);
            return RedirectToAction("ViewRoleList", "Roles");
        }

        [BasicPermissions("roles_create")]
        [HttpGet]
        public ActionResult NewRole()
        {
            return View();
        }

        [BasicPermissions("roles_create")]
        [HttpPost]
        public ActionResult NewRole(RoleEditViewModel model)
        {
            Role role = new Role(model.RoleID, model.Name, model.Convert());
            var client = new RestClient("http://localhost:4000/api/roles");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(role);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NewRole", "Roles");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}