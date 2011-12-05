using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Services.Models
{
    public class Setting
    {
        
        public int SettingID { get; set; }

        public int CurrencyID { get; set; }

        public string Location { get; set; }

        public int Class { get; set; }

        public int BeneficiaryID { get; set; }

    }
}