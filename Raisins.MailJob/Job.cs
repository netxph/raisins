using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Raisins.MailJob
{
    public class Job
    {
        
        public int Count { get; private set; }
        public int Interval { get; private set; }
        public string SmtpHost { get; private set; }
        public int SmtpPort { get; private set; }
        public string ServerBaseUri { get; private set; }

        public Job(int count, int interval, string smtpHost, int smtpPort, string serverBaseUri)
        {
            Count = count;
            Interval = interval;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            ServerBaseUri = serverBaseUri;

            Mailer.Instance = new SmtpMailer(smtpHost, smtpPort);
        }

        public void Run(CancellationToken token)
        {
            var client = new RestClient(ServerBaseUri);
            var text = File.ReadAllLines("MailTemplate.tml");
            var subject = text[0];
            var template = string.Join("", text.Skip(1).ToArray());

            var request = new RestRequest("mailqueuesall", Method.GET);
            request.AddQueryParameter("count", Count.ToString());

            while (!token.IsCancellationRequested)
            {
                var response = client.Execute<List<MailQueue>>(request);

                foreach (var mailQueue in response.Data)
                {
                    try
                    {
                        Console.Write($"[{mailQueue.PaymentID}] Sending email to {mailQueue.Name} [{mailQueue.To}]... ");

                        var message = new Mail("no-reply@navitaire.com", mailQueue.To)
                        {
                            Subject = subject
                        };

                        var ticketString = string.Join("<br>", mailQueue.Tickets.Select(t => t.Code).ToArray());

                        var body = template.Replace("{Beneficiary}", mailQueue.Beneficiary);
                        body = body.Replace("{Name}", mailQueue.Name);
                        body = body.Replace("{Tickets}", ticketString);

                        message.Body = body;

                        Mailer.Send(message);

                        Console.WriteLine("DONE.");
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FAILED.");
                        Console.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine($"Sleeping for {Interval}ms...");
                Thread.Sleep(Interval);
            }
        }
    }

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

    public abstract class Mailer
    {

        private static IMailProvider _mailProvider;

        public static IMailProvider Instance
        {
            get { return _mailProvider; }
            set { _mailProvider = value; }
        }

        public static void Send(Mail message)
        {
            Instance.OnSend(message);
        }
    }

    public class Mail
    {

        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Mail(string from, string to)
        {
            From = from;
            To = to;
            Subject = string.Empty;
            Body = string.Empty;
        }
    }

    public interface IMailProvider
    {
        void OnSend(Mail message);
    }
}