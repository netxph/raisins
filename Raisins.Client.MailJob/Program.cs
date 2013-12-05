using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.MailJob
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var mailer = new Mailer();
                mailer.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
            }

        }
    }
}
