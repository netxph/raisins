using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Raisins.Client.MailJob
{
    public class ConfigurationProvider : IConfigurationProvider
    {

        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key]; 
        }

    }
}
