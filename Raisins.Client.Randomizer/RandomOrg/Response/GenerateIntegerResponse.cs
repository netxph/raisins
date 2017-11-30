using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Client.Randomizer.RandomOrg.Response
{
    public class GenerateIntegerResponse
    {
        public string JsonRpc { get; set; }
        public GenerateIntegerResponseResult Result { get; set; }
        public int Id { get; set; }

        public int Code { get; set; }
        public GenerateIntegerErrorResponse Error { get; set; }
    }
}
