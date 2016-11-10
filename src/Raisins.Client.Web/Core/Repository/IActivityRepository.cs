using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IActivityRepository
    {
        Activity GetActivityByName(string activityName);
        void Add(Activity activity);
        bool Any(string activityName);
    }
}