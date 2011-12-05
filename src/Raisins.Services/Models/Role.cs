using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Services.Models
{
    public class Role
    {

        public int RoleID { get; set; }

        public int RoleType { get; set; }

    }

    public enum RoleType
    {
        Administrator,
        Auditor,
        User
    }
}