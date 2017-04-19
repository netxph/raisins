using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Models
{
    public class AccountProfile
    {
        public AccountProfile(string name, IEnumerable<Beneficiary> beneficiaries)
        {
            Name = name;
            Beneficiaries = beneficiaries.ToList();
        }
        public AccountProfile(int profileID, string name, IEnumerable<Beneficiary> beneficiaries)
        {
            ProfileID = profileID;
            Name = name;
            Beneficiaries = beneficiaries.ToList();
        }
        public AccountProfile(int profileID, string name, IEnumerable<Beneficiary> beneficiaries, bool isLocal)
        {
            ProfileID = profileID;
            Name = name;
            Beneficiaries = beneficiaries.ToList();
            IsLocal = isLocal;
        }
        public AccountProfile()
        {
        }
        [Key]
        public int ProfileID { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
        public bool IsLocal { get; set; }
    }
}
