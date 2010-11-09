using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using System.Web.Mvc;

namespace Raisins.Client.Web.Models
{
    public class AccountModel
    {

        public decimal Amount { get; set; }
        public long BeneficiaryID { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public long ID { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string SelectedBeneficiary { get; set; }
        
    }
}