using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
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