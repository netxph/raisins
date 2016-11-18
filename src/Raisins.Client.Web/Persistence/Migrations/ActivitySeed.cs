using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Collections.Generic;

namespace Raisins.Client.Web.Migrations
{
    public class ActivitySeed : IDbSeeder
    {
        private UnitOfWork _unitOfWork;
        public void Seed(RaisinsDB context)
        {
            _unitOfWork = new UnitOfWork(context);
            AddActivity("Payment.Edit", "Administrator", "User");
            AddActivity("Payment.Lock", "Administrator", "Accountant");
            AddActivity("Home.Dashboard", "Administrator");
        }

        private void AddActivity(string activityName, params string[] roleNames)
        {
            if (!_unitOfWork.Activities.Any(activityName))
            {
                Activity newActivity = new Activity() { Name = activityName };
                List<Role> roles = new List<Role>();
                
                foreach (string roleName in roleNames)
                {
                    roles.Add(_unitOfWork.Roles.Get(roleName));
                }

                newActivity.Roles = roles;

                _unitOfWork.Activities.Add(newActivity);
                _unitOfWork.Complete();
            }
        }
    }
}