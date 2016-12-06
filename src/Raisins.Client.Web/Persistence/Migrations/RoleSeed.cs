using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;

namespace Raisins.Client.Web.Migrations
{
    public class RoleSeed : IDbSeeder
    {
        private UnitOfWork _unitOfWork;
        public void Seed(RaisinsDB context)
        {
            _unitOfWork = new UnitOfWork(context);
            //Role
            AddRole("Administrator");
            AddRole("Accountant");
            AddRole("Manager");
            AddRole("User");
        }

        private void AddRole(string roleName)
        {
            if (!_unitOfWork.Roles.Any(roleName))
            {
                _unitOfWork.Roles.Add(new Role() { Name = roleName });
                _unitOfWork.Complete();
            }
        }
    }
}