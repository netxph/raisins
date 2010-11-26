using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Raisins.Services
{
    [ActiveRecord]
    public class Role : ActiveRecordBase<Role>
    {

        [PrimaryKey(PrimaryKeyType.Foreign)]
        public int ID { get; set; }

        [OneToOne]
        public Account Account { get; set; }

        [Property]
        public RoleType RoleType { get; set; }

    }

    public enum RoleType
    { 
        Administrator,
        Auditor,
        User
    }
}
