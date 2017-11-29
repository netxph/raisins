using Raisins.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Interfaces
{
    public interface IMailQueueService
    {
        void AddToQueue(MailQueue mail);
        void AddListToQueue(MailQueues mails);
        MailQueues GetAll(int count);
    }
}
