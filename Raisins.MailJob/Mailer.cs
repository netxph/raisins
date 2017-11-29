namespace Raisins.MailJob
{
    public abstract class Mailer
    {

        private static IMailProvider _mailProvider;

        public static IMailProvider Instance
        {
            get { return _mailProvider; }
            set { _mailProvider = value; }
        }

        public static void Send(Mail message)
        {
            Instance.OnSend(message);
        }
    }
}