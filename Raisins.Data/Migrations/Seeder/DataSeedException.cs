using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder
{
    public class DataSeedException : Exception
    {
        public int ErrorCode { get; set; }

        public DataSeedException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
