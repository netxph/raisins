namespace Raisins.Mailer;

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
