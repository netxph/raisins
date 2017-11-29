using System.Net.Mail;

namespace Raisins.MailJob
{
    public class SmtpMailer : IMailProvider
    {

        private readonly SmtpClient _client;

        public SmtpMailer(string smtpHost, int smtpPort)
        {
            _client = new SmtpClient(smtpHost, smtpPort);
            _client.UseDefaultCredentials = true;
        }

        public void OnSend(Mail message)
        {
            var mail = new MailMessage(message.From, message.To);
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            _client.Send(mail);
        }
    }
}