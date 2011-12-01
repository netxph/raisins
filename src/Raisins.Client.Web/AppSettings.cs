using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Raisins.Client.Web
{
    public class AppSettings
    {

        public static string SmtpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["smtpServer"];
            }
        }

        public static int SmtpPort
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            }
        }

        public static string TicketSender
        {
            get
            {
                return ConfigurationManager.AppSettings["ticketSender"];
            }
        }

    }
}