using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class DefaultMailer : IMailer
    {

        const string NAME = "SMTP";
        const string DEFAULT_HOST = "mailhost.navitaire.com";
        const int DEFAULT_PORT = 25;

        public string Name { get { return NAME; } }

        public void SendMessage(MailMessage message)
        {
            SmtpClient smtp = new SmtpClient(DEFAULT_HOST, DEFAULT_PORT); 
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Send(message);
        }

    }
}
