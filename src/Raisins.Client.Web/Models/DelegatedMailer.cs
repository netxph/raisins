using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class DelegatedMailer : IMailer
    {

        const string NAME = "Delegate";

        public string Name { get { return NAME; } }

        public void SendMessage(MailMessage message)
        {
            var mail = new MailQueue()
            {
                From = message.From.Address,
                To = message.To[0].Address,
                Subject = message.Subject,
                Content = message.Body
            };

            MailQueue.Push(mail);
        }

    }
}
