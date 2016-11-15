﻿using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private RaisinsDB _raisinsDb;

        public IEnumerable<Role> GetAll()
        {
            return _raisinsDb.Roles;
        }

        public RoleRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }

        public Role Get(string roleName)
        {
            return _raisinsDb.Roles.FirstOrDefault(r => r.Name == roleName);
        }

        public Role Find(int roleId)
        {
            return _raisinsDb.Roles.Find(roleId);
        }

        public void Add(Role role)
        {
            _raisinsDb.Roles.Add(role);
        }

    }
}