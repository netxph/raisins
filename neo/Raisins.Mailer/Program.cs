// See https://aka.ms/new-console-template for more information
using Raisins.Mailer;

const int DEFAULT_COUNT = 10;
const int DEFAULT_SLEEP_TIME = 30000;

var count = DEFAULT_COUNT;
var sleep = DEFAULT_SLEEP_TIME;
var baseUri = "http://localhost:4000/api";

var job = new Job(count, sleep, baseUri);

Console.WriteLine("Raisins Neo Mailjob");
Console.WriteLine("Type [Q] to exit.");

var tokenSource = new CancellationTokenSource();

var mailer = new FakeMailProvider();

var task = Task.Factory.StartNew(() => job.Run(tokenSource.Token, mailer));

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
