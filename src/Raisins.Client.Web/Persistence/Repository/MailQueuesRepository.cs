using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class MailQueuesRepository : IMailQueuesRepository
    {
        private RaisinsDB _raisinsDb;

        public MailQueuesRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }

        public IEnumerable<MailQueue> GetAll()
        {
            return _raisinsDb.MailQueues;
        }

        public void DeleteMultiple(IEnumerable<MailQueue> mailQueues)
        {
            _raisinsDb.MailQueues.RemoveRange(mailQueues);
        }

        public void Add(MailQueue mailQueue)
        {
            _raisinsDb.MailQueues.Add(mailQueue);
        }
    }
}