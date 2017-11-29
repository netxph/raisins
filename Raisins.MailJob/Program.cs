using System;
using System.Collections.Generic;
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
            var job = new Job(10, 10000);

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
