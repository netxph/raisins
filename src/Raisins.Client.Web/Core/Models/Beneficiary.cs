using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Beneficiary
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string VideoLink { get; set; }
    }
}