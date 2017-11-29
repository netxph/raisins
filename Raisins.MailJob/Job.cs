using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raisins.MailJob
{
    public class Job
    {
        
        public int Count { get; private set; }
        public int Interval { get; private set; }

        public Job(int count, int interval)
        {
            Count = count;
            Interval = interval;
        }

        public void Run(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Hello world!!!");
            }
        }
    }
}