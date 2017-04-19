using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Kernel
{
    public interface ICryptProvider
    {
        string Encrypt(string token);
        string Decrypt(string token);
        string Hash(string password, string salt);
    }
}
