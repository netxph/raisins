using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Raisins.MailJob
{
    public class Job
    {
        
        public int Count { get; private set; }
        public int Interval { get; private set; }
        public string ServerBaseUri { get; private set; }

        public Job(int count, int interval, string serverBaseUri)
        {
            Count = count;
            Interval = interval;
            ServerBaseUri = serverBaseUri;

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
                        Thread.Sleep(5000);
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