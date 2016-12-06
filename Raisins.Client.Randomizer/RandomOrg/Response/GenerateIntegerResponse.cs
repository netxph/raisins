using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.Randomizer.RandomOrg.Response
{
    public class GenerateIntegerResponse
    {
        public string jsonrpc { get; set; }
        public GenerateIntegerResponseResult result { get; set; }
        public int id { get; set; }
    }
}
