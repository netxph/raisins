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

namespace Raisins.Api.Controllers
{
    public class RolesListController : ApiController
    {
        private readonly IRoleService _service;
        protected IRoleService Service { get { return _service; } }

        public RolesListController() : this (new RoleService(new RoleRepository()))
        {
        }

        public RolesListController(IRoleService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("RolesListController:service");
            }
            _service = service;
        }

        [HttpGet]
        public D.Roles GetRolesList()
        {
            return Service.GetList();
        }
    }
}
