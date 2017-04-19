using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Notifications.Models
{
    public class MailQueues : IEnumerable<MailQueue>
    {
        private readonly List<MailQueue> _mailQueues;
        public MailQueues()
        {
            _mailQueues = new List<MailQueue>();
        }
        public void AddRange(IEnumerable<MailQueue> mailQueues)
        {
            _mailQueues.AddRange(mailQueues);
        }
        public void Add(MailQueue mailQueue)
        {
            _mailQueues.Add(mailQueue);
        }

        public IEnumerator<MailQueue> GetEnumerator()
        {
            return _mailQueues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mailQueues.GetEnumerator();
        }

    }
}
