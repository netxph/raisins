using Raisins.Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Notifications.Models;
using DATA = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class MailQueueRepository : IMailQueueRepository
    {
        private RaisinsContext _context;

        public MailQueueRepository() : this(RaisinsContext.Instance)
        {
        }
        public MailQueueRepository(RaisinsContext context)
        {
            _context = context;
        }
        public void Add(MailQueue mailQueue)
        {
            _context.MailQueues.Add(ConvertToEF((mailQueue)));
            _context.SaveChanges();
        }
        public void Add(MailQueues mailQueues)
        {
            _context.MailQueues.AddRange(ConvertToEFList((mailQueues)));
            _context.SaveChanges();
        }

        //TODO
        public void DeleteMultiple(MailQueues mailQueue)
        {
            throw new NotImplementedException();
        }

        public MailQueues GetAll(int count)
        {
            var mails = new MailQueues();
            var data = _context.MailQueues.Where(m => !m.Status && string.IsNullOrEmpty(m.Content)).Take(count);

            foreach (var item in data)
            {
                var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == item.PaymentID);
                if (payment != null)
                {
                    var tickets = _context.Tickets.Where(t => t.PaymentID == payment.PaymentID).ToList();

                    if (tickets.Any())
                    {
                        var mail = new MailQueue(item.PaymentID, item.To, item.Status);
                        mail.SetName(payment.Name);
                        mail.SetAmount(payment.Amount);
                        mail.SetTickets(tickets.ConvertAll(t => new Ticket(t.TicketCode)));
                         
                        mails.Add(mail);

                        item.Status = true;
                    }

                }

            }

            _context.SaveChanges();
            return mails;
        }

        private MailQueue ConvertToDomain(DATA.MailQueue efMailQueue)
        {
            return new MailQueue(efMailQueue.PaymentID, efMailQueue.To, efMailQueue.Status);
        }
        private MailQueues ConvertToDomainList(IEnumerable<DATA.MailQueue> efMailQueues)
        {
            MailQueues mailQueues = new MailQueues();
            foreach (var efMailQueue in efMailQueues)
            {
                mailQueues.Add(ConvertToDomain(efMailQueue));
            }
            return mailQueues;
        }

        private DATA.MailQueue ConvertToEF(MailQueue mailQueue)
        {
            return new DATA.MailQueue(mailQueue.PaymentID, mailQueue.To);
        }

        private IEnumerable<DATA.MailQueue> ConvertToEFList(MailQueues mailqueues)
        {
            List<DATA.MailQueue> efMailqueues = new List<DATA.MailQueue>();
            foreach (var mailqueue in mailqueues)
            {
                efMailqueues.Add(ConvertToEF(mailqueue));
            }
            return efMailqueues;
        }
    }
}
