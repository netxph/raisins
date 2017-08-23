using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder
{
    public class InvalidDataSeedException : DataSeedException
    {
        public InvalidDataSeedException(string message) : base(message, DataSeedErrorCode.INVALID_DATASEED)
        {
            
        }

    }
}
