using Raisins.Data.Repository;
using Raisins.Roles.Interfaces;
using Raisins.Roles.Services;
using System;
using System.Web.Http;
using D = Raisins.Roles.Models;

namespace Raisins.Api.Controllers
{
    public class RolesListController : ApiController
    {
        private readonly IRoleService _service;
        protected IRoleService Service { get { return _service; } }

        public RolesListController() : this (
            new RoleService(
                new RestrictRoleRepository(
                    new RoleRepository())))
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
