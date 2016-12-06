using Raisins.Client.Web.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.MailJob
{
    public class Mailer
    {


        public Mailer()
        {
            Settings = new Settings();
        }

        protected Settings Settings { get; set; }

        public void Run()
        {
            List<MailQueue> mails = DequeueMails(); 
            
            while(mails != null && mails.Count > 0)
            {
                Send(mails);

                mails = DequeueMails();
            }
        }

        public List<MailQueue> DequeueMails()
        {
            var client = new RestClient(Settings.ServiceBaseUrl);
            var request = new RestRequest("mailer", Method.GET);

            var response = client.Execute<List<MailQueue>>(request);

            return response.Data;
        }

        public void Send(List<MailQueue> mails)
        {
            
            var smtp = new SmtpClient(Settings.SmtpHost, Settings.SmtpPort);

            foreach(var mail in mails)
            {
                var message = new MailMessage(mail.From, mail.To);
                message.Body = mail.Content;
                message.Subject = mail.Subject;
                message.IsBodyHtml = true;

                smtp.Send(message);
            }
        
        }

    }
}
