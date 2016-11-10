using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Role
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
