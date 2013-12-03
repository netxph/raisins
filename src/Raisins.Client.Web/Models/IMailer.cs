using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public interface IMailer
    {

        string Name { get; }
        void SendMessage(MailMessage message);

    }
}
