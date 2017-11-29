using System;
using System.Collections.Generic;
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

        public Job(int count, int interval, string smtpHost, int smtpPort)
        {
            Count = count;
            Interval = interval;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            ServerBaseUri = "http://localhost:4000/api";
        }

        public void Run(CancellationToken token)
        {
            var client = new RestClient(ServerBaseUri);
            var request = new RestRequest("mailqueuesall", Method.GET);
            request.AddQueryParameter("count", Count.ToString());

            while (!token.IsCancellationRequested)
            {
                var response = client.Execute<List<MailQueue>>(request);

                foreach (var mailQueue in response.Data)
                {
                    Console.WriteLine(mailQueue.PaymentID);
                    Console.WriteLine(mailQueue.Name);
                    Console.WriteLine(mailQueue.Amount);
                    Console.WriteLine(mailQueue.Tickets.Count);
                }

                Console.WriteLine($"Sleeping for {Interval}ms...");
                Thread.Sleep(Interval);
            }
        }
    }
}