using Raisins.Roles.Interfaces;
using Raisins.Roles.Models;
using System;
using System.Linq;

namespace Raisins.Data.Repository
{
    public class RestrictRoleRepository : IRoleRepository
    {
        private const string SUPER = "super";
        private readonly IRoleRepository _repository;

        protected IRoleRepository Repository { get { return _repository; } }

        public RestrictRoleRepository(IRoleRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        public Role Get(string Name)
        {
            return Repository.Get(Name);
        }

        public Role Get(int roleID)
        {
            return Repository.Get(roleID);
        }

        public Roles.Models.Roles GetList()
        {
            var r = Repository.GetList();
            var roles = r.Where(i => i.Name.ToLower() != SUPER)
                    .ToList();

            return new Roles.Models.Roles(roles);
        }

        public void Add(Role role)
        {
            if(role.Name.ToLower() != SUPER)
            {
                Repository.Add(role);
            }
        }

        public void Edit(Role role)
        {
            if (role.Name.ToLower() != SUPER)
            {
                Repository.Edit(role);
            }
        }

        public bool Any(string roleName)
        {
            return Repository.Any(roleName);
        }
    }
}