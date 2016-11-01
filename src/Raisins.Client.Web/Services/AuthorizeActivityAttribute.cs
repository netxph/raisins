using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Web.Services
{
    public class AuthorizeActivityAttribute : AuthorizeAttribute
    {
        private UnitOfWork _unitOfWork;

        public string ActivityName { get; set; }

        public AuthorizeActivityAttribute(string activityName)
        {
            ActivityName = activityName;
            _unitOfWork = new UnitOfWork(new RaisinsDB());

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var account = _unitOfWork.Accounts.GetCurrentUserAccount();
            Activity activity = _unitOfWork.Activities.GetActivityByName(ActivityName);
            return activity.DoUserRolesExists(account.Roles);
        }

    }
}