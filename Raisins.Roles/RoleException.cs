using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Roles
{
    public class RoleException : Exception
    {
        public int ErrorCode { get; set; }

        public RoleException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
