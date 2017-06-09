using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Accounts
{
    public class InvalidUserException : RaisinsException
    {
        const string ERROR_MESSAGE = "Invalid User!";
        public InvalidUserException(string message) : base(message, RaisinsErrorCodes.INVALID_USER)
        {
            
        }
        public InvalidUserException() : this(ERROR_MESSAGE)
        {

        }
    }
}
