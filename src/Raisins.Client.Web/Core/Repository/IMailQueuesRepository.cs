using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IMailQueuesRepository
    {
        IEnumerable<MailQueue> GetAll();
        void DeleteMultiple(IEnumerable<MailQueue> mailQueue);
        void Add(MailQueue mailQueue);
    }
}