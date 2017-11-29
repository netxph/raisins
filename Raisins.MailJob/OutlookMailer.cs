using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;

namespace Raisins.MailJob
{
    public class OutlookMailer : IMailProvider
    {
        private readonly OutlookApp _client;

        public OutlookMailer()
        {
            _client = new OutlookApp();
        }

        public void OnSend(Mail message)
        {
            MailItem mail = _client.CreateItem(OlItemType.olMailItem);
            mail.Subject = message.Subject;
            mail.HTMLBody = message.Body;
            mail.To = message.To;

            mail.Send();
        }
    }
}
