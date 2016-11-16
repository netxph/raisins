using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private RaisinsDB _raisinDb;

        public ActivityRepository(RaisinsDB raisinsDb)
        {
            _raisinDb = raisinsDb;
        }


        public Activity GetActivityByName(string activityName)
        {
            return _raisinDb.Activities
                    .Include(a => a.Roles)
                    .FirstOrDefault(a => a.Name == activityName);
        }

        public void Add(Activity activity)
        {
            //activity.Roles.SetState(_raisinDb, EntityState.Modified);
            _raisinDb.Activities.Add(activity);
            
        }

        public bool Any(string activityName)
        {
            return _raisinDb.Activities.Any(a => a.Name == activityName);
        }
    }
}