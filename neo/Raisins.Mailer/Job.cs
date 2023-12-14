using RestSharp;

namespace Raisins.Mailer;

public class Job
{
   private readonly int _count;
   private readonly int _interval;
   private readonly string _baseUri;

   public Job(int count, int interval, string baseUri)
   {
      _count = count > 0 ? count : throw new ArgumentOutOfRangeException(nameof(count));
      _interval = interval > 0 ? interval : throw new ArgumentOutOfRangeException(nameof(interval));
      _baseUri = !string.IsNullOrEmpty(baseUri) ? baseUri : throw new ArgumentNullException(nameof(baseUri));
   }

   public void Run(CancellationToken token, IMailProvider provider)
   {
      var client = new RestClient(_baseUri);
      var text = File.ReadAllLines("MailTemplate.tml");
      var subject = text[0];
      var template = string.Join("", text.Skip(1).ToArray());

      var request = new RestRequest("mailqueuesall", Method.Get);
      request.AddQueryParameter("count", _count.ToString());

      while (!token.IsCancellationRequested)
      {
         var response = client.Execute<List<MailQueue>>(request);

         if(response != null && response.Data != null)
         {
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

                  provider.Send(message);

                  Console.WriteLine("DONE.");
                  Thread.Sleep(5000);
               }
               catch (Exception ex)
               {
                  Console.WriteLine("FAILED.");
                  Console.WriteLine(ex.Message);
               }
            }
         }

         Console.WriteLine($"Sleeping for {_interval}ms...");
         Thread.Sleep(_interval);
      }

   }
}
