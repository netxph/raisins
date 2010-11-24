using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class SettingModel
    {

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public Currency Currency { get; set; }
        public long BeneficiaryID { get; set; }
        public PaymentClass Class { get; set; }

    }
}