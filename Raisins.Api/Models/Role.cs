using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Api.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}