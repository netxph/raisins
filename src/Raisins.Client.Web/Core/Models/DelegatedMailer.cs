using Raisins.Client.Web.Core;
using System.Net.Mail;

namespace Raisins.Client.Web.Models
{
    public class DelegatedMailer : IMailer
    {
        private IUnitOfWork _unitOfWork;

        const string NAME = "Delegate";

        public string Name { get { return NAME; } }

        public DelegatedMailer()
        {

        }
        public DelegatedMailer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SendMessage(MailMessage message)
        {
            var mail = new MailQueue()
            {
                From = message.From.Address,
                To = message.To[0].Address,
                Subject = message.Subject,
                Content = message.Body
            };

            _unitOfWork.MailQueues.Add(mail);
            _unitOfWork.Complete();
        }

    }
}
