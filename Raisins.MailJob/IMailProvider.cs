namespace Raisins.MailJob
{
    public interface IMailProvider
    {
        void OnSend(Mail message);
    }
}