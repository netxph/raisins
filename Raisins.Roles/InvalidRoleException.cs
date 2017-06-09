using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Roles
{
    public class InvalidRoleException : RoleException
    {
        const string ERROR_MESSAGE = "Invalid Role!";

        public InvalidRoleException(string message) : base(message, RoleErrorCode.INVALID_ROLE)
        {

        }
        public InvalidRoleException() : this(ERROR_MESSAGE)
        {

        }
    }
}
