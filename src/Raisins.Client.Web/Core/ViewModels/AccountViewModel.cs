using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Raisins.Client.Web.Core.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }

        public Role RoleObject { get; set; }

        [Required]
        public int Beneficiary { get; set; }

        [Required]
        public int Currency { get; set; }

        public IEnumerable<Beneficiary> Beneficiaries;

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }


    }
}