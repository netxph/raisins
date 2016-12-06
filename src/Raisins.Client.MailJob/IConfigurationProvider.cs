using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.MailJob
{
    public interface IConfigurationProvider
    {
        
        string Get(string key);

    }
}
