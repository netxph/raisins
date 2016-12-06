using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.MailJob
{
    public class Settings
    {

        public Settings()
            : this(new ConfigurationProvider())
        {
        }

        public Settings(IConfigurationProvider provider)
        {
            Configuration = provider;
        }

        protected IConfigurationProvider Configuration { get; set; }

        public string ServiceBaseUrl
        {
            get { return Configuration.Get("ServiceBaseUrl"); }
        }

        public string SmtpHost
        {
            get { return Configuration.Get("SmtpHost"); }
        }

        public int SmtpPort
        {
            get { return int.Parse(Configuration.Get("SmtpPort")); }
        }

    }
}
