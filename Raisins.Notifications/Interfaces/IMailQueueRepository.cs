using Raisins.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Interfaces
{
    public interface IMailQueueRepository
    {
        MailQueues GetAll();
        void DeleteMultiple(MailQueues mailQueue);
        void Add(MailQueue mailQueue);
        void Add(MailQueues mailQueues);
    }
}
