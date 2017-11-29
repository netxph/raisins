using System;
using System.Collections.Generic;
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
        }

        public void Run(CancellationToken token)
        {
            var client = new RestClient(ServerBaseUri);

            var mailClient = new SmtpClient(SmtpHost, SmtpPort);
            mailClient.UseDefaultCredentials = true;

            var request = new RestRequest("mailqueuesall", Method.GET);
            request.AddQueryParameter("count", Count.ToString());

            while (!token.IsCancellationRequested)
            {
                var response = client.Execute<List<MailQueue>>(request);

                foreach (var mailQueue in response.Data)
                {
                    try
                    {
                        Console.Write($"Sending email to {mailQueue.Name} [{mailQueue.To}]... ");

                        var message = new MailMessage("no-reply@navitaire.com", mailQueue.To)
                        {
                            Subject = "[Do the HMS Move 2017] - Ticket Notification",
                            IsBodyHtml = true
                        };

                        message.Body = string.Join("<br>", mailQueue.Tickets.Select(t => t.Code).ToArray());
                        mailClient.Send(message);

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
}