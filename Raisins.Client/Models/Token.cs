using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Models
{
    public class Token
    {
        public string Role { get; private set; }
        public int DaysExpire { get; private set; }
        public List<string> Permissions { get; private set; }
        public string User { get; private set; }
    }
}