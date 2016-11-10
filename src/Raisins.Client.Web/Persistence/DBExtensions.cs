using System.Collections.Generic;
using System.Data.Entity;

namespace Raisins.Client.Web.Persistence
{
    public static class DBExtensions
    {

        public static void SetState<T>(this IList<T> dbList, DbContext context, EntityState state)
            where T: class
        {
            foreach (var item in dbList)
            {
                context.Entry<T>(item).State = state;
            }
        }

    }
}