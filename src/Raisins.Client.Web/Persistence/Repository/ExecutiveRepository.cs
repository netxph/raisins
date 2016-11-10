using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class ExecutiveRepository : IExecutiveRepository
    {
        private RaisinsDB _raisinsDb;

        public ExecutiveRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }
        public IEnumerable<Executive> GetAll()
        {
            return _raisinsDb.Executives;
        }
    }
}