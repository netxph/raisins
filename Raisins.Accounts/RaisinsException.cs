using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts
{
    public abstract class RaisinsException : Exception
    {
        public int ErrorCode { get; set; }

        public RaisinsException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
