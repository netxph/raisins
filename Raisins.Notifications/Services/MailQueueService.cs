using Raisins.Notifications.Interfaces;
using Raisins.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Services
{
    public class MailQueueService : IMailQueueService
    {
        private readonly IMailQueueRepository _mailQueueRepository;
        protected IMailQueueRepository MailQueueRepository { get { return _mailQueueRepository; } }

        public MailQueueService(IMailQueueRepository mailQueueRepository)
        {
            if (mailQueueRepository == null)
            {
                throw new ArgumentNullException("MailQueueService:mailQueueRepository");
            }
            _mailQueueRepository = mailQueueRepository;
        }

        public void AddListToQueue(MailQueues mails)
        {
            MailQueueRepository.Add(mails);
        }

        public void AddToQueue(MailQueue mail)
        {
            MailQueueRepository.Add(mail);
        }

        public MailQueues GetAll(int count)
        {
            return MailQueueRepository.GetAll(count);
        }
    }
}
