using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class SettingModel
    {

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public long BeneficiaryID { get; set; }

    }
}