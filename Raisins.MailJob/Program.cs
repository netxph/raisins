using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Raisins.MailJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();

            //Mailer.Instance = new SmtpMailer("localhost", 2525);
            Mailer.Instance = new OutlookMailer();

            var count = int.Parse(ConfigurationManager.AppSettings["MailCount"]);
            var sleep = int.Parse(ConfigurationManager.AppSettings["SleepTime"]);
            var baseUri = ConfigurationManager.AppSettings["ServiceBaseUri"];

            var job = new Job(count, sleep, baseUri);

            Console.WriteLine("Raisins MailJob");
            Console.WriteLine("Type [Q] to exit.");

            var task = Task.Factory.StartNew(() => job.Run(tokenSource.Token));

            if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                tokenSource.Cancel();

                try
                {
                    task.Wait();
                }
                catch (AggregateException ae)
                {
                    foreach (var ex in ae.InnerExceptions)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                finally
                {
                    tokenSource.Dispose();
                }
            }

            
        }
    }
}
