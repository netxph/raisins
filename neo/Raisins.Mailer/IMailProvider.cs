namespace Raisins.Mailer;

public interface IMailProvider
{
   void Send(Mail message);
}
