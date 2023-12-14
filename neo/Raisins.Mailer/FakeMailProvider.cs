namespace Raisins.Mailer;

public class FakeMailProvider : IMailProvider
{
   public void Send(Mail message)
   {
      Console.WriteLine($"FROM: {message.From}");
      Console.WriteLine($"TO: {message.To}");
      Console.WriteLine($"SUBJECT: {message.Subject}\r\n");
      Console.WriteLine(message.Subject);
   }
}
