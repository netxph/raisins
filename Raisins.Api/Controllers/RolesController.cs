using Raisins.Data.Repository;
using Raisins.Roles.Interfaces;
using Raisins.Roles.Services;
using D = Raisins.Roles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Api.Models;
using AutoMapper;
using Raisins.Accounts.Services;
using Raisins.Roles;

namespace Raisins.Api.Controllers
{
    public class RolesController : ApiController
    {
        private readonly IRoleService _service;
        protected IRoleService Service { get { return _service; } }

        public RolesController() : this(new RestrictRoleService(
            new Roles.Services.RoleService(
                new RestrictRoleRepository(
                new RoleRepository()))))
        {
        }

        public RolesController(IRoleService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("RolesListController:service");
            }
            _service = service;
        }
        [HttpPost]
        public HttpResponseMessage CreateRole([FromBody]Role role)
        {
            D.Role temp = new D.Role(role.Name, role.Permissions);

            try
            {
                Service.Add(temp);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (InvalidRoleException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public D.Role GetRole(int roleID)
        {
            return Service.Get(roleID);
        }
        [HttpPut]
        public HttpResponseMessage EditRole([FromBody]Role role)
        {
            D.Role temp = new D.Role(role.RoleID, role.Name, role.Permissions);
            Service.Edit(temp);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
